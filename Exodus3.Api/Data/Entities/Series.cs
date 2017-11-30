using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exodus3.Api.Data.Entities
{
    public class Series : E3Entity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public virtual ICollection<Season> Seasons { get; } = new List<Season>();
    }
}
