using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task AddAsync(ApplicationUser user, string password);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(string id);
    }
}

