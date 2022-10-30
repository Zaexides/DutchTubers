using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;

namespace DutchTubers.Server.Services
{
    [TestFixture]
    public class SecretProviderTests
    {
        private const string ClientIDKey = "TTV_CLIENT_ID";
        private const string SecretKey = "TTV_SECRET";

        private const string FakeUserClientID = "ucidtest1234567890";
        private const string FakeUserSecret = "usecrettest1234567890";
        private const string FakeProcessClientID = "pcidtest1234567890";
        private const string FakeProcessSecret = "psecrettest1234567890";

        private Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private string? _originalUserClientID;
        private string? _originalUserSecret;
        private string? _originalProcessClientID;
        private string? _originalProcessSecret;

        [SetUp]
        public void SetUp()
        {
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _originalUserClientID = Environment.GetEnvironmentVariable(ClientIDKey, EnvironmentVariableTarget.User);
            _originalUserSecret = Environment.GetEnvironmentVariable(SecretKey, EnvironmentVariableTarget.User);
            _originalProcessClientID = Environment.GetEnvironmentVariable(ClientIDKey, EnvironmentVariableTarget.Process);
            _originalProcessSecret = Environment.GetEnvironmentVariable(SecretKey, EnvironmentVariableTarget.Process);

            Environment.SetEnvironmentVariable(ClientIDKey, FakeUserClientID, EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable(SecretKey, FakeUserSecret, EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable(ClientIDKey, FakeProcessClientID, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(SecretKey, FakeProcessSecret, EnvironmentVariableTarget.Process);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Environment.SetEnvironmentVariable(ClientIDKey, _originalUserClientID, EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable(SecretKey, _originalUserSecret, EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable(ClientIDKey, _originalProcessClientID, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(SecretKey, _originalProcessSecret, EnvironmentVariableTarget.Process);
        }

        [Test]
        public void GetTwitchClientID_Development()
        {
            _webHostEnvironmentMock.Setup(whe => whe.EnvironmentName).Returns("Development");
            Assert.IsTrue(_webHostEnvironmentMock.Object.IsDevelopment());
            var fixture = new SecretProvider(_webHostEnvironmentMock.Object);
            var obtained = fixture.GetTwitchClientID();
            Assert.AreEqual(FakeUserClientID, obtained);
        }

        [Test]
        public void GetTwitchClientID_Production()
        {
            _webHostEnvironmentMock.Setup(whe => whe.EnvironmentName).Returns("Production");
            Assert.IsFalse(_webHostEnvironmentMock.Object.IsDevelopment());
            var fixture = new SecretProvider(_webHostEnvironmentMock.Object);
            var obtained = fixture.GetTwitchClientID();
            Assert.AreEqual(FakeProcessClientID, obtained);
        }

        [Test]
        public void GetTwitchSecret_Development()
        {
            _webHostEnvironmentMock.Setup(whe => whe.EnvironmentName).Returns("Development");
            Assert.IsTrue(_webHostEnvironmentMock.Object.IsDevelopment());
            var fixture = new SecretProvider(_webHostEnvironmentMock.Object);
            var obtained = fixture.GetTwitchSecret();
            Assert.AreEqual(FakeUserSecret, obtained);
        }

        [Test]
        public void GetTwitchSecret_Production()
        {
            _webHostEnvironmentMock.Setup(whe => whe.EnvironmentName).Returns("Production");
            Assert.IsFalse(_webHostEnvironmentMock.Object.IsDevelopment());
            var fixture = new SecretProvider(_webHostEnvironmentMock.Object);
            var obtained = fixture.GetTwitchSecret();
            Assert.AreEqual(FakeProcessSecret, obtained);
        }
    }
}
