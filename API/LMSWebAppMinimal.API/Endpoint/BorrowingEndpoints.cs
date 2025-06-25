using LMSWebAppMinimal.API.DTO;
using LMSWebAppMinimal.API.Interface;
using LMSWebAppMinimal.Application.Interface;

namespace LMSWebAppMinimal.API.Endpoint;

public class BorrowingEndpoints : IEndpointGroup
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var borrowing = app.MapGroup("/api/borrowing")
            .WithTags("Book Borrowing")
            .WithOpenApi();

        // POST borrow book
        borrowing.MapPost("/borrow", (BookBorrowDTO borrowDTO, IBorrowingService borrowingService) =>
        {
            try
            {
                var book = borrowingService.BorrowBook(borrowDTO.BookId, borrowDTO.MemberId);
                return Results.Ok(book);
            }
            catch (Exception)
            {
                return Results.BadRequest("Could not borrow Book for user");
            }
        });

        // POST return book
        borrowing.MapPost("/return", (BookBorrowDTO borrowDTO, IBorrowingService borrowingService) =>
        {
            try
            {
                var book = borrowingService.ReturnBook(borrowDTO.BookId, borrowDTO.MemberId);
                return Results.Ok(book);
            }
            catch (Exception)
            {
                return Results.BadRequest("Could not return book for user.");
            }
        });

        // GET borrowed books by member ID
        borrowing.MapGet("/member/{memberId}", (int memberId, IBorrowingService borrowingService) =>
        {
            try
            {
                var books = borrowingService.GetBorrowedBooks(memberId);
                return Results.Ok(books);
            }
            catch (Exception)
            {
                return Results.BadRequest("Could not get borrowed books for user with Id.");
            }
        });
    }
}