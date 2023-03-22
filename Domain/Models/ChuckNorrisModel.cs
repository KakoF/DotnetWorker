
using Newtonsoft.Json;

namespace Domain.Models
{
    public class ChuckNorrisModel
    {
        public List<object> Categories { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }
        public string Id { get; set; }
        [JsonProperty("update_at")]
        public string UpdatedAt { get; set; }
        public string Url { get; set; }
        public string Value { get; set; }
    }
}
