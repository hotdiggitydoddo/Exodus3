using System;
namespace Exodus3.Api.Models
{
    public class SermonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string AudioSrcUrl { get; set; }
        public DateTimeOffset Date { get; set; }

        public Guid? SeriesId { get; set; }
    }
}
