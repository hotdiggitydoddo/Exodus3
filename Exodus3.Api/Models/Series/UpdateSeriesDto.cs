using System;
namespace Exodus3.Api.Models
{
    public class UpdateSeriesDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
