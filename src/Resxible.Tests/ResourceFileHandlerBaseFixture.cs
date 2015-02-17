using System;
using System.IO;
using System.Linq;
using Amp.LocalizationTool.Tests;
using NUnit.Framework;

namespace Com.Apcurium.Resxible.Tests
{
    [TestFixture]
    public class ResourceFileHandlerBaseFixture
    {
        private readonly string _myfileTxt = string.Format("MyFile-{0}.txt", DateTime.Now.Ticks);
        private const string SourceFile = "Original.txt";

        [SetUp]
        public void Setup()
        {
            foreach (var file in Directory.GetFiles(".", "*.txt"))
            {
                File.Delete(file);
            }
            using (var file = File.CreateText(SourceFile))
            {
                file.WriteLine("onevalue");
                file.Close();
            }
        }

        [Test]
        public void Ctor_NoFile_FileCreated()
        {
            var sut = new DummyResourceFileHandlerBase(_myfileTxt);

            Assert.That(File.Exists(_myfileTxt));
        }

        [Test]
        public void Name_WithFile_ReturnName()
        {
            var sut = new DummyResourceFileHandlerBase(_myfileTxt);

            var name = sut.Name;

            Assert.That(name, Is.EqualTo(_myfileTxt));
        }

        [Test]
        public void Save_OldFile_ShouldKeepContent()
        {
            var sut = new DummyResourceFileHandlerBase(SourceFile);

            sut["secondline"] = "value";
            var backup = sut.Save(true);
            
            var lines = File.ReadLines(backup);
            Assert.That(lines.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SaveTwice_OldFile_NoDuplicateError()
        {
            var sut = new DummyResourceFileHandlerBase(SourceFile);

            sut["secondline"] = "value";
            sut.Save(true);
            sut.Save(true);

            Assert.Pass();
        }

        [Test]
        public void Save_NewFile_ShouldHaveNewContent()
        {
            var sut = new DummyResourceFileHandlerBase(SourceFile);

            sut["secondline"] = "value";
            sut.Save(true);

            var lines = File.ReadLines(SourceFile);
            Assert.That(lines.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Save_WihtoutBackup_NoValueReturned()
        {
            var sut = new DummyResourceFileHandlerBase(SourceFile);

            sut["secondline"] = "value";
            var backup = sut.Save(false);

            Assert.IsNull(backup);
        }


    }
}