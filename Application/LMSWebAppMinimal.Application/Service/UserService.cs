using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Data.Repository;
using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Enum;
using LMSWebAppMinimal.Domain.Model;

namespace LMSWebAppMinimal.Application.Service
{
    public class UserService : IUserService
    {
         
        private readonly IRepository<BaseUser> userRepository;

        public UserService(IRepository<BaseUser> userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public BaseUser AddUser(string name, UserType type)
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

        public List<BaseUser> GetAllUsers()
        {
            return userRepository.GetAll();
        }

        public BaseUser GetUser(int id)
        {

            return userRepository.Get(id) ?? throw new Exception("User with Title cannot be found");
        }

        public BaseUser UpdateUser(int id, String? name, UserType? type)
        {
            BaseUser user = userRepository.Get(id) ?? throw new Exception("User with Id cannot be found");
            user.Name = name ?? user.Name;
            user.Type = type ?? user.Type;
            return userRepository.Update(user);
        }

        public BaseUser RemoveUser(int id)
        {
            return userRepository.Remove(id) ?? throw new Exception("User with Id cannot be found");
        }
    }
}
