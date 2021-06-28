using System;
using System.Text.Json.Serialization;

namespace Projects.Modelling.DTOs
{
	public class Team
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }
	}
}
