﻿using TaskFlow.Core.Entities;
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

        public async Task<ApplicationUser> CreateAsync(ApplicationUser user)
        {
            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
