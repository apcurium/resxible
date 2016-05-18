using System;
using System.IO;
using Com.Apcurium.Resxible.Localization;

namespace Com.Apcurium.Resxible.Tests
{
    class DummyResourceFileHandlerBase : ResourceFileHandlerBase
    {
        private string _content;

        public DummyResourceFileHandlerBase(string filePath) : base(filePath)
        {
            _content = File.ReadAllText(filePath);
        }

        protected override string GetFileText()
        {
            foreach (var entry in this)
            {
                _content += entry.Value + entry.Value + Environment.NewLine;
            }
            return _content;
        }
    }
}