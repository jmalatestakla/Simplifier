using System;
using System.ComponentModel.DataAnnotations;

namespace Simplifier.Entities
{
    public class FormFields
    {
        [Key]
        public Guid Uuid { get; set; } // PK
        public Guid TemplateId { get; set; } // FK
        public string FormField { get; set; }
        public string FormType { get; set; }
        public string ExpectedResponse { get; set; }
        public int Order { get; set; }
    }
}