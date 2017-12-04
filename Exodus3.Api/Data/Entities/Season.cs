using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exodus3.Api.Data.Entities
{
    public class Season : E3Entity
    {
        [Required]
        public int Number { get; set; }

        [Required]
        public Guid SeriesId { get; set; }
        public Series Series { get; set; }

        public ICollection<Sermon> Sermons { get; } = new List<Sermon>();
    }
}
