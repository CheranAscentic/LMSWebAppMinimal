using LMSWebAppMinimal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.Interface
{
    public interface IPermissionChecker
    {
        bool HasPermission(UserType userType, Permission permission);
    }
}
