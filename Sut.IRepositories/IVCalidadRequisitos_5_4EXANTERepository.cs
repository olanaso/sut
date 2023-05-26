using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IVCalidadRequisitos_5_4EXANTERepository : IBaseRepository<VCalidadRequisitos_5_4EXANTE>
    {
        VCalidadRequisitos_5_4EXANTE GetOne(long VCalidadRequisitos_5_4EXANTEid); 
        List<VCalidadRequisitos_5_4EXANTE> GetAll();


        List<VCalidadRequisitos_5_4EXANTE> GetAllCOD(Int32 CodEntidad);

    }
}
