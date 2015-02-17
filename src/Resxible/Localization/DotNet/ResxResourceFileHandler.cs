using System.Collections;
using System.ComponentModel.Design;
using System.IO;
using System.Resources;
using System.Text;

namespace Com.Apcurium.Resxible.Localization.DotNet
{
	public class ResxResourceFileHandler : ResourceFileHandlerBase
	{
		public ResxResourceFileHandler(string filePath) : base(filePath)
		{
			var resXResourceReader = new ResXResourceReader (filePath) { UseResXDataNodes = true };

			foreach (DictionaryEntry de in resXResourceReader)
			{
				var node = (ResXDataNode)de.Value;

				//FileRef is null if it's not a file reference
				if (node.FileRef == null)
				{
					TryAdd (node.Name, node.GetValue ((ITypeResolutionService)null).ToString ());
				}
			}
		}

		protected override string GetFileText ()
		{
            using(var stream = new MemoryStream())
            using(var writer = new ResXResourceWriter(stream))
            {
                foreach (var resource in this)
                {
                    writer.AddResource(resource.Key, resource.Value);
                }
                writer.Generate();

                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream, Encoding.UTF8, true, 4096, leaveOpen: true))
                {
                    return reader.ReadToEnd();
                }
            }
		}
	}
}

