using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Interfaces;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync() =>
        await _userManager.Users.ToListAsync();

    public async Task<ApplicationUser?> GetByIdAsync(string id) =>
        await _userManager.FindByIdAsync(id);

    public async Task AddAsync(ApplicationUser user)
    {
        await _userManager.CreateAsync(user, user.Password);
    }

    public async Task DeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
            await _userManager.DeleteAsync(user);
    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        await _userManager.UpdateAsync(user);
    }
}
