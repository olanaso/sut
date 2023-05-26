using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IFactorDedicacionService
    {
        List<FactorDedicacion> GetValoresByUndOrganica(long ExpedienteId, long UnidadOrganicaId);
        List<FactorDedicacion> GetValoresByUndOrganicaLista(long ExpedienteId,TipoRecurso tiporecurso);
        List<FactorDedicacion> GetAutoCalculoByUndOrganica(long EntidadId, long ActivarAlgoritmo ,long ExpedienteId, long UnidadOrganicaId);
        void Eliminar(long ExpedienteId);

        List<string> Validarfactor(long ExpedienteId,long UnidadOrganicaId);

        List<FactorDedicacion> GetAutoCalculoByUndOrganicacantidad(long EntidadId, long ExpedienteId, long UnidadOrganicaId);
        void Guardar(List<FactorDedicacion> lista);
    }
}
