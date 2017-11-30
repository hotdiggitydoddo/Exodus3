using System;
using System.Collections.Generic;

namespace Exodus3.Api.Models
{
    public class SeasonDto
    {
        public int Number { get; set; }
        public List<SermonDto> Sermons { get; set; }

        public SeasonDto()
        {
            Sermons = new List<SermonDto>();
        }
    }
}
