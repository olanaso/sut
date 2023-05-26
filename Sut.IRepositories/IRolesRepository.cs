using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IRolesRepository : IBaseRepository<Roles>
    {
        
        new void Save(Roles obj); 
        Roles GetByone(long rolId);

        void Eliminar(long rolId);

        new List<Roles> GetAll();


    }
}
