using System;
namespace Exodus3.Domain
{
    public class E3Entity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
       
    }
}
