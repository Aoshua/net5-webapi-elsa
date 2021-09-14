using System;
using System.ComponentModel.DataAnnotations;
using static DataClasses.Library.Enums;

namespace DataClasses.Library
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public BookFormat Format { get; set; }
        public string Isbn { get; set; }
        public DateTime? OriginalPublication { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int? PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
