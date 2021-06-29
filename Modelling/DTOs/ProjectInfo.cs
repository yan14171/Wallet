using Projects.Modelling.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projects.Modelling.DTOs
{
    public class ProjectInfo
    {
        [JsonPropertyName("project")]
        public ProjectEntity Project { get; set; }

        [JsonPropertyName("longestTask")]
        public TaskEntity LongestTask { get; set; }

        [JsonPropertyName("shortestTask")]
        public TaskEntity ShortestTask { get; set; }

        [JsonPropertyName("usersQuantity")]
        public int UsersQuantity { get; set; }
    }
}
