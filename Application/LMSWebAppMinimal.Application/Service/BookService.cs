using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Data.Repository;
using LMSWebAppMinimal.Domain.Model;
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

        public BookService(IRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        public Book AddBook(string title, string? author, int? year, string category)
        {
            Book newBook = new Book(title, author, year, category);
            bookRepository.Add(newBook);
            return newBook;
        }

        public Book GetBook(int id)
        {
            return bookRepository.Get(id) ?? throw new Exception("Book with Title cannot be found");
        }

        public List<Book> GetBooks()
        {
            return bookRepository.GetAll();
        }

        public Book UpdateBook(int id, string? title, string? author, int? year, string? category)
        {
            /*var book = new Book(title, author, year, category);
            book.Id = id;*/
            var book = bookRepository.Get(id) ?? throw new Exception("Book with Title cannot be found");

            book.Title = title ?? book.Title;
            book.Author = author ?? book.Author;
            book.PublicationYear = year ?? book.PublicationYear;
            book.Category = category ?? book.Category;

            return bookRepository.Update(book);
        }

        public Book RemoveBook(int id)
        {
            return bookRepository.Remove(id) ?? throw new Exception("Book with Title cannot be found");
        }
    }
}
