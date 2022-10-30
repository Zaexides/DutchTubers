using System;
using DutchTubers.Server.Services;
using Moq;
using NUnit.Framework;

namespace DutchTubers.Server.Utils
{
    [TestFixture]
    public class CachedDataTests
    {
        private TimeSpan _lifetime;
        private TestData _data;
        private Mock<IDateTimeProvider> _dateTimeProviderMock;
        private CachedData<TestData> _fixture;

        [SetUp]
        public void SetUp()
        {
            _lifetime = new TimeSpan(hours: 2, minutes: 0, seconds: 0);
            _data = new TestData() {Value = 10};
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _dateTimeProviderMock.Setup(dtp => dtp.Now).Returns(DateTime.UtcNow);
            _fixture = new CachedData<TestData>(_lifetime, _data, _dateTimeProviderMock.Object);
        }

        [Test]
        public void DataElapsedLifespan()
        {
            _dateTimeProviderMock.Setup(dtp => dtp.Now).Returns(new DateTime(year: 1, month: 1, day: 1, hour: 2, minute: 0, second: 0));
            _fixture.Store(_data);
            Assert.IsFalse(_fixture.DataElapsedLifespan);
            _dateTimeProviderMock.Setup(dtp => dtp.Now).Returns(new DateTime(year: 1, month: 1, day: 1, hour: 0, minute: 0, second: 0));
            Assert.IsFalse(_fixture.DataElapsedLifespan);
            _dateTimeProviderMock.Setup(dtp => dtp.Now).Returns(new DateTime(year: 1, month: 1, day: 1, hour: 4, minute: 0, second: 0));
            Assert.IsTrue(_fixture.DataElapsedLifespan);
        }

        [Test]
        public void Store_Retrieve()
        {
            var obtained = _fixture.Retrieve();
            Assert.AreEqual(_data, obtained);
            var expected = new TestData() {Value = 20};
            _fixture.Store(expected);
            obtained = _fixture.Retrieve();
            Assert.AreEqual(expected, obtained);
        }

        [Test]
        public void GetMeta()
        {
            _dateTimeProviderMock.Setup(dtp => dtp.Now).Returns(new DateTime(year: 1, month: 1, day: 1, hour: 2, minute: 0, second: 0));
            _fixture.Store(new TestData());
            var meta1 = _fixture.GetMeta();
            var meta2 = _fixture.GetMeta();

            Assert.AreEqual(meta1.Id, meta2.Id);
            Assert.IsFalse(meta1.IsOutdated);
            Assert.IsFalse(meta2.IsOutdated);

            _dateTimeProviderMock.Setup(dtp => dtp.Now).Returns(new DateTime(year: 1, month: 1, day: 1, hour: 4, minute: 0, second: 0));
            meta2 = _fixture.GetMeta();
            Assert.AreEqual(meta1.Id, meta2.Id);
            Assert.IsFalse(meta1.IsOutdated);
            Assert.IsTrue(meta2.IsOutdated);

            _fixture.Store(new TestData());
            meta2 = _fixture.GetMeta();
            Assert.AreNotEqual(meta1.Id, meta2.Id);
            Assert.IsFalse(meta2.IsOutdated);
        }

        private class TestData
        {
            public int Value { get; set; }
        }
    }
}
