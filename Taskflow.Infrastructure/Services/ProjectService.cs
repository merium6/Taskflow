using TaskFlow.Core.Entities;
using Taskflow.API.Core.Interfaces;
using TaskFlow.Core.Services.Interfaces;

namespace TaskFlow.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.CompleteAsync();
            return project;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _unitOfWork.Projects.GetAllAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Projects.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Project project)
        {
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project != null)
            {
                _unitOfWork.Projects.Remove(project);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
