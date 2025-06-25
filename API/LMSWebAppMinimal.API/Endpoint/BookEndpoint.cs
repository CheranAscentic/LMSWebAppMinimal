using LMSWebAppMinimal.API.DTO;
using LMSWebAppMinimal.API.Interface;
using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Application.Service;

namespace LMSWebAppMinimal.API.Endpoint
{
    public class BookEndpoint : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            //Get All Books
            var books = app.MapGroup("/api/books")
                .WithTags("Books")
                .WithOpenApi();

            books.MapGet("/", (IBookService bookService) =>
            {
                return Results.Ok(bookService.GetBooks());
            });

            //Get Book with Id
            books.MapGet("/{id}", (int id, IBookService bookService) =>
            {
                try
                {
                    var book = bookService.GetBook(id);
                    return Results.Ok(book);
                }
                catch (Exception e)
                {
                    return Results.NotFound("Book with Id could not be found.");
                }
            });

            //Create Book with BookDTO
            books.MapPost("/", (BookDTO bookDTO, IBookService bookService) =>
            {
                try
                {
                    var book = bookService.AddBook(bookDTO.Title, bookDTO.Author, bookDTO.Year, bookDTO.Category);
                    return Results.Created($"/api/books/{book.Id}", book);
                }
                catch (Exception e)
                {
                    return Results.NotFound("Book could not be created");
                }
            });

            //Update Book with id to BookDTO
            books.MapPut("/{id}", (int id, BookDTO bookDTO, IBookService bookService) =>
            {
                try
                {
                    var book = bookService.UpdateBook(id, bookDTO.Title, bookDTO.Author, bookDTO.Year, bookDTO.Category);
                    return Results.Ok(book);
                }
                catch (Exception e)
                {
                    return Results.NotFound("Book with Id could not be updated");
                }
            });

            //Delete Book with Id
            books.MapDelete("/{id}", (int id, IBookService bookService) =>
            {
                try
                {
                    var book = bookService.RemoveBook(id);
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.NotFound("Book with Id could not be deleted.");
                }
            });
        }
    }
}
