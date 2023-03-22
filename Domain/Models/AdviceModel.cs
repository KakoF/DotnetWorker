using Newtonsoft.Json;

namespace Domain.Models
{
    public class AdviceModel
    {
        [JsonProperty("slip")]
        public SlipModel Slip { get; set; }
    }

    public class SlipModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("advice")]
        public string Advice { get; set; }
    }
}
