using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class AccountDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("fortune")]
        public decimal Fortune { get; set; }
    }
}
