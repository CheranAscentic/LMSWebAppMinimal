using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Domain.Enum
{
    public enum Permission
    {
        BookView,
        BookViewAll,
        BookAdd,
        BookUpdate,
        BookDelete,

        UserView,
        UserViewAll,
        UserAdd,
        UserUpdate,
        UserDelete,

        BorrowBook,
        BorrowReturn,
        BorrowViewBorrowedBooks,

        None,
        All

/*        LoginLogin,
        LoginRegister,*/

    }
}
