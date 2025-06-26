using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Data.Repository;
using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Enum;
using LMSWebAppMinimal.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.Service
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<BaseUser> userRepository;

        public LoginService(IRepository<BaseUser> userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public BaseUser Login(int userId)
        {
            return userRepository.Get(userId) ?? throw new Exception("User with ID cannot be found to login");
        }

        public BaseUser Regsiter(string name, UserType type)
        {
            BaseUser newUser;

            switch (type)
            {
                case UserType.Member:
                    newUser = new Member(name);
                    break;
                case UserType.StaffMinor:
                    newUser = new Staff(name, type);
                    break;
                case UserType.StaffManagement:
                    newUser = new Staff(name, type);
                    break;
                default:
                    throw new ArgumentException("Invalid user type");
            }

            return userRepository.Add(newUser);
        }
    }
}
