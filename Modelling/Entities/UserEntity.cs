using ProjectsAccess.Models;
using System;

namespace Projects.Modelling.Entities
{
	public class UserEntity
	{
        public UserEntity()
        {

        }
        public UserEntity(User userModel)
        {
			Id = userModel.Id;
			TeamId = userModel.TeamId;
			FirstName = userModel.FirstName;
			LastName = userModel.LastName;
			Email = userModel.Email;
			RegisteredAt = userModel.RegisteredAt;
			BirthDay = userModel.BirthDay;
		}

		public int Id { get; set; }

		public int? TeamId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public DateTime RegisteredAt { get; set; }

		public DateTime BirthDay { get; set; }
	}
}
