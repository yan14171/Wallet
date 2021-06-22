using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ProjectsAccess.Models
{ 
	public class Project
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("authorId")]
		public int AuthorId { get; set; }

		[JsonPropertyName("teamId")]
		public int TeamId { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("deadline")]
		public DateTime Deadline { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }
	}
}
