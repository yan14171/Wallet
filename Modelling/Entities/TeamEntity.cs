using Projects.Modelling.DTOs;
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

        public override string ToString()
        {
			var users = string.Empty;

			foreach (var item in users)
			{
				users += item.ToString();
			}

			return $"Id : {Id}|\n" +
				$"Users : {users}|\n" +
				$" Name : {Name}|\n " +
				$"Created At : {CreatedAt}|" +
				$"/n";
		}
	}
}

