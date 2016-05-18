using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Com.Apcurium.Resxible.Localization.Android
{
    public class AndroidResourceFileHandler : ResourceFileHandlerBase
    {
        public AndroidResourceFileHandler(string filePath) : base(filePath)
        {
            XElement document;
            try
            {
                document = XElement.Load(filePath);
            }
            catch(Exception e)
            {
                Console.WriteLine("Warning: Could not load XML from file {0}. Exception: {1}", filePath, e.Message);
                document = new XElement ("resources");
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

            //<string name="ApplicationName">TaxiHail</string>
            foreach (var resource in this)
            {
                stringBuilder.AppendFormat("  <string name=\"{0}\">{1}</string>\r\n", resource.Key, Encode(resource.Value));
            }

            stringBuilder.Append("</resources>");

            return stringBuilder.ToString();
        }
        
        protected virtual string Encode(string text)
        {
            return EncodeAndroid(EncodeXml(text));
        }

        protected string Decode(string text)
        {
            return DecodeAndroid(DecodeXml(text));
        }

        protected virtual string EncodeAndroid(string text)
        {
            return text.Replace("'", "\\'").Replace("\"", "\\\"");
        }

        private string DecodeAndroid(string text)
        {
            return text.Replace("\\'", "'").Replace("\\\"", "\"");
        }
    }
}

