using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
namespace TaskFlow.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Basic Profile
        public string FullName { get; set; } = string.Empty;
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public string Password { get; set; } = string.Empty;

        // Extended Info
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public string? ProfilePictureUrl { get; set; }

        // Navigation Properties (relationships)
        public ICollection<Project>? ProjectsCreated { get; set; }
        public ICollection<TaskItem>? TasksAssigned { get; set; }
    }
}
