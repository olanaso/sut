using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IVCalidadRequisitos_4_2EXANTERepository : IBaseRepository<VCalidadRequisitos_4_2EXANTE>
    {
        VCalidadRequisitos_4_2EXANTE GetOne(long VCalidadRequisitos_4_2EXANTEid); 
        List<VCalidadRequisitos_4_2EXANTE> GetAll();


        List<VCalidadRequisitos_4_2EXANTE> GetAllCOD(Int32 CodEntidad);

    }
}
