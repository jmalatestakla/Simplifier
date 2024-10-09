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
                UserId = Guid.Parse("74939c8b-654e-4e83-a3a2-0403c8afb4a3"),
                Name = "Malatesta Family",
                RawText = "This is a random blob of text from an email or text file.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TemplateId = Guid.NewGuid(),
                FormResponses = "{\"question1\":\"answer1\",\"question2\":\"answer2\"}",
            },
            new Application
            {
                Uuid = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Johnson Family",
                RawText = "Another random application from the johnson family...",
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
