using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IInformeService
    {
        Informe GetOne(long Informeid);
        Informe LsitaGetOne();
        List<Informe> listaGetOne(long Informeid);
        List<Informe> GetAllLikePagin(Informe filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(Informe obj);
 
    }
}
