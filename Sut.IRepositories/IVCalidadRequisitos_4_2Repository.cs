using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IVCalidadRequisitos_4_2Repository : IBaseRepository<VCalidadRequisitos_4_2>
    {
        VCalidadRequisitos_4_2 GetOne(long VCalidadRequisitos_4_2id); 
        List<VCalidadRequisitos_4_2> GetAll();


        List<VCalidadRequisitos_4_2> GetAllCOD(Int32 CodEntidad);

    }
}
