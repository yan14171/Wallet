using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Projects.Modelling.DTOs
{
    public class DTOBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

    }
}
