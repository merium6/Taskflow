using Taskflow.API.Core.Interfaces;
using TaskFlow.Core.Entities;
using TaskFlow.Infrastructure.Data;

namespace Taskflow.API.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<User>? _users;
        private IRepository<Project>? _projects;
        private IRepository<TaskItem>? _tasks;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<User> Users => _users ??= new Repository<User>(_context);
        public IRepository<Project> Projects => _projects ??= new Repository<Project>(_context);
        public IRepository<TaskItem> Tasks => _tasks ??= new Repository<TaskItem>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
