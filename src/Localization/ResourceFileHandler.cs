using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace Amp.LocalizationTool
{
	//Working with .resx Files Programmatically http://msdn.microsoft.com/en-us/library/gg418542.aspx
	public abstract class ResourceFileHandlerBase : Dictionary<string, string>
	{
		private readonly string _filePath;
		private readonly HashSet<string> _duplicateKeys;

		protected ResourceFileHandlerBase(string filePath) : base(StringComparer.OrdinalIgnoreCase)
		{
			_duplicateKeys = new HashSet<string>();
			_filePath = filePath;
			CreateFileIfMissing ();
		}

		private void CreateFileIfMissing()
		{
			var file = new FileInfo (_filePath);

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

		public virtual string Name
		{
			get { return Path.GetFileName(_filePath); }
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
                var resourceFileHandler = (ResourceFileHandlerBase)Activator.CreateInstance(GetType(), _filePath);
				backupFilePath = GetBackupFilePath();

				File.WriteAllText(backupFilePath, resourceFileHandler.GetFileText());
			}

			File.WriteAllText(_filePath, GetFileText());

			return backupFilePath;
		}

		protected abstract string GetFileText();

	    private string GetBackupFilePath()
		{
			return string.Format("{3}\\{0}-{1:yyyy-MM-dd_hh-mm-ss-tt}{2}",
				Path.GetFileNameWithoutExtension(_filePath), DateTime.Now,
				Path.GetExtension(_filePath),
				Path.GetDirectoryName(_filePath));
		}

		public ReadOnlyCollection<string> DuplicateKeys { get { return _duplicateKeys.ToList().AsReadOnly(); } }
	}
}

