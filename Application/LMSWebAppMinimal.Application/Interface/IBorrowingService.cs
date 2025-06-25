using LMSWebAppMinimal.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.Interface
{
    public interface IBorrowingService
    {
        Book BorrowBook(int bookId, int memberId);
        Book ReturnBook(int bookId, int memberId);
        List<Book> GetBorrowedBooks(int memberId);
    }
}
