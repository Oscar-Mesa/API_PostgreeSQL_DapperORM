using Microsoft.AspNetCore.Mvc;
using NetCoreAPIPostgreSQL.Data.Repositories;
using NetCoreAPIPostgreSQL.Model;

namespace NetCoreAPIPostgreSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{PKId}")]
        public async Task<IActionResult> GetBookDetails(int PKId)
        {
            return Ok(await _bookRepository.GetBookDetails(PKId));
        }

        [HttpPost]
        public async Task<IActionResult> InsertBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest();
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addedBook = await _bookRepository.InsertBook(book);
            return Ok(addedBook);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bookRepository.UpdateBook(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {


            await _bookRepository.DeleteBook(new Book { PKId = id });

            return NoContent();
        }
    }
}
