using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchTubers.Server.Utils;
using Moq;
using NUnit.Framework;

namespace DutchTubers.Server.Services
{
    [TestFixture]
    public class CacheProviderTests
    {
        private Mock<IDateTimeProvider> _mockDateTimeProvider;
        private CacheProvider _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _fixture = new CacheProvider();

            _mockDateTimeProvider.Setup(dtp => dtp.Now).Returns(() => DateTime.UtcNow);
        }

        [Test]
        public void Store_DataIsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _fixture.Store<TestData1>(null));
            Assert.AreEqual("cachedData", ex.ParamName);
        }

        [Test]
        public void Store_TryGetCachedDataFor()
        {
            var td1 = new CachedData<TestData1>(new TimeSpan(1, 0, 0, 0), new TestData1(), _mockDateTimeProvider.Object);
            var td2 = new CachedData<TestData2>(new TimeSpan(1, 0, 0, 0), new TestData2(), _mockDateTimeProvider.Object);
            _fixture.Store(td1);
            _fixture.Store(td2);

            var b1 = _fixture.TryGetCachedDataFor<TestData1>(out var obtained1);
            Assert.IsTrue(b1);
            Assert.IsNotNull(obtained1);
            Assert.AreEqual(td1, obtained1);

            var b2 = _fixture.TryGetCachedDataFor<TestData2>(out var obtained2);
            Assert.IsTrue(b2);
            Assert.IsNotNull(obtained2);
            Assert.AreEqual(td2, obtained2);
        }

        [Test]
        public void TryGetCachedDataFor_NoDataStored()
        {
            var b = _fixture.TryGetCachedDataFor<TestData1>(out var obtained);
            Assert.IsFalse(b);
            Assert.IsNull(obtained);
        }

        private class TestData1
        {
        }

        private class TestData2
        {
        }
    }
}
