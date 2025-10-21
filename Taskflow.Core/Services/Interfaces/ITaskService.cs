using TaskFlow.Core.Entities;

namespace TaskFlow.Core.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(int id);
        Task<IEnumerable<TaskItem>> GetTasksByProjectAsync(int projectId);
    }
}
