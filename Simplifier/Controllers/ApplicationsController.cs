using Microsoft.AspNetCore.Mvc;
using Simplifier.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Simplifier.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private static readonly List<Application> Applications = new List<Application>
        {
            new Application
            {
                Uuid = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Personal Loan",
                RawText = "This is a personal loan application.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TemplateId = Guid.NewGuid(),
                FormResponses = "{\"question1\":\"answer1\",\"question2\":\"answer2\"}",
            },
            new Application
            {
                Uuid = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Business Loan",
                RawText = "This is a business loan application.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TemplateId = Guid.NewGuid(),
                FormResponses = "{\"question1\":\"answer1\",\"question2\":\"answer2\"}",
            }
        };
        private readonly ILogger<ApplicationsController> _logger;
        private readonly SimplifierContext _context;


        public ApplicationsController(ILogger<ApplicationsController> logger, SimplifierContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Application> Get()
        {
            var user = new User
            {
                Uuid = Guid.NewGuid(),
                Email = "joe@gmail.com"
            };
            _logger.LogInformation("adding user {user.Email}, {user.Uuid}", user.Email, user.Uuid);
            var tableNames = _context.Users.Add(user);
            _context.SaveChanges();    
            return Applications;
        }

        [HttpPost]
        public IActionResult Post([FromBody] string randomString)
        {
            _logger.LogInformation("hitting this {randomString} ", randomString);
            // var newApplication = new Application
            // {
            //     Uuid = Guid.NewGuid(),
            //     UserId = Guid.NewGuid(),
            //     Name = "Personal Loan",
            //     RawText = "This is a personal loan application.",
            //     CreatedAt = DateTime.UtcNow,
            //     UpdatedAt = DateTime.UtcNow,
            //     TemplateId = Guid.NewGuid(),
            //     FormResponses = "{\"question1\":\"answer1\",\"question2\":\"answer2\"}",
            // };

            // _context.Applications.Add(newApplication);
            // _context.SaveChanges();

            return Ok(new { message = randomString });}
    }
}
