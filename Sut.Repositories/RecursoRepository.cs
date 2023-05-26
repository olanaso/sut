using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class RecursoRepository : BaseRepository<Recurso>, IRecursoRepository
    {
        public RecursoRepository(IContext context) : base(context) { }

        public Recurso GetOne(long RecursoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Recurso
                        .SingleOrDefault(x => x.RecursoId == RecursoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Recurso> GetAll(long EntidadId, TipoRecurso tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                short o = (short)tipo;

                return ctx.Recurso
                        .Include(x => x.UnidadMedida)
                        .Where(x => x.EntidadId == EntidadId
                                    && (short)x.TipoRecurso == o
                                    && x.Activo)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Recurso> GetAll(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Recurso
                        .Include(x => x.UnidadMedida)
                        .Where(x => x.EntidadId == EntidadId
                                    && x.Activo)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Recurso obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.RecursoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Recurso> GetByExpedienteUndOrganica2(long ExpedienteId,TipoRecurso tiporecurso)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ActividadRecurso
                        .Include(x => x.Actividad)
                        .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                && x.Recurso.TipoRecurso == tiporecurso)
                        .Select(x => x.Recurso)
                        .Distinct()
                        .OrderBy(x => x.TipoRecurso)
                        .ThenBy(x => x.Nombre)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Recurso> GetByExpedienteUndOrganica(long ExpedienteId, long UnidadOrganicaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var asdas = ctx.ActividadRecurso
                        .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                && x.Actividad.UnidadOrganicaId == UnidadOrganicaId
                                && x.Recurso.TipoRecurso != TipoRecurso.Personal
                                && x.Recurso.TipoRecurso != TipoRecurso.MaterialFungible
                                && x.Recurso.TipoRecurso != TipoRecurso.ServicioIdentificable)
                        .Select(x => x.Recurso)
                        .Distinct()
                        .OrderBy(x => x.TipoRecurso)
                        .ThenBy(x => x.Nombre);

                return ctx.ActividadRecurso
                        .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                && x.Actividad.UnidadOrganicaId == UnidadOrganicaId
                                && x.Recurso.TipoRecurso != TipoRecurso.Personal
                                && x.Recurso.TipoRecurso != TipoRecurso.MaterialFungible
                                && x.Recurso.TipoRecurso != TipoRecurso.ServicioIdentificable)
                        .Select(x => x.Recurso)
                        .Distinct()
                        .OrderBy(x => x.TipoRecurso)
                        .ThenBy(x => x.Nombre)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long IdRecurso)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vRecursoId = ctx.Recurso.Where(x => x.RecursoId == IdRecurso);

                foreach (Recurso o in vRecursoId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // procesar factor tupa y no tupa de todos los recursos
        // se agrega el siguiente codigo  && x.Actividad.TipoActividad != TipoActividad.Espera
        public List<Recurso> GetByExpedienteUndOrganicaWithDuracionTotal(long ExpedienteId, long UnidadOrganicaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                //var ver1 = ctx.ActividadRecurso
                //    .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                //           && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                //           && x.Actividad.UnidadOrganicaId == UnidadOrganicaId
                //            //&& x.Actividad.TipoActividad != TipoActividad.Espera
                //           && x.Recurso.TipoRecurso != TipoRecurso.Personal
                //           && x.Recurso.TipoRecurso != TipoRecurso.MaterialFungible
                //           && x.Recurso.TipoRecurso != TipoRecurso.ServicioIdentificable && x.Actividad.TablaAsme.Procedimiento.Estado != 3)
                          
                //      .GroupBy(x => x.Recurso,
                //           x => x.Actividad.Duracion * x.Actividad.TablaAsme.Prestaciones,
                //           (k, g) => new { Recurso = k, Duracion = g.Sum() });



                //var ver = ctx.ActividadRecurso
                //   .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                //           && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                //           && x.Actividad.UnidadOrganicaId == UnidadOrganicaId
                //            //&& x.Actividad.TipoActividad != TipoActividad.Espera
                //           && x.Recurso.TipoRecurso != TipoRecurso.Personal
                //           && x.Recurso.TipoRecurso != TipoRecurso.MaterialFungible
                //           && x.Recurso.TipoRecurso != TipoRecurso.ServicioIdentificable && x.Actividad.TablaAsme.Procedimiento.Estado != 3)
                //   .GroupBy(x => x.Recurso,
                //           x => x.Actividad.Duracion * x.Actividad.TablaAsme.Prestaciones,
                //           (k, g) => new { Recurso = k, Duracion = g.Sum() })
                //   .ToList()
                //   .Select(x =>
                //   {
                //       x.Recurso.DuracionTotal = x.Duracion;
                //       return x.Recurso;
                //   })
                //   .OrderBy(x => x.TipoRecurso)
                //   .ThenBy(x => x.Nombre);



                return ctx.ActividadRecurso
                        .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                && x.Actividad.UnidadOrganicaId == UnidadOrganicaId

                                && x.Actividad.TipoActividad != TipoActividad.Espera

                                && x.Recurso.TipoRecurso != TipoRecurso.Personal
                                && x.Recurso.TipoRecurso != TipoRecurso.MaterialFungible
                                && x.Recurso.TipoRecurso != TipoRecurso.ServicioIdentificable && x.Actividad.TablaAsme.Procedimiento.Estado != 3)
                        .GroupBy(x => x.Recurso,
                                x => x.Actividad.Duracion * x.Actividad.TablaAsme.Prestaciones,
                                (k, g) => new { Recurso = k, Duracion = g.Sum() })
                        .ToList()
                        .Select(x => {
                            x.Recurso.DuracionTotal = x.Duracion;
                            return x.Recurso;
                        })
                        .OrderBy(x => x.TipoRecurso)
                        .ThenBy(x => x.Nombre)
                        .ToList();


             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
