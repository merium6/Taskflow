using System;
using System.ComponentModel.DataAnnotations;
using Taskflow.Core.Enums;

namespace TaskFlow.Core.Entities
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public TasksStatus Status { get; set; } = TasksStatus.Pending;

        public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(7);

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public string? AssignedToId { get; set; }
        public ApplicationUser? AssignedTo { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
    }

    

   
}
