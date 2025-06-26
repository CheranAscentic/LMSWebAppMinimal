using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.Service
{
    internal class LoginService : ILoginService
    {
        private readonly IRespositor<BaseUser> userRepository;
        public BaseUser Login(int userId)
        {
            throw new NotImplementedException();
        }

        public BaseUser Regsiter(string name, UserType type)
        {
            throw new NotImplementedException();
        }
    }
}
