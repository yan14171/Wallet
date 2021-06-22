using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ProjectsAccess.Models
{
	public class Task
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("projectId")]
		public int ProjectId { get; set; }

		[JsonPropertyName("performerId")]
		public int PerformerId { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("state")]
		public State State { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		[JsonPropertyName("finishedAt")]
		public DateTime? FinishedAt { get; set; }
	}

	public enum State
    {
		Unbegun,
		InProgress,
		Finished,
		Canceled,
    }
}
