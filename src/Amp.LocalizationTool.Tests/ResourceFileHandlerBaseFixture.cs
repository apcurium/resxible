using System.IO;
using NUnit.Framework;

namespace Amp.LocalizationTool.Tests
{
    [TestFixture]
    public class ResourceFileHandlerBaseFixture
    {
        [Test]
        public void Ctor_NoFile_FileCreated()
        {
            const string filename = "MyFile.txt";
            var sut = new DummyResourceFileHandlerBase(filename);

            Assert.That(File.Exists(filename));
        }
    }
}