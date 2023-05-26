using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IVCalidadRequisitosService
    {
        VCalidadRequisitos GetOne(long VCalidadRequisitosid); 
        List<VCalidadRequisitos> GetAllLikePagin(VCalidadRequisitos filtro, int pageIndex, int pageSize, ref int totalRows);
   
 
    }
}
