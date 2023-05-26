using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IVCalidadRequisitos_4_2Service
    {
        VCalidadRequisitos_4_2 GetOne(long VCalidadRequisitos_4_2id); 
        List<VCalidadRequisitos_4_2> GetAllLikePagin(VCalidadRequisitos_4_2 filtro, int pageIndex, int pageSize, ref int totalRows);

        List<VCalidadRequisitos_4_2> GetAll(Int32 CodEntidad);

    }
}
