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
    public class AnexoPersonalRepository : BaseRepository<AnexoPersonal>, IAnexoPersonalRepository
    {
        public AnexoPersonalRepository(IContext context) : base(context) { }


        public List<AnexoPersonal> GetAnexoPeronalByExpedienteTablaASMEId(long ExpedienteId, long TablaASMEId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                return ctx.AnexoPersonal
                        .Include("Expediente.Entidad")
                        .Include("TablaAsme.Procedimiento.UndOrgResponsable")
                        .Where(x => x.ExpedienteId == ExpedienteId && x.TablaAsmeId == TablaASMEId)
                        .OrderBy(x => x.TablaAsmeId)
                        .ThenBy(x => x.Nro)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnexoPersonal> GetByExpediente(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 

                return ctx.AnexoPersonal
                        .Include("Expediente.Entidad")
                        .Include("TablaAsme")
                        .Include("TablaAsme.Procedimiento.UndOrgResponsable")
                        .Where(x => x.ExpedienteId == ExpedienteId)
                        .OrderBy(x => x.TablaAsmeId)
                        .ThenBy(x => x.Nro)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AnexoPersonal> GetByExpedientelista(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;



                //var costPersonal = ctx.RecursoCosto
                //                   .Include(x => x.Recurso)
                //                   .Where(x => x.ExpedienteId == ExpedienteId
                //                           && x.Recurso.TipoRecurso == TipoRecurso.Personal);
                //var personal = ctx.ActividadRecurso
                //                .Include("Actividad.UnidadOrganica")
                //                .Include("Recurso")
                //                .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                //                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                //                        && x.Actividad.TipoActividad != TipoActividad.Espera
                //                        && x.Recurso.TipoRecurso == TipoRecurso.Personal && x.Actividad.TablaAsme.Procedimiento.Estado != 3
                //                       );

                //var anexo12 = personal.Join(costPersonal,
                //                       p => p.Actividad.UnidadOrganicaId + "_" + p.RecursoId,
                //                       c => c.UnidadOrganicaId + "_" + c.RecursoId,
                //                       (p, c) => new { Personal = p, Actividad = p.Actividad, Costo = c, UnidadOrganica = p.Actividad.UnidadOrganica, Recurso = p.Recurso })
                //                       ;
                //var anexo1 = personal.Join(costPersonal,
                //                        p => p.Actividad.UnidadOrganicaId + "_" + p.RecursoId,
                //                        c => c.UnidadOrganicaId + "_" + c.RecursoId,
                //                        (p, c) => new { Personal = p, Actividad = p.Actividad, Costo = c, UnidadOrganica = p.Actividad.UnidadOrganica, Recurso = p.Recurso })
                //                        .ToList();


                return ctx.AnexoPersonal
                        .Include("Expediente.Entidad")
                        .Include("TablaAsme.Procedimiento.UndOrgResponsable")
                        .Where(x => x.ExpedienteId == ExpedienteId)
                        .OrderBy(x => x.TablaAsmeId)
                        .ThenBy(x => x.Nro)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

    }
}
