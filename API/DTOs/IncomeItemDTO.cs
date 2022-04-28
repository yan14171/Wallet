using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class IncomeItemDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("account_id")]
        public int AccountId { get; set; }
    }
}
