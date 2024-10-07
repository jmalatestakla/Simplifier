using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Simplifier.Entities
{
    public class User
    {
        [Key]
        public Guid Uuid { get; set; } // PK
        public string Email { get; set; }
    }
}
