using LMSWebAppMinimal.Application.Interface;
using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Enum;
using LMSWebAppMinimal.Domain.Model;

namespace LMSWebAppMinimal.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPermissionChecker permissionChecker;

        public UserService(IUnitOfWork unitOfWork, IPermissionChecker permissionChecker)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.permissionChecker = permissionChecker ?? throw new ArgumentNullException(nameof(permissionChecker));
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

            var result = unitOfWork.Users.Add(newUser);
            unitOfWork.SaveChanges();
            return result;
        }

        public List<BaseUser> GetAllUsers(int authId)
        {
            permissionChecker.Check(authId, Permission.UserViewAll, "User does not have permission to view all users.");
            return unitOfWork.Users.GetAll();
        }

        public BaseUser GetUser(int authId, int userId)
        {
            permissionChecker.Check(authId, Permission.UserView, "User does not have permission to view user.");
            return unitOfWork.Users.Get(userId) ?? throw new Exception("User with Title cannot be found");
        }

        public BaseUser UpdateUser(int authId, int userId, String? name, UserType? type)
        {
            permissionChecker.Check(authId, Permission.UserUpdate, "User does not have permission to update user.");
            BaseUser user = unitOfWork.Users.Get(userId) ?? throw new Exception("User with Id cannot be found");
            user.Name = name ?? user.Name;
            user.Type = type ?? user.Type;
            var result = unitOfWork.Users.Update(user);
            unitOfWork.SaveChanges();
            return result;
        }

        public BaseUser RemoveUser(int authId, int userId)
        {
            permissionChecker.Check(authId, Permission.UserDelete, "User does not have permission to delete user.");
            var result = unitOfWork.Users.Remove(userId) ?? throw new Exception("User with Id cannot be found");
            unitOfWork.SaveChanges();
            return result;
        }
    }
}
