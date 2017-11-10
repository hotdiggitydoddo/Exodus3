using System;
namespace Exodus3.Api.Data.Entities
{
    public class E3Entity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public E3Entity()
        {
        }
    }
}
