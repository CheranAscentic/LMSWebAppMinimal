using LMSWebAppMinimal.Application.Interface;
using Microsoft.AspNetCore.Mvc;

using LMSWebAppMinimal.API.DTO;

namespace LMSWebAppMinimal.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    [Produces("application/json")]
    public class BookController : ControllerBase            
    {
        private readonly IBookService bookService;
        public BookController(IBookService bookService) {
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            try
            {
                var book = bookService.GetBook(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return NotFound("Book with Id could not be found.");
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] BookDTO bookDTO)
        {
            try
            {
                var book = bookService.AddBook(bookDTO.Title, bookDTO.Author, bookDTO.Year, bookDTO.Category);
                return CreatedAtAction(nameof(book), new { Id = book.Id}, book);
            }
            catch (Exception ex)
            {
                return NotFound("Book could not be created.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var book = bookService.RemoveBook(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return NotFound("Book with Id could not be deleted.");
            }
        }
    }
}
