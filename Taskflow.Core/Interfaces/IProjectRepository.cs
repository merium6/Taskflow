using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(int id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int id);
    }
}

