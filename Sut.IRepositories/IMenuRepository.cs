using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IMenuRepository : IBaseRepository<Menu>
    {
        
        new void Save(Menu obj); 
        List<Menu> GetByMenu();
        List<Menu> GetByMenuidpadre(long menuid);
        List<Menu> GetByMenusub();
        new List<Menu> GetAllid(long id);


    }
}
