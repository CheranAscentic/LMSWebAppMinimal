using LMSWebAppMinimal.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.Interface
{
    public interface IBookService
    {
        public Book AddBook(string title, string? author, int? year, string category);
        public Book GetBook(int id);
        public List<Book> GetBooks();
        public Book UpdateBook(int id, string title, string? author, int? year, string category);
        public Book RemoveBook(int id);

    }
}
