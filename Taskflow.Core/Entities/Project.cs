using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Taskflow.Core.Enums;

namespace TaskFlow.Core.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.Planned;

        public string? CreatedById { get; set; }
        public ApplicationUser? CreatedBy { get; set; }

        public ICollection<TaskItem>? Tasks { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

  
}
