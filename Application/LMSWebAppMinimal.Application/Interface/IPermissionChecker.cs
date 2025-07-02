﻿using LMSWebAppMinimal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Application.Interface
{
    public interface IPermissionChecker
    {
        void Check(int userId, Permission permission, string errMsg);
    }
}
