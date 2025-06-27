using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.PermissionChecker
{
    public class PermissionChecker : IPermissionChecker
    { 
        private readonly Dictionary<UserType, List<Permission>> userTypePermissions =
            new Dictionary<UserType, List<Permission>>
            {
            {
                UserType.Member,
                new List<Permission>
                {
                    Permission.BookView,
                    Permission.BookViewAll,
                    Permission.UserView,
                    Permission.UserUpdate,
                    Permission.BorrowBook,
                    Permission.BorrowReturn,
                    Permission.BorrowViewBorrowedBooks
                }
            },
            {
                UserType.StaffMinor,
                new List<Permission>
                {
                    Permission.BookView,
                    Permission.BookViewAll,
                    Permission.BookAdd,
                    Permission.BookUpdate,
                    Permission.BookDelete,
                    Permission.UserAdd,
                    Permission.BorrowViewBorrowedBooks
                }
            },
            {
                UserType.StaffManagement,
                new List<Permission>
                {
                    Permission.BookView,
                    Permission.BookViewAll,
                    Permission.BookAdd,
                    Permission.BookUpdate,
                    Permission.BookDelete,
                    Permission.UserView,
                    Permission.UserViewAll,
                    Permission.UserAdd,
                    Permission.UserUpdate,
                    Permission.UserDelete,
                    Permission.BorrowViewBorrowedBooks
                }
            }
            };

        public bool HasPermission(UserType userType, Permission permission)
        {
            var perms = userTypePermissions[userType] ?? throw new ArgumentException("Invalid user type");
            return (!perms.Contains(Permission.None) || perms.Contains(Permission.All) || perms.Contains(permission));
        }
    }
}
