using DataClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult> GetBookGraph(int id)
        {
            var book = await context.Books.AsNoTracking().Include(x => x.Publisher).Include(x => x.Author)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(book);
        }
    }
}
