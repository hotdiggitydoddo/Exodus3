using System;
using System.Collections.Generic;

namespace Exodus3.Api.Data.Entities
{
    public class Series : E3Entity
    {
        public string Name { get; set; }
        public List<Sermon> Sermons { get; set; }

        public Series()
        {
        }
    }
}
