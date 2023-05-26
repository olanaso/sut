using Sut.Entities;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IActividadService
    {
        Actividad GetOne(long ActividadId);
        Actividad GetOneasme(long TablaAsmeId);
        void Delete(long TablaAsmeId, long ActividadId);

        void Deleteactividad(long TablaAsmeId, long ActividadId);
        List<Actividad> GetByTablaAsme(long TablaAsmeId);


        List<Actividad> GetByTablaAsmever(Filtros filtros, long TablaAsmeId, string des);
        void guardarnumeroprocedimiento(Actividad obj);
        List<ActividadRecurso> GetByActividad(long ActividadId);
        void GuardarRecursos(Actividad obj);

        void ActualizarActividad(Actividad obj);

        void GuardarRecursosEliminar(Actividad obj);
        
        List<ActividadRecurso> GetDataByTablaAsme(long TablaAsmeId);
        List<ActividadRecurso> GetDataByTablaAsmeTotal(long ExpedienteId);


        List<Actividad> GetDataByTablaAsmeActividad(long TablaAsmeId);

        List<ActividadRecurso> GetActividadRecursoLista2(long ExpedienteId);
        

        List<Recurso> GetDataByTablaAsmeRecursos(long IdEntidad);
    }
}
