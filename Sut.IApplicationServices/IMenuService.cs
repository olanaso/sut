using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IMenuService
    {
       
        void Save(Menu obj);
        void Delete(long MenuId);

        List<Menu> GetByMenu();
        List<Menu> GetByMenuConfig(long id, int pageIndex, int pageSize, ref int totalRows);

        List<Menu> GetByMenuidpadre(long menuid);
        List<Menu> GetByMenusub();


    }
}
