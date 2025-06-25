using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Domain.Model
{
    public class Staff : BaseUser
    {
        public Staff(string name, UserType type) : base(name, type) { }

        public override UserType Type
        {
            get { return type; }
            set
            {
                UserType[] validTypes = { UserType.StaffMinor, UserType.StaffManagement };

                if (!validTypes.Contains(value) || value == null)
                {
                    throw new Exception("Invalid user type for Staff.");
                }
                type = value;
            }
        }
    }
}
