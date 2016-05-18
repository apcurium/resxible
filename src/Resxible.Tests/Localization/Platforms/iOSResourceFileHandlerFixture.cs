using System.IO;
using Com.Apcurium.Resxible.Localization.iOS;
using NUnit.Framework;

namespace Com.Apcurium.Resxible.Tests.Localization.Platforms
{
    [TestFixture]
    public class iOSResourceFileHandlerFixture
    {
        private const string iOSFileName = "Localizable.Copy.strings";

        [SetUp]
        public void Setup()
        {
            File.Delete(iOSFileName);
            File.Copy("Localizable.strings", iOSFileName);
        }

        [Test]
        public void Ctor_WithFile_CorrectlyInitialized()
        {
            var sut = new iOSResourceFileHandler(iOSFileName);

            Assert.That(sut.Keys.Count, Is.EqualTo(2));
        }

        [Test]
        public void AddValue_Save_FileUpdated()
        {
            var sut = new iOSResourceFileHandler(iOSFileName);

            sut["anotherkey"] = "test";
            sut.Save(false);

            sut = new iOSResourceFileHandler(iOSFileName);
            Assert.That(sut.Keys.Count, Is.EqualTo(3));
        }

        [Test]
        public void AddValue_Save_FileUpdated_Escaped()
        {
            var sut = new iOSResourceFileHandler(iOSFileName);

            sut["anotherkey"] = "< > & ¢ £ ¥ € © ®";
            sut.Save(false);

            sut = new iOSResourceFileHandler(iOSFileName);
            Assert.That(sut.Keys.Count, Is.EqualTo(3));
        }
    }
}
