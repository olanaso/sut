using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IVCalidadRequisitos_CABECERAService
    {
        VCalidadRequisitos_CABECERA GetOne(long VCalidadRequisitos_CABECERAid); 
        List<VCalidadRequisitos_CABECERA> GetAllLikePagin(VCalidadRequisitos_CABECERA filtro, int pageIndex, int pageSize, ref int totalRows);
        List<VCalidadRequisitos_CABECERA> GetAll(Int32 CodEntidad);

    }
}
