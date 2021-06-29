using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projects.Modelling.DTOs
{
    public class TaskInfo : DTOBase
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
