using System;
namespace Exodus3.Api.Data.Entities
{
    public class Sermon : E3Entity
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string AudioSrcUrl { get; set; }

        public int SeriesId { get; set; }
        public Series Series { get; set; }

        public Sermon()
        {
        }
    }
}
