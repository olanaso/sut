using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IRolMenuRepository : IBaseRepository<RolMenu>
    {

        List<RolMenu> GetByRolMenu(long rolId);
        List<RolMenu> GetByRolMenuid(long rolId, long menuId);
        new void Save(RolMenu obj);
        void Eliminar(long rolId);


        void Guardarrolmenu(List<RolMenu> lstRolMenu);


    } 
}
