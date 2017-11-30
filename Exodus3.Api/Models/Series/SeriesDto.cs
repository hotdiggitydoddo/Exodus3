using System;
using System.Collections.Generic;

namespace Exodus3.Api.Models
{
    public class SeriesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SeasonDto> Seasons { get; set; }
        public bool IsMultiPart => Seasons.Count > 1;

        public SeriesDto()
        {
            Seasons = new List<SeasonDto>();
        }
    }
}
