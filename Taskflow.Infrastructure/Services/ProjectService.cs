using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;
using TaskFlow.Core.Services.Interfaces;

namespace TaskFlow.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            await _projectRepository.AddAsync(project);
            return project;
        }

        public async Task DeleteAsync(int id) => await _projectRepository.DeleteAsync(id);

        public async Task<IEnumerable<Project>> GetAllAsync() => await _projectRepository.GetAllAsync();

        public async Task<Project?> GetByIdAsync(int id) => await _projectRepository.GetByIdAsync(id);

        public async Task UpdateAsync(Project project) => await _projectRepository.UpdateAsync(project);
    }
}
