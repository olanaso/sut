using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class TablaAsmeRepository : BaseRepository<TablaAsme>, ITablaAsmeRepository
    {
        public TablaAsmeRepository(IContext context) : base(context) { }

        public TablaAsme GetOne(long TablaAsmeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.TablaAsme
                        .Include(x => x.ArMotivoAdjunto)
                        .Include(x => x.ArchivoAdjunto)
                        .Include("Procedimiento")
                        .Include("Procedimiento.Expediente.Entidad")
                        .Include("Procedimiento.UndOrgResponsable")
                        .Include("Actividad")
                        .SingleOrDefault(x => x.TablaAsmeId == TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Guardar(TablaAsme obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var old = ctx.Actividad.Where(x => x.TablaAsmeId == obj.TablaAsmeId);

                if (obj.Actividad != null)
                {

                    foreach (Actividad item in obj.Actividad)
                        ctx.Entry(item).State = old.Count(x => x.ActividadId == item.ActividadId) > 0 ? EntityState.Modified : EntityState.Added;
                    old.ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });
                }
                else
                {
                    old.ToList()
                       .ForEach(x =>
                       {
                           if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                       });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Guardarexcel(TablaAsme obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var old = ctx.Actividad.Where(x => x.TablaAsmeId == obj.TablaAsmeId);

                if (obj.Actividad != null)
                {

                    foreach (Actividad item in obj.Actividad)
                        ctx.Entry(item).State = old.Count(x => x.ActividadId == item.ActividadId) > 0 ? EntityState.Modified : EntityState.Added;
                    old.ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });
                }
                else
                {
                    old.ToList()
                       .ForEach(x =>
                       {
                           if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                       });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Guardardelete(TablaAsme obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var old = ctx.Actividad.Where(x => x.TablaAsmeId == obj.TablaAsmeId);

                foreach (Actividad item in obj.Actividad)
                    ctx.Entry(item).State = old.Count(x => x.ActividadId == 0) > 0 ? EntityState.Modified : EntityState.Added;
               
                old.ToList()
                    .ForEach(x =>
                    {
                        if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                    }
                    );

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void  delete(TablaAsme obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var old = ctx.Actividad.Where(x => x.TablaAsmeId == obj.TablaAsmeId);

                foreach (Actividad item in obj.Actividad)
                    ctx.Entry(item).State = old.Count(x => x.ActividadId == 0) > 0 ? EntityState.Modified : EntityState.Added;

                old.ToList()
                    .ForEach(x =>
                    {
                        if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                    }
                    );

                 
                var vReproduccionId = ctx.TablaAsme.Where(x => x.TablaAsmeId == obj.TablaAsmeId);

                foreach (TablaAsme o in vReproduccionId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TablaAsme> GetResumenCostos(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.TablaAsme
                        .Include("Procedimiento.UndOrgResponsable") 
                        .Include("UIT") 
                        .Where(x => x.Procedimiento.ExpedienteId == ExpedienteId
                                && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion && x.Procedimiento.Estado!= 3 && x.UIT != null
                                //&& (
                                //    (x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal 
                                //     && x.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                                //    ||
                                //    (x.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar 
                                //     && x.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                                //)
                                )
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TablaAsme> GetByExpediente(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.TablaAsme
                        .Include(x => x.Procedimiento)
                        .Where(x => x.Procedimiento.ExpedienteId == ExpedienteId
                            && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TablaAsme> GetByExpedienteSinEliminados(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var listo = ctx.TablaAsme
                        .Include(x => x.Procedimiento)
                        .Include(x => x.Procedimiento.UndOrgResponsable)
                        .Where(x => x.Procedimiento.ExpedienteId == ExpedienteId
                            && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion && x.Procedimiento.Estado != 3)
                            .OrderBy(x => x.Procedimiento.UndOrgResponsable.Nombre);

                return ctx.TablaAsme
                        .Include(x => x.Procedimiento)
                        .Include(x => x.Procedimiento.UndOrgResponsable)
                        .Where(x => x.Procedimiento.ExpedienteId == ExpedienteId
                            && x.Procedimiento.Operacion != OperacionExpediente.Eliminacion && x.Procedimiento.Estado != 3)
                            .OrderBy(x=> x.Procedimiento.UndOrgResponsable.Nombre)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TablaAsme> GetByMef()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                //return ctx.TablaAsme
                //        .Include(x => x.Procedimiento)
                //        .Include(x => x.UIT)
                //        .Include("Procedimiento.Expediente")
                //          .Where(x => x.AutorizacionMEF == Autorizacionmef.Autorizar || x.AutorizacionMEF == Autorizacionmef.Enviar || x.AutorizacionMEF == Autorizacionmef.NoAutorizar)
                //        .ToList();

                return ctx.TablaAsme
                       .Include(x => x.Procedimiento)
                       .Include(x => x.UIT)
                       .Include("Procedimiento.Expediente")
                         .Where(x =>  x.AutorizacionMEF == Autorizacionmef.Enviar )
                       .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarSubvencion(List<TablaAsme> lstSubvencion)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                long[] ids = lstSubvencion.Select(x => x.TablaAsmeId).ToArray();
                var lista = ctx.TablaAsme.Where(x => ids.Contains(x.TablaAsmeId));
                foreach (var asme in lista)
                {
                    var modif = lstSubvencion.First(x => x.TablaAsmeId == asme.TablaAsmeId);
                    asme.DerechoTramitacion = modif.DerechoTramitacion;
                    asme.EsSubvencion = modif.EsSubvencion;
                    ctx.Entry(asme).State = EntityState.Modified;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Save(TablaAsme obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.TablaAsmeId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
