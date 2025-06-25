using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.Interface
{
    public interface IUserService
    {
        public BaseUser AddUser(string name, UserType type);
        public BaseUser RemoveUser(int id);
        public BaseUser GetUser(int id);
        public BaseUser UpdateUser(int id, String? name, UserType? type);
        public List<BaseUser> GetAllUsers();
    }
}
