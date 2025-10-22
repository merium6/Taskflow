using Taskflow.API.Core.Interfaces;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;
using TaskFlow.Core.Services.Interfaces;

namespace TaskFlow.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.CompleteAsync();
            return task;
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task != null)
            {
                _unitOfWork.Tasks.Remove(task);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _unitOfWork.Tasks.GetAllAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Tasks.GetByIdAsync(id);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByProjectAsync(int projectId)
        {
            var allTasks = await _unitOfWork.Tasks.GetAllAsync();
            return allTasks.Where(t => t.ProjectId == projectId);
        }
    }
}
