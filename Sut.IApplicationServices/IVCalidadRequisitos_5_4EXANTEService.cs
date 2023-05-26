using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IVCalidadRequisitos_5_4EXANTEService
    {
        VCalidadRequisitos_5_4EXANTE GetOne(long VCalidadRequisitos_5_4id); 
        List<VCalidadRequisitos_5_4EXANTE> GetAllLikePagin(VCalidadRequisitos_5_4EXANTE filtro, int pageIndex, int pageSize, ref int totalRows);
        List<VCalidadRequisitos_5_4EXANTE> GetAll(Int32 CodEntidad);

    }
}
