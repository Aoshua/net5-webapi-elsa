using DataClasses;
using DataClasses.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class LibraryStore
    {
        private readonly DataContext context;

        public LibraryStore(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Book>> GetBooksGraph(bool track = false)
        {
            var books = new List<Book>();
            if (track)
                books = await context.Books.Include(x => x.Author).Include(x => x.Publisher).ToListAsync();
            else
                await context.Books.AsNoTracking().Include(x => x.Author).Include(x => x.Publisher).ToListAsync();

            return books;
        }
    }
}
