using DataClasses;
using DataClasses.Library;
using Microsoft.EntityFrameworkCore;
using System;

namespace Store
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Book>()
                .HasOne(x => x.Publisher)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.PublisherId)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed:
            builder.Entity<Author>()
                .HasData(new Author { Id = 1, FirstName = "Victor", MiddleName = "", LastName = "Hugo", DateOfBirth = Convert.ToDateTime("02/26/1802"), DateOfDeath = Convert.ToDateTime("05/22/1885") });

            builder.Entity<Author>()
                .HasData(new Author { Id = 2, FirstName = "Nelle", MiddleName = "Harper", LastName = "Lee", DateOfBirth = Convert.ToDateTime("04/28/1926"), DateOfDeath = Convert.ToDateTime("02/19/2016") });

            builder.Entity<Publisher>()
                .HasData(new Publisher { Id = 1, Title = "Harper Perennial", Address1 = "", Address2 = "", City = "New York", State = StateCode.NY, Zip = "" });

            builder.Entity<Publisher>()
                .HasData(new Publisher { Id = 2, Title = "Signet Classes", Address1 = "", Address2 = "", City = "New York", State = StateCode.NY, Zip = "" });

            builder.Entity<Book>()
                .HasData(new Book { Id = 1, Title = "Les Misérables", Format = Enums.BookFormat.Paperback, PageCount = 1488, Isbn = "045141943X", AuthorId = 1, PublisherId = 2, OriginalPublication = Convert.ToDateTime("01/01/1862") });

            builder.Entity<Book>()
                .HasData(new Book { Id = 2, Title = "To Kill a Mockingbird", Format = Enums.BookFormat.Hardcover, PageCount = 336, Isbn = "0062420704", AuthorId = 2, PublisherId = 1, OriginalPublication = Convert.ToDateTime("06/11/1960") });

        }

        #region Library
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        #endregion
    }
}
