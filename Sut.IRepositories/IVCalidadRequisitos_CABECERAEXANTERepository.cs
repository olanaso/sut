using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IVCalidadRequisitos_CABECERAEXANTERepository : IBaseRepository<VCalidadRequisitos_CABECERAEXANTE>
    {
        VCalidadRequisitos_CABECERAEXANTE GetOne(long VCalidadRequisitos_CABECERAEXANTEid); 
        List<VCalidadRequisitos_CABECERAEXANTE> GetAll();

        List<VCalidadRequisitos_CABECERAEXANTE> GetAllCOD(Int32 CodEntidad);

    }
}
