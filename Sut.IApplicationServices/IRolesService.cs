using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IRolesService
    {
       
        void Save(Roles obj);
         
        List<Roles> GetByRoles(Roles filtro, int pageIndex, int pageSize, ref int totalRows);
        
        List<Roles> GetAll();
        Roles GetByone(long rolId);


        void Eliminar(long rolId);


    }
}
