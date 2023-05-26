using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IPreguntasFrecuentesService
    {
        PreguntasFrecuentes GetOne(long preguntasID);
        PreguntasFrecuentes LsitaGetOne();
        List<PreguntasFrecuentes> GetAllLikePagin(PreguntasFrecuentes filtro, int pageIndex, int pageSize, ref int totalRows);
        List<PreguntasFrecuentes> GetAllLikePaginAcordion(PreguntasFrecuentes filtro, int pageIndex, int pageSize, ref int totalRows);
        List<PreguntasFrecuentes> GetAllLikePaginEntidad(PreguntasFrecuentes filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(PreguntasFrecuentes obj);
 
    }
}
