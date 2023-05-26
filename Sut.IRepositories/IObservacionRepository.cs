using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IObservacionRepository : IBaseRepository<Observacion>
    {
        Observacion GetOne(long Observacionid); 
         Observacion GetOneGlobalobs(Observacion filtro);
        Observacion GetOneGlobal(Observacion filtro);
        List<Observacion> Getlstentidad(long EntidadId);
        List<Observacion> GetlstentidadExp(long EntidadId, long ExpedienteId);
        List<Observacion> GetlstentidadTotal(long EntidadId, string Pantalla);

        List<Observacion> GetOneVerObservacion(Observacion filtro);
        List<Observacion> GetOneGlobalObservacion(Observacion filtro);
        Observacion LsitaGetOne();
        List<Observacion> GetAll();
        void Save(Observacion obj);


        List<Observacion> GetByExpedienteListapro(long ExpedienteId);

    }
}
