using LMSWebAppMinimal.Domain.Enum;
using LMSWebAppMinimal.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Domain.Base
{
    public abstract class BaseUser : IEntity
    {
        private string name;
        private int? id;
        protected UserType type;

        protected BaseUser() { }

        public BaseUser(string name, UserType userType)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Type = userType;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                name = value;
            }
        }

        public int Id
        {
            get { return id ?? throw new NullReferenceException(); }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("ID must be a positive integer.");
                }
                id = value;
            }
        }

        public abstract UserType Type
        {
            get;
            set;
        }
    }
}
