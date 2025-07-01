using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Domain.Model;
using LMSWebAppMinimal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.Service
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPermissionChecker permissionChecker;

        public BookService(IUnitOfWork unitOfWork, IPermissionChecker permissionChecker)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.permissionChecker = permissionChecker ?? throw new ArgumentNullException(nameof(permissionChecker));
        }

        public Book AddBook(int authId, string title, string? author, int? year, string category)
        {
            permissionChecker.Check(authId, Permission.BookAdd, "User does not have permission to add book.");
            Book newBook = new Book(title, author, year, category);
            var result = unitOfWork.Books.Add(newBook);
            unitOfWork.SaveChanges();
            return result;
        }

        public Book GetBook(int authId, int bookId)
        {
            permissionChecker.Check(authId, Permission.BookView, "User does not have permission to view book.");
            return unitOfWork.Books.Get(bookId) ?? throw new Exception("Book with Title cannot be found");
        }

        public List<Book> GetBooks(int authId)
        {
            permissionChecker.Check(authId, Permission.BookViewAll, "User does not have permission to view all books.");
            return unitOfWork.Books.GetAll();
        }

        public Book UpdateBook(int authId, int bookId, string? title, string? author, int? year, string? category)
        {
            permissionChecker.Check(authId, Permission.BookUpdate, "User does not have permission to update book.");
            var book = unitOfWork.Books.Get(bookId) ?? throw new Exception("Book with Title cannot be found");

            book.Title = title ?? book.Title;
            book.Author = author ?? book.Author;
            book.PublicationYear = year ?? book.PublicationYear;
            book.Category = category ?? book.Category;

            var result = unitOfWork.Books.Update(book);
            unitOfWork.SaveChanges();
            return result;
        }

        public Book RemoveBook(int authId, int bookId)
        {
            permissionChecker.Check(authId, Permission.BookDelete, "User does not have permission to delete book.");
            var result = unitOfWork.Books.Remove(bookId) ?? throw new Exception("Book with Title cannot be found");
            unitOfWork.SaveChanges();
            return result;
        }
    }
}
