using DataClasses;
using DataClasses.Library;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class LibraryStore
    {
        private readonly DataContext context;

        public LibraryStore(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Book>> GetBooksGraph()
        {
            var books = await context.Books.AsNoTracking().Include(x => x.Publisher).Include(x => x.Author).ToListAsync();
            return books;
        }

        public async Task<Book> GetBookGraph(int id)
        {
            var book = await context.Books.AsNoTracking().Include(x => x.Publisher).Include(x => x.Author)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            return book;
        }
    }
}
