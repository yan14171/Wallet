using System;

namespace Projects.Modelling.Entities
{
	public class TaskEntity
	{
        public TaskEntity()
        {

        }
        public TaskEntity(Task taskModel, UserEntity performerEntity)
        {
			Id = taskModel.Id;
			ProjectId = taskModel.ProjectId;
			Performer = performerEntity;
			Name = taskModel.Name;
			Description = taskModel.Description;
			State = taskModel.State;
			CreatedAt = taskModel.CreatedAt;
			FinishedAt = taskModel.FinishedAt;
        }
		public int Id { get; set; }

		public int ProjectId { get; set; }

		public UserEntity Performer { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public State State { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? FinishedAt { get; set; }
	}
}
