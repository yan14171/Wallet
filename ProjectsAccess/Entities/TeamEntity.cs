using ProjectsAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace ProjectsAccess.Entities
{
	public class TeamEntity
	{
        public TeamEntity()
        {

        }
        public TeamEntity(Team teamModel, IEnumerable<UserEntity> userEntities)
        {
			Id = teamModel.Id;
			Name = teamModel.Name;
			Users = userEntities;
			CreatedAt = teamModel.CreatedAt;
        }

		public int Id { get; set; }

		public string Name { get; set; }

		public IEnumerable<UserEntity> Users { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
