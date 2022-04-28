using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class TransactionDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("sum")]
        [Required]
        public decimal Sum { get; set; }

        [JsonPropertyName("transaction_type")]
        public string? TransactionType { get; set; }

        [JsonPropertyName("income_item_id")]
        [Required]
        public int IncomeItemId { get; set; }

        [JsonPropertyName("account_id")]
        [Required]
        public int AccountId { get; set; }
    }
}
