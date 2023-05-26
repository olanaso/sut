using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class RecursoCostoRepository : BaseRepository<RecursoCosto>, IRecursoCostoRepository
    {
        public RecursoCostoRepository(IContext context) : base(context) { }

        public List<RecursoCosto> GetCostoPersonal(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var query = ctx.ActividadRecurso
                            .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion && x.Actividad.TablaAsme.Procedimiento.Estado != 3
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Ninguna

                                        //&& (
                                        //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                        //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                                        //    ||
                                        //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                        //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                                        //    )
                                        && x.Recurso.TipoRecurso == TipoRecurso.Personal)
                            .GroupBy(x => new
                            {
                                x.Actividad.UnidadOrganicaId,
                                x.Actividad.UnidadOrganica,
                                x.RecursoId,
                                x.Recurso
                            })
                            .ToList()
                            .Select(x => new RecursoCosto()
                            {
                                RecursoId = x.Key.RecursoId,
                                UnidadOrganicaId = x.Key.UnidadOrganicaId,
                                Recurso = x.Key.Recurso,
                                UnidadOrganica = x.Key.UnidadOrganica
                            });
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<RecursoCosto> GetRecursoCostoLista(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext; 

                var DASDA= ctx.RecursoCosto
                        .Include(x => x.Recurso)
                        .Where(x => x.ExpedienteId == ExpedienteId
                           && x.Recurso.TipoRecurso == TipoRecurso.Personal);

                return ctx.RecursoCosto
                        .Include(x => x.Recurso)
                        .Where(x => x.ExpedienteId == ExpedienteId
                           && x.Recurso.TipoRecurso == TipoRecurso.Personal).Distinct().ToList();
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }
        public List<RecursoCosto> GetRecursoCosto(long ExpedienteId, TipoRecurso tipo)
        {
            try
            {
                var t = (int)tipo;
                SutContext ctx = Context.GetContext() as SutContext;

                var query = ctx.ActividadRecurso
                            .Include("Recurso.UnidadMedida")
                            .Include("Recurso.TipoDepreciacion")
                            .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion && x.Actividad.TablaAsme.Procedimiento.Estado != 3
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Ninguna
                                        //&& (
                                        //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                        //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                                        //    ||
                                        //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                        //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                                        //    )
                                        && (int)x.Recurso.TipoRecurso == t)
                            .GroupBy(x => new
                            {
                                x.RecursoId,
                                x.Recurso,
                                x.Recurso.UnidadMedida,
                                x.Recurso.TipoDepreciacion
                            })
                            .ToList()
                            .Select(x => {
                                RecursoCosto rc = new RecursoCosto()
                                {
                                    RecursoId = x.Key.RecursoId,
                                    Recurso = x.Key.Recurso
                                };
                                rc.Recurso.UnidadMedida = x.Key.UnidadMedida;
                                rc.Recurso.TipoDepreciacion = x.Key.TipoDepreciacion;
                                return rc;
                            });
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Guardar(List<RecursoCosto> lista)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                foreach (RecursoCosto item in lista)
                {
                    ctx.Entry(item).State = item.RecursoCostoId > 0 ? EntityState.Modified : EntityState.Added;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
