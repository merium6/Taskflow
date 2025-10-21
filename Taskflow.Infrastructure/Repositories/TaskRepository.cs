using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
            await _context.TaskItems.ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(int id) =>
            await _context.TaskItems.FindAsync(id);

        public async Task UpdateAsync(TaskItem taskItem)
        {
            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync();
        }
    }
}

