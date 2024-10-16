using Microsoft.AspNetCore.Mvc;
using Simplifier.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Simplifier.Constants;

namespace Simplifier.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplatesController : ControllerBase
    {
        private readonly ILogger<TemplatesController> _logger;
        private readonly SimplifierContext _context;


        public TemplatesController(ILogger<TemplatesController> logger, SimplifierContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Template> Get()
        {
            var templates = _context.Templates.Include(t => t.FormFields).ToList();
            return templates;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Template template)
        {   
            _context.Templates.Add(template);
            foreach (var field in template.FormFields)
            {
            field.TemplateId = template.Uuid;
            _context.FormFields.Add(field);
            }
            _context.SaveChanges();

            return Ok(new { message = "success" });
        }
    }
}
