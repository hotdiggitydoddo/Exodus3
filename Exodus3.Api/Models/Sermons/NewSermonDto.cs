using System;
using System.ComponentModel.DataAnnotations;

namespace Exodus3.Api.Models
{
    public class NewSermonDto
    {
        [Required, StringLength(150)]
        public string Name { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required, StringLength(100)]
        public string AudioSrcUrl { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        public Guid? SeasonId { get; set; }
    }
}
