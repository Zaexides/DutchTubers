using System.Text.Json.Serialization;

namespace DutchTubers.Server.Models
{
    public class StreamInfoDTO
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("game")]
        public string? Game { get; set; }
    }
}
