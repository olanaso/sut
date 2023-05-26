using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IUITRepository : IBaseRepository<UIT>
    {
        UIT GetOne(long UITid);
        UIT LsitaGetOne();
        List<UIT> GetAll();
        void Save(UIT obj);
      
    }
}
