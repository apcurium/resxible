using System;
using Com.Apcurium.Resxible.Localization;
using NUnit.Framework;

namespace Com.Apcurium.Resxible.Tests
{
    [TestFixture]
    public class ResourceManagerFixture
    {
        private ResourceManager _sut;

        [SetUp]
        public void PrepareSourceAndDestination()
        {
            _sut = new ResourceManager();

        }

        [Test]
        public void when_adding_null_source()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.AddSource(default(ResourceFileHandlerBase)));
        }

        [Test]
        public void when_adding_null_destination()
        {
            Assert.Throws<ArgumentNullException>(() => _sut.AddDestination(default(ResourceFileHandlerBase)));
        }

        [Test]
        public void Update_WithOneSource_DestinationFilled()
        {
            var source = new DummyResourceFileHandlerBase("Source.txt");
            source["key"] = "value";
            _sut.AddSource(source);
            var destination = new DummyResourceFileHandlerBase("Destination.txt");
            _sut.AddDestination(destination);

            _sut.Update();

            Assert.That(destination.Keys.Count, Is.EqualTo(source.Keys.Count));
        }

        [Test]
        public void Update_WithSeveralSources_DestinationFilled()
        {
            var source = new DummyResourceFileHandlerBase("Source.txt");
            source["key"] = "value";
            _sut.AddSource(source);
            source = new DummyResourceFileHandlerBase("Source2.txt");
            source["key2"] = "value2";
            _sut.AddSource(source);
            var destination = new DummyResourceFileHandlerBase("Destination.txt");
            _sut.AddDestination(destination);

            _sut.Update();

            Assert.That(destination.Keys.Count, Is.EqualTo(2));
        }

        [Test]
        public void Update_WithSeveralSourcesAndSameKeys_DestinationFilled()
        {
            var source = new DummyResourceFileHandlerBase("Source.txt");
            source["key"] = "value";
            _sut.AddSource(source);
            source = new DummyResourceFileHandlerBase("Source2.txt");
            source["key"] = "value2";
            _sut.AddSource(source);
            var destination = new DummyResourceFileHandlerBase("Destination.txt");
            _sut.AddDestination(destination);

            _sut.Update();

            Assert.That(destination.Keys.Count, Is.EqualTo(1));
            Assert.That(destination["key"], Is.EqualTo("value2"));
        }

        [Test]
        public void Update_WithOneSource_SeveralDestinationFilled()
        {
            var source = new DummyResourceFileHandlerBase("Source.txt");
            source["key"] = "value";
            _sut.AddSource(source);
            var destination = new DummyResourceFileHandlerBase("Destination.txt");
            _sut.AddDestination(destination);
            var destination2 = new DummyResourceFileHandlerBase("Destination2.txt");
            _sut.AddDestination(destination2);

            _sut.Update();

            Assert.That(destination.Keys.Count, Is.EqualTo(source.Keys.Count));
            Assert.That(destination2.Keys.Count, Is.EqualTo(source.Keys.Count));
        }
    }
}
