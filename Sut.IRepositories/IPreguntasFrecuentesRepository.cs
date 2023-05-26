using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IPreguntasFrecuentesRepository : IBaseRepository<PreguntasFrecuentes>
    {
        PreguntasFrecuentes GetOne(long preguntasID);
        PreguntasFrecuentes LsitaGetOne();
        List<PreguntasFrecuentes> GetAll();
        List<PreguntasFrecuentes> GetAllAcordion(PreguntasFrecuentes filtro);
        List<PreguntasFrecuentes> GetAllEntidad(PreguntasFrecuentes filtro);
        void Save(PreguntasFrecuentes obj);
      
    }
}
