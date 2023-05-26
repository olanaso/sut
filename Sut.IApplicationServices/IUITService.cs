using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IUITService
    {
        UIT GetOne(long UITid);
        UIT LsitaGetOne();
        List<UIT> GetAllLikePagin(UIT filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(UIT obj);
 
    }
}
