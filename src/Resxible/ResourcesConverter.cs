using System;
using System.IO;
using Com.Apcurium.Resxible.Localization;
using Com.Apcurium.Resxible.Localization.Android;
using Com.Apcurium.Resxible.Localization.DotNet;
using Com.Apcurium.Resxible.Localization.iOS;

namespace Com.Apcurium.Resxible
{
    static class ResourcesConverter
    {
        public static void Generate(string source, string commonSource, string destination, string target, bool backup)
        {
            var masterResxFile = new FileInfo(source);
            if (masterResxFile.Directory != null)
            {
                //search for Master.fr.resx, Master.es.resx, etc files
                var allMasterFileNames = masterResxFile.Directory.EnumerateFiles(GetSearchPattern(masterResxFile));
                foreach (var localizedResourceFile in allMasterFileNames)
                {
                    var language = ExtractLanguageCodeFromFileName(masterResxFile.Name, localizedResourceFile);
                    var commonFile = GetLanguageSpecificCommonFile(commonSource, language);
                    var languageSpecificDestinationFolder = GetLanguageSpecificDestinationFolder(destination, language, target);

                    ConvertResourceFile(commonFile, localizedResourceFile.FullName, target, languageSpecificDestinationFolder, backup);
                }
            }
        }

        private static string GetSearchPattern(FileInfo file)
        {
            return string.Format ("{0}*{1}", Path.GetFileNameWithoutExtension(file.Name), file.Extension);
        }

        private static string ExtractLanguageCodeFromFileName(string originalFileName, FileInfo fileWithLanguageCode)
        {
            return fileWithLanguageCode.Name.ToLower ()
                .Replace (originalFileName.ToLower(), string.Empty) // if we have the original filename, we want to return string.empty
                .Replace (string.Format ("{0}.", Path.GetFileNameWithoutExtension (originalFileName).ToLower()), string.Empty)
                .Replace (Path.GetExtension(originalFileName), string.Empty);
        }

        private static string GetLanguageSpecificCommonFile(string commonSource, string language)
        {
            if (string.IsNullOrWhiteSpace(commonSource))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(language))
            {
                return commonSource;
            }

            var resxFile = new FileInfo(commonSource);
            if (resxFile.Directory != null)
            {
                var resxForLanguage = resxFile.Directory.GetFiles (string.Format ("{0}.{1}{2}", Path.GetFileNameWithoutExtension(resxFile.Name), language, resxFile.Extension));
                if (resxForLanguage.Length == 1)
                {
                    return resxForLanguage[0].FullName;
                }
            }

            return commonSource;
        }

        private static string GetLanguageSpecificDestinationFolder(string destination, string language, string target)
        {
            if (string.IsNullOrWhiteSpace(language))
            {
                return destination.Trim();
            }

            switch (target)
            {
                case "android":
                    return destination.Trim().ToLower().Replace("values", string.Format("values-{0}", language));
                case "ios":
                    return destination.Trim().Replace("en.lproj", string.Format("{0}.lproj", language));
                default:
                    throw new InvalidOperationException("Invalid program arguments");
            }
        }

        private static void ConvertResourceFile(string commonFile, string masterFile, string target, string destination, bool backup)
        {
            var resourceManager = new ResourceManager();
            ResourceFileHandlerBase handler;

            if (!string.IsNullOrWhiteSpace (commonFile))
            {
                resourceManager.AddSource(new ResxResourceFileHandler(commonFile));
            }

            resourceManager.AddSource(new ResxResourceFileHandler(masterFile));

            switch (target)
            {
                case "android":
                    resourceManager.AddDestination(handler = new AndroidResourceFileHandler(destination));
                    break;
                case "ios":
                    resourceManager.AddDestination(handler = new iOSResourceFileHandler(destination));
                    break;
                default:
                    throw new InvalidOperationException("Invalid program arguments");
            }

            resourceManager.Update();
            handler.Save(backup);
        }
    }
}