using ProjectsAccess.Models;
using System;
using System.Collections.Generic;

namespace Projects.Modelling.Entities
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
