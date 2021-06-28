using System;
using System.Text.Json.Serialization;

namespace Projects.Modelling.DTOs
{
	public class Team : DTOBase
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }
	}
}
