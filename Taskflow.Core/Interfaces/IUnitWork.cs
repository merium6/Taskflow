using TaskFlow.Core.Entities;

namespace Taskflow.API.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Project> Projects { get; }
        IRepository<TaskItem> Tasks { get; }

        Task<int> CompleteAsync();
    }
}
