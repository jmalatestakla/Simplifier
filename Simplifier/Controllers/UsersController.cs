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
    }
}
