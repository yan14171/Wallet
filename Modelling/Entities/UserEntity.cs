using Projects.Modelling.DTOs;
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

        public override string ToString()
        {
			return $"Id : {Id}|\n" +
				$" First Name : {FirstName}|\n " +
				$"Last Name : {LastName}|\n " +
				$"Email: {Email}|\n" +
				$"Registered At : {RegisteredAt}|\n " +
				$"Birth Day : {BirthDay}|\n ";
		}
	}
}

