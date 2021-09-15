using DataClasses;
using DataClasses.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Net5WithElsa.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LibraryController : ControllerBase
    {
        private readonly DataContext context;

        public LibraryController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooksGraph()
        {
            var books = await context.Books.AsNoTracking().Include(x => x.Publisher).Include(x => x.Author).ToListAsync();
            return Ok(books);
        }

        [HttpGet]
        public async Task<ActionResult<Book>> GetBookGraph(int id)
        {
            var book = await context.Books.AsNoTracking().Include(x => x.Publisher).Include(x => x.Author)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(book);
        }
    }
}
