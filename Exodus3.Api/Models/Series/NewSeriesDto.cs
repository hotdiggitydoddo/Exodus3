using System;
using System.ComponentModel.DataAnnotations;

namespace Exodus3.Api.Models
{
    public class NewSeriesDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
    }
}
