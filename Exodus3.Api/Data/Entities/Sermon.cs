﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Exodus3.Api.Data.Entities
{
    public class Sermon : E3Entity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string AudioSrcUrl { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        public Guid SeasonId { get; set; }

        public virtual Season Season { get; set; }
    }
}
