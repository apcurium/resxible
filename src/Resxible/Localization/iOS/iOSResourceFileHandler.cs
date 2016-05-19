using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Apcurium.Resxible.Localization.iOS
{
    public class iOSResourceFileHandler : ResourceFileHandlerBase
    {
        public iOSResourceFileHandler(string filePath, bool overwriteContent) : base(filePath)
        {
            if (overwriteContent)
            {
                return;
            }

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim(' ', ';');

                var keyValue = trimmedLine.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim().Trim('"')).ToList();

                if (keyValue.Any())
                {
                    if (keyValue.Count == 2)
                    {
                        TryAdd(keyValue.First(), Decode(keyValue.ElementAt(1)));
                    }
                    else
                    {
                        throw new Exception("Conflict with line in localizations:" + line);
                    }
                }
            }
        }
        
        protected override string GetFileText()
        {
            var stringBuilder = new StringBuilder();

            //"key"="value"\n;
            foreach (var resource in this)
            {
                stringBuilder.AppendFormat("\"{0}\"=\"{1}\";\n", resource.Key, Encode(resource.Value));
            }

            return stringBuilder.ToString();
        }

        protected virtual string Encode(string text)
        {
            return EscapeQuotes(text);
        }

        protected virtual string Decode(string text)
        {
            return UnescapeQuotes(text);
        }
    }
}

