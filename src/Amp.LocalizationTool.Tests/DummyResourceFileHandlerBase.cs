namespace Amp.LocalizationTool.Tests
{
    class DummyResourceFileHandlerBase : ResourceFileHandlerBase
    {
        public DummyResourceFileHandlerBase(string filePath) : base(filePath)
        {
        }

        protected override string GetFileText()
        {
            throw new System.NotImplementedException();
        }
    }
}