using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IReporteService
    {
        List<AnexoPersonal> GetAnexoPeronalByExpediente(long ExpedienteId);

        List<AnexoPersonal> GetAnexoPeronalByExpedienteTablaASMEId(long ExpedienteId, long TablaASMEId);
        List<RecursoCosto> GetRecursoCostoLista(long ExpedienteId);
        List<ActividadRecurso> GetActividadRecursoLista(long ExpedienteId );
        List<ActividadRecurso> GetActividadRecursoListaProceso1(long ExpedienteId, TipoRecurso tiporecurso);
        List<AnexoPersonal> GetAnexoPeronalByExpedientelista(long ExpedienteId);
        List<AnexoIdentificable> GetAnexoIdentificableByExpediente(long ExpedienteId, TipoRecurso tipo);

        List<AnexoIdentificable> GetAnexoIdentificableByExpedienteTablaASMEId(long ExpedienteId, TipoRecurso tipo, long TablaASMEId);
        List<AnexoNoIdentificable> GetAnexoNoIdentificableByExpediente(long ExpedienteId, TipoRecurso tipo);
        List<AnexoNoIdentificable> GetAnexoNoIdentificableByExpedientep1(long ExpedienteId, TipoRecurso tipo);
    }
}
