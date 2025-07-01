using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Enum;
using LMSWebAppMinimal.Domain.Model;

namespace LMSWebAppMinimal.Application.Service
{
    public class UserService : IUserService
    {
         
        private readonly IRepository<BaseUser> userRepository;
        private readonly IPermissionChecker permissionChecker;

        public UserService(IRepository<BaseUser> userRepository, IPermissionChecker permissionCherkker)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.permissionChecker = permissionCherkker ?? throw new ArgumentNullException(nameof(permissionCherkker));
        }

        public BaseUser AddUser(int authId, string name, UserType type)
        {
            permissionChecker.Check(authId, Permission.UserAdd, "User does not have permission to add user.");
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

        public List<BaseUser> GetAllUsers(int authId)
        {
            permissionChecker.Check(authId, Permission.UserViewAll, "User does not have permission to view all users.");
            return userRepository.GetAll();
        }

        public BaseUser GetUser(int authId, int userId)
        {
            permissionChecker.Check(authId, Permission.UserView, "User does not have permission to view user.");
            return userRepository.Get(userId) ?? throw new Exception("User with Title cannot be found");
        }

        public BaseUser UpdateUser(int authId, int userId, String? name, UserType? type)
        {
            permissionChecker.Check(authId, Permission.UserUpdate, "User does not have permission to update user.");
            BaseUser user = userRepository.Get(userId) ?? throw new Exception("User with Id cannot be found");
            user.Name = name ?? user.Name;
            user.Type = type ?? user.Type;
            return userRepository.Update(user);
        }

        public BaseUser RemoveUser(int authId, int userId)
        {
            permissionChecker.Check(authId, Permission.UserDelete, "User does not have permission to delete user.");
            return userRepository.Remove(userId) ?? throw new Exception("User with Id cannot be found");
        }
    }
}
