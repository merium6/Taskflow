using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Core.Entities;
using TaskFlow.Core.Services.Interfaces;

namespace Taskflow.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize] 
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ApplicationUser user)
        {
            var newUser = await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Update(string id, [FromBody] ApplicationUser user)
        {
            if (id != user.Id)
                return BadRequest();

            await _userService.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
