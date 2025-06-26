using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.Interface
{
    public interface ILoginService
    {
        BaseUser Login(int userId);
        BaseUser Regsiter(string name, UserType type);
    }
}
