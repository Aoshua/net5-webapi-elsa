using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataClasses.Library
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public StateCode State { get; set; }
        public string Zip { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
