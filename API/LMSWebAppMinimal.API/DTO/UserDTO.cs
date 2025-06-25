using LMSWebAppMinimal.Domain.Enum;

namespace LMSWebAppMinimal.API.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        private UserType type;

        public UserType Type {
            get { return this.type; }
            set {
                UserType[] validTypes = { UserType.StaffMinor, UserType.StaffManagement };

                if (value == null || !validTypes.Contains(value))
                {
                    throw new Exception("Invalid user type for Staff.");
                }
                type = value;
            }
        }
    }
}
