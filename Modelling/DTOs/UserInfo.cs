using Projects.Modelling.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.DTOs
{
    public class UserInfo
    {
        public UserEntity User { get; set; }

        public ProjectEntity LastProject { get; set; }

        public int LastProjectTasksQuantity { get; set; }

        public int UnhandledTasksQuantity { get; set; }

        public TaskEntity LongetsTask { get; set; }
    }
}
