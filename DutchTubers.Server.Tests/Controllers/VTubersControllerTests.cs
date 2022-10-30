using System;
using System.Linq;
using System.Threading.Tasks;
using DutchTubers.Server.Models;
using DutchTubers.Server.Services;
using DutchTubers.Server.Utils;
using Moq;
using NUnit.Framework;

namespace DutchTubers.Server.Controllers
{
    [TestFixture]
    public class VTubersControllerTests
    {
        private Mock<ITwitchService> _twitchServiceMock;
        private Mock<IRandomProvider> _randomProviderMock;
        private VTubersController _fixture;

        [SetUp]
        public void SetUp()
        {
            _twitchServiceMock = new Mock<ITwitchService>();
            _randomProviderMock = new Mock<IRandomProvider>();
            _fixture = new VTubersController(_twitchServiceMock.Object, _randomProviderMock.Object);
        }

        [Test]
        public async Task Get()
        {
            var vtubers = new VTuberDTO[]
            {
                new VTuberDTO() { Username = "0" },
                new VTuberDTO() { Username = "1" },
                new VTuberDTO() { Username = "2" },
                new VTuberDTO() { Username = "3", StreamInfo = new StreamInfoDTO() },
                new VTuberDTO() { Username = "4" },
                new VTuberDTO() { Username = "5" },
                new VTuberDTO() { Username = "6", StreamInfo = new StreamInfoDTO() },
                new VTuberDTO() { Username = "7" },
                new VTuberDTO() { Username = "8" },
                new VTuberDTO() { Username = "9", StreamInfo = new StreamInfoDTO() }
            };
            _twitchServiceMock.Setup(ts => ts.GetVTubersAsync()).ReturnsAsync(() => vtubers);
            _randomProviderMock.Setup(rp => rp.Next()).Returns(new Random(125).Next());

            var obtained = await _fixture.Get();
            var expected = vtubers
                .OrderBy((_) => new Random(125).Next())
                .ThenBy((vt) => vt.StreamInfo == null);
            CollectionAssert.AreEqual(expected, obtained);
            Assert.IsNotNull(obtained.First().StreamInfo);
            Assert.IsNull(obtained.Last().StreamInfo);
        }

        [Test]
        public void GetCache()
        {
            _twitchServiceMock.Setup(ts => ts.GetCacheMeta()).Returns(() => null);
            var obtained = _fixture.GetCache();
            Assert.IsNotNull(obtained);
            Assert.AreEqual(Guid.Empty, obtained.Id);
            Assert.IsTrue(obtained.IsOutdated);

            var cacheMeta = new MockCacheMeta()
            {
                Id = Guid.NewGuid(),
                IsOutdated = false
            };
            _twitchServiceMock.Setup(ts => ts.GetCacheMeta()).Returns(cacheMeta);
            obtained = _fixture.GetCache();
            Assert.IsNotNull(obtained);
            Assert.AreEqual(cacheMeta.Id, obtained.Id);
            Assert.IsFalse(obtained.IsOutdated);
        }

        private class MockCacheMeta : ICacheMeta
        {
            public Guid Id { get; set; }

            public bool IsOutdated { get; set; }
        }
    }
}
