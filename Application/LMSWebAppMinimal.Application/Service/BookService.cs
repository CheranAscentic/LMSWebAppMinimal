using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Data.Repository;
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
        private readonly IRepository<Book> bookRepository;
        private readonly IPermissionChecker permissionChecker;

        public BookService(IRepository<Book> bookRepository, IPermissionChecker permissionChecker)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            this.permissionChecker = permissionChecker ?? throw new ArgumentNullException(nameof(permissionChecker));
        }
        public Book AddBook(int authId, string title, string? author, int? year, string category)
        {
            permissionChecker.Check(authId, Permission.BookAdd, "User does not have permission to add book.");
            Book newBook = new Book(title, author, year, category);
            bookRepository.Add(newBook);
            return newBook;
        }

        public Book GetBook(int authId, int bookId)
        {
            permissionChecker.Check(authId, Permission.BookView, "User does not have permission to view book.");
            return bookRepository.Get(bookId) ?? throw new Exception("Book with Title cannot be found");
        }

        public List<Book> GetBooks(int authId)
        {
            permissionChecker.Check(authId, Permission.BookViewAll, "User does not have permission to view all books.");
            return bookRepository.GetAll();
        }

        public Book UpdateBook(int authId, int bookId, string? title, string? author, int? year, string? category)
        {
            permissionChecker.Check(authId, Permission.BookUpdate, "User does not have permission to update book.");
            var book = bookRepository.Get(bookId) ?? throw new Exception("Book with Title cannot be found");

            book.Title = title ?? book.Title;
            book.Author = author ?? book.Author;
            book.PublicationYear = year ?? book.PublicationYear;
            book.Category = category ?? book.Category;

            return bookRepository.Update(book);
        }

        public Book RemoveBook(int authId, int bookId)
        {
            permissionChecker.Check(authId, Permission.BookDelete, "User does not have permission to delete book.");
            return bookRepository.Remove(bookId) ?? throw new Exception("Book with Title cannot be found");
        }
    }
}
