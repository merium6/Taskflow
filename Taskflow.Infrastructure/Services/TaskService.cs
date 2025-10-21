using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;
using TaskFlow.Core.Services.Interfaces;

namespace TaskFlow.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            await _taskRepository.AddAsync(task);
            return task;
        }

        public async Task DeleteAsync(int id) => await _taskRepository.DeleteAsync(id);

        public async Task<IEnumerable<TaskItem>> GetAllAsync() => await _taskRepository.GetAllAsync();

        public async Task<TaskItem?> GetByIdAsync(int id) => await _taskRepository.GetByIdAsync(id);

        public async Task UpdateAsync(TaskItem task) => await _taskRepository.UpdateAsync(task);

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectAsync(int projectId)
        {
            var allTasks = await _taskRepository.GetAllAsync();
            return allTasks.Where(t => t.ProjectId == projectId);
        }
    }
}
