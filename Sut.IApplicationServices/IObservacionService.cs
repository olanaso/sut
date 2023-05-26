using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IObservacionService
    {
        Observacion GetOne(long Observacionid);
        Observacion GetOneGlobal(Observacion filtro);
        Observacion GetOneGlobalobs(Observacion filtro);
        List<Observacion> GetOneGlobalObservacion(Observacion filtro);


        List<Observacion> GetOneVerObservacion(Observacion filtro);
        List<Observacion> Getlstentidad(long EntidadId);

        List<Observacion> GetlstentidadExp(long EntidadId, long ExpedienteId);

        List<Observacion> GetlstentidadTotal(long EntidadId, string Pantalla);
        Observacion LsitaGetOne();
        List<Observacion> GetAllLikePagin(Observacion filtro, int pageIndex, int pageSize, ref int totalRows);        
        List<Observacion> GetAllLikePaginGeneral(Observacion filtro, int pageIndex, int pageSize, ref int totalRows);
        List<Observacion> GetAllLikePaginEntidad(Observacion filtro, int pageIndex, int pageSize, ref int totalRows);
        void Save(Observacion obj);


        List<Observacion> GetByExpedienteListapro(long ExpedienteId);
        
    }
}
