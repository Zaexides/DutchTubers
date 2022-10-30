using System.Text.Json.Serialization;

namespace DutchTubers.Server.Models
{
    public class CacheMetaDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("isOutdated")]
        public bool IsOutdated { get; set; }
    }
}
