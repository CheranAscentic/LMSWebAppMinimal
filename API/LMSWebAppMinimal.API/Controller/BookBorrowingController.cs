using LMSWebAppMinimal.API.DTO;
using LMSWebAppMinimal.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LMSWebAppMinimal.API.Controllers
{
    [Route("api/borrowing")]
    [ApiController]
    [Produces("application/json")]
    public class BookBorrowingController : ControllerBase
    {
        private readonly IBorrowingService borrowingService;

        public BookBorrowingController(IBorrowingService borrowingService)
        {
            this.borrowingService = borrowingService ?? throw new ArgumentNullException(nameof(borrowingService));
        }

        [HttpPost("borrow")]
        public IActionResult BorrowBook([FromBody] BookBorrowDTO borrowDTO)
        {
            try
            {
                var book = borrowingService.BorrowBook(borrowDTO.BookId, borrowDTO.MemberId);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not borrow Book for user");
            }
        }

        [HttpPost("return")]
        public IActionResult ReturnBook([FromBody] BookBorrowDTO borrowDTO)
        {
            try
            {
                var book = borrowingService.ReturnBook(borrowDTO.BookId, borrowDTO.MemberId);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not return book for user.");
            }
        }

        [HttpGet("member/{memberId}")]
        public IActionResult GetBorrowedBooks(int memberId)
        {
            try
            {
                var books = borrowingService.GetBorrowedBooks(memberId);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest("Could not get borrowed books for user with Id.");
            }
        }
    }
}