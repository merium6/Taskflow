using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskflow.Core.Enums;

namespace TaskFlow.Core.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TasksStatus Status { get; set; } = TasksStatus.ToDo;
        public DateTime? DueDate { get; set; }

        // Relationships
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
    }
}

