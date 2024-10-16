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
            var applicationsWithResponses = _context.Applications
                .Include(a => a.FormResponses)
                .ToList();
            foreach (var app in applicationsWithResponses)
            {
                _logger.LogInformation($"Application: {app.Name}, UserId: {app.UserId}, CreatedAt: {app.CreatedAt}");
            }
            return applicationsWithResponses;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Application application)
        {
            _context.Applications.Add(application);
            _context.SaveChanges();

            return Ok(new { message = "success" });
        }


        [HttpDelete("{uuid}")]
        public IActionResult Delete(Guid uuid)
        {
            _logger.LogInformation("Deleting application with uuid {uuid}", uuid);
            var application = _context.Applications.FirstOrDefault(a => a.Uuid == uuid);
            if (application == null)
            {
                _logger.LogWarning("Application with uuid {uuid} not found", uuid);
                return NotFound(new { message = "Application not found" });
            }

            // Retrieve and delete all form fields associated with the application
            var formResponses = _context.FormResponses.Where(f => f.ApplicationId == application.Uuid).ToList();
            _context.FormResponses.RemoveRange(formResponses);

            // Delete the application
            _context.Applications.Remove(application);
            _context.SaveChanges();

            _logger.LogInformation("Deleted application with uuid {uuid}", uuid);

            return Ok(new { message = "Successfully deleted application and associated form fields" });
        }


    }
}
