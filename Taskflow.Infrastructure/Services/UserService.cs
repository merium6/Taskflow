using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;
using TaskFlow.Core.Services.Interfaces;

namespace TaskFlow.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task DeleteAsync(int id) => await _userRepository.DeleteAsync(id);

        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepository.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id) => await _userRepository.GetByIdAsync(id);

        public async Task UpdateAsync(User user) => await _userRepository.UpdateAsync(user);
    }
}
