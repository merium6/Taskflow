using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Core.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Relationships
        public int CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
        public ICollection<TaskItem>? Tasks { get; set; }
    }
}

