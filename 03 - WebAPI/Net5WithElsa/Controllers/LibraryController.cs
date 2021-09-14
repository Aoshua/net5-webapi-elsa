using Microsoft.AspNetCore.Mvc;
using Store;
using System.Threading.Tasks;

namespace Net5WithElsa.Controllers
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
    }
}
