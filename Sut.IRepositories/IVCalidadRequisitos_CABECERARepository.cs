using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IVCalidadRequisitos_CABECERARepository : IBaseRepository<VCalidadRequisitos_CABECERA>
    {
        VCalidadRequisitos_CABECERA GetOne(long VCalidadRequisitos_CABECERAid); 
        List<VCalidadRequisitos_CABECERA> GetAll();

        List<VCalidadRequisitos_CABECERA> GetAllCOD(Int32 CodEntidad);

    }
}
