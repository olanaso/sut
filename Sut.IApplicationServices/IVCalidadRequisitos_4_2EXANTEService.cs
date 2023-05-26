using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IVCalidadRequisitos_4_2EXANTEService
    {
        VCalidadRequisitos_4_2EXANTE GetOne(long VCalidadRequisitos_4_2id); 
        List<VCalidadRequisitos_4_2EXANTE> GetAllLikePagin(VCalidadRequisitos_4_2EXANTE filtro, int pageIndex, int pageSize, ref int totalRows);

        List<VCalidadRequisitos_4_2EXANTE> GetAll(Int32 CodEntidad);

    }
}
