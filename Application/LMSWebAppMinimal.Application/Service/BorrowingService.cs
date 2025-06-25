using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Data.Repository;
using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.Service
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<BaseUser> userRepository;

        public BorrowingService(IRepository<Book> bookRepository, IRepository<BaseUser> userRepository)
        {
            this.bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public Book BorrowBook(int bookId, int memberId)
        {
            var book = bookRepository.Get(bookId) ?? throw new Exception("Book not found");

            if (!book.Available)
                throw new Exception("Book is not available for borrowing");

            var user = userRepository.Get(memberId);

            if (user == null || !(user is Member member))
                throw new Exception("Only members can borrow books");

            book.Available = false;
            bookRepository.Update(book);

            member.BorrowedBooks.Add(book);
            userRepository.Update(member);

            return book;
        }

        public List<Book> GetBorrowedBooks(int memberId)
        {
            var user = userRepository.Get(memberId);

            if (user == null || !(user is Member member))
                throw new Exception("Member not found");

            return member.BorrowedBooks;
        }

        public Book ReturnBook(int bookId, int memberId)
        {
            var book = bookRepository.Get(bookId) ?? throw new Exception("Book not found");

            var user = userRepository.Get(memberId);

            if (user == null || !(user is Member member))
                throw new Exception("Only members can return books");

            if (!member.BorrowedBooks.Any(b => b.Id == bookId))
                throw new Exception("Member has not borrowed this book");

            book.Available = true;
            bookRepository.Update(book);

            member.BorrowedBooks.RemoveAll(b => b.Id == bookId);
            userRepository.Update(member);

            return book;
        }
    }
}
