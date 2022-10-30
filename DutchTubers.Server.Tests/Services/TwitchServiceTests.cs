using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DutchTubers.Server.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using TwitchLib.Api;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using TwitchLib.Api.Interfaces;

namespace DutchTubers.Server.Services
{
    [TestFixture]
    public class TwitchServiceTests
    {
        private static readonly string ClientID = "6536E186-C6A6-4AE6-B156-B92536AEF077";
        private static readonly string Secret = "B982FC05-162F-4D20-B2A4-894D661490A7";

        private static UserModel[] Users = new UserModel[]
        {
            new UserModel() { DisplayName = "Test", Description = "Test Description", Id = "1234", ProfileImageUrl = "http://localhost/img/1234.png" },
            new UserModel() { DisplayName = "Test2", Description = "Another Test Description", Id = "5678", ProfileImageUrl = "http://localhost/img/5678.png" },
        };

        private static StreamModel[] Streams = new StreamModel[]
        {
            new StreamModel() { UserId = "5678", GameName = "Antibody", Title = "Playing this new awesome game!" }
        };

        private Mock<IHttpCallHandler> _twitchAPICallHandlerMock;
        private Mock<ISecretProvider> _secretProviderMock;
        private Mock<IVTuberListProvider> _vtuberListProviderMock;
        private Mock<IDateTimeProvider> _dateTimeProviderMock;
        private TwitchService _fixture;

        [SetUp]
        public void SetUp()
        {
            _twitchAPICallHandlerMock = new Mock<IHttpCallHandler>();
            _secretProviderMock = new Mock<ISecretProvider>();
            _vtuberListProviderMock = new Mock<IVTuberListProvider>();
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();

            _secretProviderMock.Setup(sp => sp.GetTwitchClientID()).Returns(ClientID);
            _secretProviderMock.Setup(sp => sp.GetTwitchSecret()).Returns(Secret);

            _vtuberListProviderMock.Setup(vlp => vlp.GetVTuberList()).Returns(Users.Select(u => u.DisplayName.ToLowerInvariant()).ToList());

            var twitchAPI = new TwitchAPI(http: _twitchAPICallHandlerMock.Object)
            {
                Settings =
                {
                    ClientId = ClientID,
                    Secret = Secret
                }
            };

            SetUpTwitchAPICallsMocks();
            _fixture = new TwitchService(twitchAPI, _secretProviderMock.Object, _vtuberListProviderMock.Object, _dateTimeProviderMock.Object, new CacheProvider());
        }

        [Test]
        public async Task GetVTubersAsync()
        {
            _dateTimeProviderMock.Setup(dtp => dtp.Now).Returns(new DateTime(year: 1, month: 1, day: 1, hour: 2, minute: 0, second: 0));
            var obtained1 = await _fixture.GetVTubersAsync();
            Assert.IsNotNull(obtained1);
            Assert.AreEqual(2, obtained1.Count());
            var user0 = obtained1.SingleOrDefault(u => u.Username == Users[0].DisplayName);
            Assert.IsNotNull(user0);
            Assert.AreEqual(Users[0].Description, user0.Description);
            Assert.AreEqual(Users[0].ProfileImageUrl, user0.ProfileImage);
            var user1 = obtained1.SingleOrDefault(u => u.Username == Users[1].DisplayName);
            Assert.IsNotNull(user1);
            Assert.AreEqual(Users[1].Description, user1.Description);
            Assert.AreEqual(Users[1].ProfileImageUrl, user1.ProfileImage);

            var obtained2 = await _fixture.GetVTubersAsync();
            Assert.AreSame(obtained1, obtained2);

            _dateTimeProviderMock.Setup(dtp => dtp.Now).Returns(new DateTime(year: 1, month: 1, day: 1, hour: 2, minute: 5, second: 0));
            var obtained3 = await _fixture.GetVTubersAsync();
            Assert.AreNotSame(obtained1, obtained3);
        }

        [Test]
        public async Task GetCacheMeta()
        {
            _dateTimeProviderMock.Setup(dtp => dtp.Now).Returns(new DateTime(year: 1, month: 1, day: 1, hour: 2, minute: 5, second: 0));
            var obtained = _fixture.GetCacheMeta();
            Assert.IsNull(obtained);
            await _fixture.GetVTubersAsync();
            obtained = _fixture.GetCacheMeta();
            Assert.IsNotNull(obtained);
        }

        private void SetUpTwitchAPICallsMocks()
        {
            string getUsersResponseJSON = $"{{\"data\":{JsonConvert.SerializeObject(Users)}}}";
            string getStreamsResponseJSON = $"{{\"data\":{JsonConvert.SerializeObject(Streams)}}}";

            var responses = new Dictionary<(string, string), KeyValuePair<int, string>>()
            {
                { ("POST", $"https://id.twitch.tv/oauth2/token?client_id={ClientID}&client_secret={Secret}&grant_type=client_credentials"), new KeyValuePair<int, string>(200, @"{ ""expires_in"": 1000, ""access_token"": ""1234"" }") },
                { ("GET", "https://api.twitch.tv/helix/users?login=test&login=test2"), new KeyValuePair<int, string>(200, getUsersResponseJSON) },
                { ("GET", "https://api.twitch.tv/helix/streams?first=20&type=all&user_login=test&user_login=test2"), new KeyValuePair<int, string>(200, getStreamsResponseJSON) },
            };

            _twitchAPICallHandlerMock.Setup(ch => ch.GeneralRequestAsync(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<ApiVersion>(), It.IsAny<string>(), It.IsAny<string>())).Returns(
                async (string url, string method, string payload, ApiVersion apiVersion, string clientID, string accessToken) =>
                {
                    return responses[(method, url)];
                });
        }

        //Below code is a modified copy of the TwitchLib.Api code for the purpose of making setters accessible for the tests.
        //Y'know, so that I can actually set up tests without painstakingly having to write the JSON myself.

        [DataContract]
        private class UserModel
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "display_name")]
            public string DisplayName { get; set; }

            [DataMember(Name = "description")]
            public string Description { get; set; }

            [DataMember(Name = "profile_image_url")]
            public string ProfileImageUrl { get; set; }
        }

        [DataContract]
        private class StreamModel
        {
            [DataMember(Name = "user_id")]
            public string UserId { get; set; }

            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "game_name")]
            public string GameName { get; set; }
        }
    }
}
