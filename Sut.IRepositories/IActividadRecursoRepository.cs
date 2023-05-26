using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IRepositories
{
    public interface IActividadRecursoRepository : IBaseRepository<ActividadRecurso>
    {
        List<ActividadRecurso> GetByActividad(long ActividadId);

        List<ActividadRecurso> GetActividadRecursoListaProceso1(long ExpedienteId, TipoRecurso tiporecurso);
        List<ActividadRecurso> GetActividadRecursoLista(long ExpedienteId);
        List<ActividadRecurso> GetActividadRecursoLista2(long ExpedienteId);
        List<ActividadRecurso> GetByTablaAsme(long TablaAsmeId);
        List<ActividadRecurso> GetByTablaAsmeTotal(long ExpedienteId);
        List<Actividad> GetDataByTablaAsmeActividad(long TablaAsmeId);
        List<Actividad> GetByActividadEntidad(long entidad);
        List<Recurso> GetDataByTablaAsmeRecursos(long IdEntidad);
    }
}
