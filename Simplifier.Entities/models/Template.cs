using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Simplifier.Entities
{
    public class Template
    {
        [Key]
        public Guid Uuid { get; set; } // PK
        public Guid UserId { get; set; } // FK
        public string Name { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<FormFields> FormFields { get; set; }
    }
}