using System;
using System.ComponentModel.DataAnnotations;

namespace Simplifier.Entities
{
    public class Application
    {
        [Key]
        public Guid Uuid { get; set; } // PK
        public Guid UserId { get; set; } // FK
        public string Name { get; set; }
        public string RawText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid TemplateId { get; set; } // FK
        public string FormResponses { get; set; }
        public Template Template { get; set; }
    }
}