using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Com.Apcurium.Resxible.Localization
{
    //Working with .resx Files Programmatically http://msdn.microsoft.com/en-us/library/gg418542.aspx
    public abstract class ResourceFileHandlerBase : SortedDictionary<string, string>
    {
        private readonly string _filePath;
        private readonly HashSet<string> _duplicateKeys;
        private readonly IList<HtmlEntity> _htmlEntities = new List<HtmlEntity>();

        protected ResourceFileHandlerBase(string filePath) : base(StringComparer.OrdinalIgnoreCase)
        {
            _duplicateKeys = new HashSet<string>();
            _filePath = filePath;
            LoadHtmlEntities();
            CreateFileIfMissing ();
        }

        public virtual string Name
        {
            get { return Path.GetFileName(_filePath); }
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

        protected void TryAdd(string key, string value)
        {
            if (ContainsKey(key))
            {
                _duplicateKeys.Add(key);
            }
            else
            {
                Add(key, value);
            }
        }

        public string Save(bool backupOldFile = true)
        {
            string backupFilePath = null;

            if (backupOldFile)
            {
                backupFilePath = GetBackupFilePath();

                File.Copy(_filePath, backupFilePath);
            }

            File.WriteAllText(_filePath, GetFileText());

            return backupFilePath;
        }

        protected abstract string GetFileText();

        private string GetBackupFilePath()
        {
            return string.Format("{3}\\{0}-{1:yyyy-MM-dd_hh-mm-ss-FFFFFF}{2}",
                Path.GetFileNameWithoutExtension(_filePath), 
                DateTime.Now,
                Path.GetExtension(_filePath),
                Path.GetDirectoryName(_filePath));
        }

        private void LoadHtmlEntities()
        {
            _htmlEntities.Add(new HtmlEntity { Normal = "&", Encoded = "&amp;" }); // must be done first
            _htmlEntities.Add(new HtmlEntity { Normal = "<", Encoded = "&lt;" });
            _htmlEntities.Add(new HtmlEntity { Normal = ">", Encoded = "&gt;" });
        }

        private void CreateFileIfMissing()
        {
            var file = new FileInfo(_filePath);

            if (file.Directory != null
                && !file.Directory.Exists)
            {
                Directory.CreateDirectory(file.Directory.FullName);
            }

            if (!file.Exists)
            {
                file.Create().Dispose();
            }
        }

        public ReadOnlyCollection<string> DuplicateKeys { get { return _duplicateKeys.ToList().AsReadOnly(); } }
    }
}

