using System;
using System.Collections.Generic;

namespace Exodus3.Domain
{
    public class Series : E3Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Sermon> Sermons { get; set; }

        public Series()
        {
        }
    }
}
