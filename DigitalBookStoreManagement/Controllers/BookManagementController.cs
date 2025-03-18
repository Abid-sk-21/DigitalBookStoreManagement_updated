using DigitalBookStoreManagement.Authentication;
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBookStoreManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookManagementController : ControllerBase
    {
        private readonly IBookManagementService _bookService;
        private readonly IAuth _jwtAuth;


        public BookManagementController(IBookManagementService bookService, IAuth jwtAuth)
        {
            _bookService = bookService;
            _jwtAuth = jwtAuth;
        }

        // GET: api/book
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookManagement>>> GetAllBooks()
        {
            return Ok(await _bookService.GetAllBooksAsync());
        }

        // GET: api/book/{id}
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookManagement>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            return Ok(book);
        }

        // POST: api/book
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddBook([FromBody] BookManagement book)
        {
            if (book == null)
            {
                return BadRequest("Invalid book data.");
            }
            await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.BookID }, book);
        }

        // PUT: api/book/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] BookManagement book)
        {
            if (book == null || id != book.BookID)
            {
                return BadRequest("Invalid book data.");
            }
            await _bookService.UpdateBookAsync(book);
            return NoContent();
        }

        // DELETE: api/book/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }

        // GET: api/book/search?title={title}
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookManagement>>> SearchBooksByTitle([FromQuery] string title)
        {
            return Ok(await _bookService.SearchBooksByTitleAsync(title));
        }

        // GET: api/book/filter/category?categoryName={categoryName}
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("filter/category")]
        public async Task<ActionResult<IEnumerable<BookManagement>>> FilterBooksByCategory([FromQuery] string categoryName)
        {
            return Ok(await _bookService.GetBooksByCategoryNameAsync(categoryName));
        }

        // GET: api/book/filter/author?authorName={authorName}
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("filter/author")]
        public async Task<ActionResult<IEnumerable<BookManagement>>> FilterBooksByAuthor([FromQuery] string authorName)
        {
            return Ok(await _bookService.GetBooksByAuthorNameAsync(authorName));
        }




        // GET: api/book/stock/{id}
        //[HttpGet("stock/{id}")]
        //public async Task<ActionResult<string>> GetStockAvailability(int id)
        //{
        //    return Ok(await _bookService.GetStockAvailabilityAsync(id));
        //}
    }
}
