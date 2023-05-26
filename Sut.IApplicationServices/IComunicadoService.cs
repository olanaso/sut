using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IComunicadoService
    {
        Comunicado GetOne(long Comunicadoid);
        Comunicado LsitaGetOne();
        List<Comunicado> GetAllLikePagin(Comunicado filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Comunicado> GetAllLikePaginBaner(Comunicado filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(Comunicado obj);
 
    }
}
