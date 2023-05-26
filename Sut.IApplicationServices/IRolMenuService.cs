using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IRolMenuService
    {
       
        void Save(RolMenu obj);
        List<RolMenu> GetByRolMenu(long rolId);
        List<RolMenu> GetByRolMenuid(long rolId, long menuId);

        void Guardarrolmenu(List<RolMenu> lstRolMenu);
        void Eliminar(long rolId);
    }
}
