using Projects.Modelling.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Projects.Modelling.DTOs
{
    public class OldestUsersInfo : DTOBase
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("users")]
        public IEnumerable<UserEntity> Users { get; set; }
        
    }
}
