using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Taskflow.API.DTOs.Auth;
using TaskFlow.API.Models.Auth;
using TaskFlow.Core.Entities;
using TaskFlow.Infrastructure.Data;
using TaskFlow.Infrastructure.Services;

namespace TaskFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtService _jwtService;
        private readonly AppDbContext _context;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            JwtService jwtService,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return BadRequest("User with this email already exists.");

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var role = "User";

            if (model.Email.Equals("admin@taskflow.com", StringComparison.OrdinalIgnoreCase))
                role = "Admin";

            await _userManager.AddToRoleAsync(user, role);

            return Ok(new
            {
                message = "User registration successful",
                user = new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    Role = role
                }
            });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken(HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown");

            refreshToken.ApplicationUserId = user.Id;
            using (var scope = HttpContext.RequestServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.RefreshTokens.Add(refreshToken);
                await context.SaveChangesAsync();
            }

            return Ok(new
            {
                accessToken,
                refreshToken = refreshToken.Token,
                expiresIn = 3600,
                email = user.Email,
                roles
            });
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto request)
        {
            var storedToken = await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);

            if (storedToken == null || storedToken.IsExpired)
                return Unauthorized("Invalid or expired refresh token.");

            var roles = await _userManager.GetRolesAsync(storedToken.User!);
            var newAccessToken = _jwtService.GenerateAccessToken(storedToken.User!, roles);
            var newRefreshToken = _jwtService.GenerateRefreshToken(HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown");

            storedToken.Revoked = DateTime.UtcNow;
            storedToken.ReplacedByToken = newRefreshToken.Token;

            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken.Token
            });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var email = User.Identity?.Name ?? "unknown";
            return Ok(new { message = $"Welcome {email}, you are authenticated!" });
        }
    }

   
    
}
