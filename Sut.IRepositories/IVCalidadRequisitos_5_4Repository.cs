using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IVCalidadRequisitos_5_4Repository : IBaseRepository<VCalidadRequisitos_5_4>
    {
        VCalidadRequisitos_5_4 GetOne(long VCalidadRequisitos_5_4id); 
        List<VCalidadRequisitos_5_4> GetAll();


        List<VCalidadRequisitos_5_4> GetAllCOD(Int32 CodEntidad);

    }
}
