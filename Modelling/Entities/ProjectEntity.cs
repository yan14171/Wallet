using Projects.Modelling.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Modelling.Entities
{
	public class ProjectEntity
	{
        public ProjectEntity()
        {

        }
        public ProjectEntity(Project projectModel, UserEntity authorEntity, TeamEntity teamEntity, IEnumerable<TaskEntity> taskEntities)
        {
			Id = projectModel.Id;
			Author = authorEntity;
			Team = teamEntity;
			Tasks = taskEntities;
			Name = projectModel.Name;
			Description = projectModel.Description;
			Deadline = projectModel.Deadline;
			CreatedAt = projectModel.CreatedAt;
        }

		public int Id { get; set; }

		public UserEntity Author { get; set; }

		public TeamEntity Team { get; set; }

		public IEnumerable<TaskEntity> Tasks { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime Deadline { get; set; }

		public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
			var tasksInfo = string.Empty;

            foreach (var item in Tasks)
            {
				tasksInfo += item.ToString();
            }

			return $"Id : {Id}|\n" +
				$" Author : {Author.ToString()}|\n " +
				$"Team : {Team.ToString()}|\n " +
				$"Tasks : {tasksInfo}|\n" +
				$" Name : {Name}|\n " +
				$"Description : {Description}|\n " +
				$"DeadLine : {Deadline}|\n Created At : {CreatedAt}|" +
				$"/n"; 
			}
        }
    }

