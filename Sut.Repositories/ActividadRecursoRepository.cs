using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class ActividadRecursoRepository : BaseRepository<ActividadRecurso>, IActividadRecursoRepository
    {
        public ActividadRecursoRepository(IContext context) : base(context) { }

        public List<ActividadRecurso> GetByActividad(long ActividadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ActividadRecurso
                        .Include("Recurso")
                        .Where(x => x.ActividadId == ActividadId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ActividadRecurso> GetActividadRecursoListaProceso1(long ExpedienteId, TipoRecurso tiporecurso)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
              

                return ctx.ActividadRecurso
                                .Include("Recurso.UnidadMedida")
                                .Include("Actividad.UnidadOrganica")
                                .Include("Actividad.TablaAsme")
                                .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                        && x.Actividad.TipoActividad != TipoActividad.Espera && x.Actividad.TablaAsme.Procedimiento.Estado != 3 
                                        && x.Recurso.TipoRecurso== tiporecurso
                                        )
                                .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadRecurso> GetActividadRecursoLista(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var SADSA = ctx.ActividadRecurso
                        .Include("Actividad.UnidadOrganica")
                        .Include("Recurso")
                        .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                && x.Actividad.TipoActividad != TipoActividad.Espera
                                && x.Recurso.TipoRecurso == TipoRecurso.Personal && x.Actividad.TablaAsme.Procedimiento.Estado != 3
                                );

                return ctx.ActividadRecurso
                        .Include("Actividad.UnidadOrganica")
                        .Include("Recurso")
                        .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                && x.Actividad.TipoActividad != TipoActividad.Espera
                                && x.Recurso.TipoRecurso == TipoRecurso.Personal && x.Actividad.TablaAsme.Procedimiento.Estado != 3
                                )
                           .Distinct()
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadRecurso> GetActividadRecursoLista2(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ActividadRecurso 
                                .Include("Actividad.UnidadOrganica")
                                .Include("Actividad.TablaAsme")
                                .Include("Inductor")
                        .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                    && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                    && x.Actividad.TipoActividad != TipoActividad.Espera && x.Actividad.TablaAsme.Procedimiento.Estado != 3
                                    && (int)x.Recurso.TipoRecurso > 3
                                )
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ActividadRecurso> GetByTablaAsme(long TablaAsmeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var ver = ctx.ActividadRecurso
                        .Include("Recurso")
                        .Include("Actividad.UnidadOrganica")
                        .Include("Actividad")
                        .Where(x => x.Actividad.TablaAsmeId == TablaAsmeId);

                return ctx.ActividadRecurso
                        .Include("Recurso")
                        .Include("Actividad.UnidadOrganica")
                        .Include("Actividad")
                        .Where(x => x.Actividad.TablaAsmeId == TablaAsmeId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ActividadRecurso> GetByTablaAsmeTotal(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var ver = ctx.ActividadRecurso
                           .Include("Recurso")
                           .Include("Actividad.UnidadOrganica")
                           .Include("Actividad")
                           .Where(x => x.Actividad.TablaAsmeId == ExpedienteId);

                return ctx.ActividadRecurso
                        .Include("Recurso")
                        .Include("Actividad.UnidadOrganica")
                        .Include("Actividad.TablaAsme")
                        .Include("Actividad.TablaAsme.Procedimiento")
                        .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 

        public List<Actividad> GetByActividadEntidad(long entidad)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Actividad 
                        .Include("UnidadOrganica")
                        .Where(x => x.UnidadOrganica.EntidadId == entidad)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Actividad> GetDataByTablaAsmeActividad(long TablaAsmeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Actividad
                        .Include("ActividadRecurso")
                        .Include("ActividadRecurso.Recurso")
                        .Include("UnidadOrganica")
                        .Where(x => x.TablaAsmeId == TablaAsmeId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Recurso> GetDataByTablaAsmeRecursos(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Recurso 
                        .Where(x => x.EntidadId == EntidadId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
