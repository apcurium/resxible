using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Com.Apcurium.Resxible.Localization.Android
{
    public class AndroidResourceFileHandler : ResourceFileHandlerBase
    {
        private readonly IList<HtmlEntity> _htmlEntities = new List<HtmlEntity>
        {
            new HtmlEntity {Normal = "&", Encoded = "&amp;"},
            new HtmlEntity {Normal = "<", Encoded = "&lt;"},
            new HtmlEntity {Normal = ">", Encoded = "&gt;"}
        };

        public AndroidResourceFileHandler(string filePath, bool overwriteContent) : base(filePath)
        {
            if (overwriteContent)
            {
                return;
            }

            XElement document;

            if(string.IsNullOrEmpty(File.ReadAllText(filePath)))
            {
                document = new XElement("resources");
            }
            else
            {
                try
                {
                    document = XElement.Load(filePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Warning: Could not load XML from file [{0}]. Exception: [{1}]", filePath, e.Message);
                    document = new XElement("resources");
                }
            }

            foreach (var localization in document.Elements().Where(e => e.Name.ToString().Equals("string", StringComparison.OrdinalIgnoreCase)))
            {
                var key = localization.FirstAttribute.Value;

                TryAdd(key, Decode(localization.Value));
            }
        }

        protected override string GetFileText()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<resources>\r\n");

            //<string name="key">value</string>
            foreach (var resource in this)
            {
                stringBuilder.AppendFormat("  <string name=\"{0}\">{1}</string>\r\n", resource.Key, Encode(resource.Value));
            }

            stringBuilder.Append("</resources>");

            return stringBuilder.ToString();
        }
        
        protected virtual string Encode(string text)
        {
            return EscapeQuotes(EncodeXml(text));
        }

        protected string Decode(string text)
        {
            return UnescapeQuotes(DecodeXml(text));
        }

        protected string DecodeXml(string text)
        {
            //Others invalid characters does not look to be escaped...
            foreach (var htmlEntity in _htmlEntities)
            {
                text = text.Replace(htmlEntity.Encoded, htmlEntity.Normal);
            }

            return text;
        }

        protected string EncodeXml(string text)
        {
            foreach (var htmlEntity in _htmlEntities)
            {
                text = text.Replace(htmlEntity.Normal, htmlEntity.Encoded);
            }

            return text;
        }
    }
}

