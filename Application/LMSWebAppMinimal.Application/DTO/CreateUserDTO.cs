using LMSWebAppMinimal.Domain.Enum;

namespace LMSWebAppMinimal.Application.DTO
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public UserType Type { get; set; }
    }
}
