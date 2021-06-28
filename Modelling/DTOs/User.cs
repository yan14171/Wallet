using System;
using System.Text.Json.Serialization;

namespace Projects.Modelling.DTOs
{
	public class User
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("teamId")]
		public int? TeamId { get; set; }

		[JsonPropertyName("firstName")]
		public string FirstName { get; set; }

		[JsonPropertyName("lastName")]
		public string LastName { get; set; }

		[JsonPropertyName("email")]
		public string Email { get; set; }

		[JsonPropertyName("registeredAt")]
		public DateTime RegisteredAt { get; set; }

		[JsonPropertyName("birthDay")]
		public DateTime BirthDay { get; set; }
	}
}
