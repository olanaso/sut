using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IVCalidadRequisitos_CABECERAEXANTEService
    {
        VCalidadRequisitos_CABECERAEXANTE GetOne(long VCalidadRequisitos_CABECERAid); 
        List<VCalidadRequisitos_CABECERAEXANTE> GetAllLikePagin(VCalidadRequisitos_CABECERAEXANTE filtro, int pageIndex, int pageSize, ref int totalRows);
        List<VCalidadRequisitos_CABECERAEXANTE> GetAll(Int32 CodEntidad);

    }
}
