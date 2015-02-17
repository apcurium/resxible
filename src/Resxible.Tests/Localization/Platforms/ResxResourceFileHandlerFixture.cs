using System.IO;
using Com.Apcurium.Resxible.Localization.DotNet;
using NUnit.Framework;

namespace Com.Apcurium.Resxible.Tests.Localization.Platforms
{
    [TestFixture]
    public class ResxResourceFileHandlerFixture
    {
        private const string ResxFileName = "ResxFileHandlerResourceTestData.Copy.resx";

        [SetUp]
        public void Setup()
        {
            File.Delete("ResxFileHandlerResourceTestData.Copy.resx");
            File.Copy("ResxFileHandlerResourceTestData.resx", "ResxFileHandlerResourceTestData.Copy.resx");
        }

        [Test]
        public void Ctor_WithFile_CorrectlyInitialized()
        {
            var sut = new ResxResourceFileHandler(ResxFileName);

            Assert.That(sut.Keys.Count, Is.EqualTo(2));
        }

        [Test]
        public void AddValue_Save_ResxUpdated()
        {
            var sut = new ResxResourceFileHandler(ResxFileName);

            sut["anotherkey"] = "test";
            sut.Save(false);

            sut = new ResxResourceFileHandler(ResxFileName);
            Assert.That(sut.Keys.Count, Is.EqualTo(3));
        }
    }
}
