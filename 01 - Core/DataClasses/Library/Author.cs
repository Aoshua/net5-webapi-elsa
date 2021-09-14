using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataClasses.Library
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
