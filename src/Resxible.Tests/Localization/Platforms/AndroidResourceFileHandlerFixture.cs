using System.IO;
using Com.Apcurium.Resxible.Localization.Android;
using NUnit.Framework;

namespace Com.Apcurium.Resxible.Tests.Localization.Platforms
{
    [TestFixture]
    public class AndroidResourceFileHandlerFixture
    {
        private const string AndroidFileName = "Strings.Copy.xml";

        [SetUp]
        public void Setup()
        {
            File.Delete(AndroidFileName);
            File.Copy("Strings.xml", AndroidFileName);
        }

        [Test]
        public void Ctor_WithFile_CorrectlyInitialized()
        {
            var sut = new AndroidResourceFileHandler(AndroidFileName, false);

            Assert.That(sut.Keys.Count, Is.EqualTo(3));
        }

        [Test]
        public void AddValue_Save_FileUpdated()
        {
            var sut = new AndroidResourceFileHandler(AndroidFileName, false);

            sut["anotherkey"] = "test";
            sut.Save(false);

            sut = new AndroidResourceFileHandler(AndroidFileName, false);
            Assert.That(sut.Keys.Count, Is.EqualTo(4));
        }

        [Test]
        public void AddValue_WithClearOption_Save_FileUpdated()
        {
            var sut = new AndroidResourceFileHandler(AndroidFileName, true);

            sut["anotherkey"] = "test";
            sut.Save(false);

            sut = new AndroidResourceFileHandler(AndroidFileName, false);
            Assert.That(sut.Keys.Count, Is.EqualTo(1));
        }
    }
}
