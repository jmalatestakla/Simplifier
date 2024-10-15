using System;
using System.ComponentModel.DataAnnotations;

namespace Simplifier.Entities
{
    public class FormResponses
    {
        [Key]
        public Guid Uuid { get; set; } // PK
        public Guid ApplicationId { get; set; } // FK
        public string FormField { get; set; }
        public string Response { get; set; }
        public Application Application { get; set; }
    }
}