using System;
using System.IO;
using Com.Apcurium.Resxible.Localization;
using Com.Apcurium.Resxible.Localization.Android;
using Com.Apcurium.Resxible.Localization.iOS;
using Mono.Options;

namespace Com.Apcurium.Resxible
{
	class Program
	{
		static void Main(string[] args)
		{
			var target = string.Empty;
			var source = string.Empty;
            var ampSource = string.Empty;
            var destination = string.Empty;
            var backup = false;

			var p = new OptionSet()
			{
				{"t|target=", "Target application: ios or android", t => target = t.ToLowerInvariant()},
				{"c|common=", "Common .resx file path", a => ampSource = a},
				{"m|master=", "Master .resx file path", m => source = m},
				{"d|destination=", "Destination file path", d => destination = d},
				{"b|backup", "Backup file", b => backup = b != null}
			};

			try
			{
				p.Parse(args);
			}
			catch (OptionException e)
			{
				ShowHelpAndExit(e.Message, p);
			}

			try
			{
				var masterResxFile = new FileInfo(source);
			    if (masterResxFile.Directory != null)
			    {
			        var allMasterFileNames = masterResxFile.Directory.EnumerateFiles(GetSearchPattern(masterResxFile));
			        foreach (var localizedResourceFile in allMasterFileNames)
			        {
			            var language = ExtractLanguageCodeFromFileName(masterResxFile.Name, localizedResourceFile);
			            var languageSpecificAmpFile = GetLanguageSpecificAmpFile(ampSource, language);
			            var languageSpecificDestinationFolder = GetLanguageSpecificDestinationFolder(destination, language, target);

			            ConvertResourceFile(languageSpecificAmpFile, localizedResourceFile.FullName, 
                                                            target, languageSpecificDestinationFolder, backup);
			        }
			    }

			    Console.WriteLine("Localization tool ran successfully.");
			}

			catch (Exception exception)
			{
				Console.Write("error: ");
				Console.WriteLine(exception.ToString());
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

		private static string GetLanguageSpecificAmpFile(string ampSource, string language)
		{
			if (string.IsNullOrWhiteSpace(language))
			{
				return ampSource;
			}

			var ampResxFile = new FileInfo(ampSource);
		    if (ampResxFile.Directory != null)
		    {
		        var ampResxForLanguage = ampResxFile.Directory.GetFiles (string.Format ("{0}.{1}{2}", Path.GetFileNameWithoutExtension(ampResxFile.Name), language, ampResxFile.Extension));
		        if (ampResxForLanguage.Length == 1)
		        {
		            return ampResxForLanguage[0].FullName;
		        }
		    }

		    return ampSource;
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

	    private static void ShowHelpAndExit(string message, OptionSet optionSet)
		{
			Console.Error.WriteLine(message);
			optionSet.WriteOptionDescriptions(Console.Error);
			Environment.Exit(-1);
		}
	}
}
