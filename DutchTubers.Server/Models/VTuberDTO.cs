using System.Text.Json.Serialization;

namespace DutchTubers.Server.Models
{
    public class VTuberDTO
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("profileImg")]
        public string ProfileImage { get; set; }

        [JsonPropertyName("streamInfo")]
        public StreamInfoDTO? StreamInfo { get; set; }
    }
}
