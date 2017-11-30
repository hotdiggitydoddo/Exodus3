using System;
using System.ComponentModel.DataAnnotations;

namespace Exodus3.Api.Data.Entities
{
    public class E3Entity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTimeOffset CreatedOn { get; set; }

        [Required]
        public DateTimeOffset UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
       
    }
}
