using LMSWebAppMinimal.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Data.Repository
{
    public interface IRepository<E> where E : IEntity
    {
        E Add(E entity);
        E Get(int id);
        List<E> GetAll();
        E Remove(int id);
        E Update(E Entity);
    }
}
