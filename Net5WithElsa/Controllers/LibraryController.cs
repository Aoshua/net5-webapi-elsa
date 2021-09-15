using DataAccess;
using DataClasses.Library;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryStore libraryStore;

        public LibraryController(LibraryStore libraryStore)
        {
            this.libraryStore = libraryStore;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooksGraph()
        {
            var books = await libraryStore.GetBooksGraph();
            return Ok(books);
        }

        [HttpGet]
        public async Task<ActionResult<Book>> GetBookGraph(int id)
        {
            var book = await libraryStore.GetBookGraph(id);
            return Ok(book);
        }
    }
}
