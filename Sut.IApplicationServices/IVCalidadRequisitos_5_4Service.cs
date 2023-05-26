using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IVCalidadRequisitos_5_4Service
    {
        VCalidadRequisitos_5_4 GetOne(long VCalidadRequisitos_5_4id); 
        List<VCalidadRequisitos_5_4> GetAllLikePagin(VCalidadRequisitos_5_4 filtro, int pageIndex, int pageSize, ref int totalRows);
        List<VCalidadRequisitos_5_4> GetAll(Int32 CodEntidad);

    }
}
