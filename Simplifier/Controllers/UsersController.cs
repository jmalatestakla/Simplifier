using Microsoft.AspNetCore.Mvc;
using Simplifier.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Simplifier.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly SimplifierContext _context;


        public UsersController(ILogger<UsersController> logger, SimplifierContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            _logger.LogInformation("getting users");
            var Users = _context.Users.ToList();
            _logger.LogInformation("got users {Users}", Users);
            return Users;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _logger.LogInformation("hitting this {user} ", user);
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "successfully added new user" });
        }

        [HttpDelete("{uuid}")]
        public IActionResult Delete(Guid uuid)
        {
            _logger.LogInformation("deleting user with uuid {uuid}", uuid);
            var user = _context.Users.FirstOrDefault(u => u.Uuid == uuid);
            if (user == null)
            {
                _logger.LogWarning("user with uuid {uuid} not found", uuid);
                return NotFound(new { message = "user not found" });
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            _logger.LogInformation("deleted user with uuid {uuid}", uuid);

            return Ok(new { message = "successfully deleted user" });
        }

        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            _logger.LogInformation("updating user with uuid {uuid}", user.Uuid);
            var existingUser = _context.Users.FirstOrDefault(u => u.Uuid == user.Uuid);
            if (existingUser == null)
            {
                _logger.LogWarning("user with uuid {uuid} not found", user.Uuid);
                return NotFound(new { message = "user not found" });
            }

            existingUser.Email = user.Email;
            _context.SaveChanges();
            _logger.LogInformation("updated user with uuid {uuid}", user.Uuid);

            return Ok(new { message = "successfully updated user email" });
        }
    }
}
