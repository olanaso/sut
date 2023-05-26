using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using Sut.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sut.Repositories
{
    public class MaestroFijoRepository : BaseRepository<MaestroFijo>, IMaestroFijoRepository
    {
        private readonly ILogService<MaestroFijoRepository> _log;

        public MaestroFijoRepository(IContext context) : base(context)
        {
            _log = new LogService<MaestroFijoRepository>();
        }

        public MaestroFijo GetOne(long MaestroFijoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MaestroFijo
                        .Include(x => x.MaestroFijoDatoAdicional)
                        .Include(x => x.MaestroFijoSede)
                        .Include(x => x.MaestroFijoPasoSeguir)
                        .Include(x => x.MaestroFijoNotaCiudadano)
                        .SingleOrDefault(x => x.MaestroFijoId == MaestroFijoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MaestroFijo GetOneByEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MaestroFijo
                        .Include(x => x.MaestroFijoDatoAdicional)
                        .Include(x => x.MaestroFijoSede)
                        .Include(x => x.MaestroFijoPasoSeguir)
                        .Include(x => x.MaestroFijoNotaCiudadano)
                        .Include("MaestroFijoSede.MaestroFijoUndOrgRecepcionDocum")
                        .SingleOrDefault(x => x.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveOnlyEntidadMaestro(MaestroFijo obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.MaestroFijoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public MaestroFijo GetOneByEntidadMaestro(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.MaestroFijo 
                        .SingleOrDefault(x => x.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Save(MaestroFijo obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                if (obj.MaestroFijoId > 0)
                {
                    //var oldSede = ctx.MaestroFijoSede.Where(x => x.MaestroFijoId == obj.MaestroFijoId);
                    //foreach (MaestroFijoSede sede in obj.MaestroFijoSede)
                    //    ctx.Entry(sede).State = oldSede.Count(x => x.SedeId == sede.SedeId) > 0 ? EntityState.Modified : EntityState.Added;
                    //oldSede.ToList()
                    //    .ForEach(x =>
                    //    {
                    //        if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                    //    });
                    var oldSede = ctx.MaestroFijoSede.Where(x => x.MaestroFijoId == obj.MaestroFijoId);
                    var oldUndOrg = ctx.MaestroFijoUndOrgRecepcionDocum.Where(x => x.MaestroFijoId == obj.MaestroFijoId);

                    foreach (MaestroFijoSede mf in obj.MaestroFijoSede)
                    {
                        if (mf.MaestroFijoUndOrgRecepcionDocum != null)
                            foreach (MaestroFijoUndOrgRecepcionDocum p in mf.MaestroFijoUndOrgRecepcionDocum)
                            {
                                ctx.Entry(p).State = EntityState.Added;
                            }
                    }
                    oldUndOrg.ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });



                    foreach (MaestroFijoSede sede in obj.MaestroFijoSede)
                        ctx.Entry(sede).State = oldSede.Count(x => x.SedeId == sede.SedeId) > 0 ? EntityState.Modified : EntityState.Added;
                    oldSede.ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });

                    var oldPasoSeguir = ctx.MaestroFijoPasoSeguir.Where(x => x.MaestroFijoId == obj.MaestroFijoId);
                    foreach (MaestroFijoPasoSeguir paso in obj.MaestroFijoPasoSeguir)
                        ctx.Entry(paso).State = oldPasoSeguir.Count(x => x.PasoSeguirId == paso.PasoSeguirId) > 0 ? EntityState.Modified : EntityState.Added;
                    oldPasoSeguir.ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });

                    var oldDatoAdicional = ctx.MaestroFijoDatoAdicional.Where(x => x.MaestroFijoId == obj.MaestroFijoId);
                    foreach (MaestroFijoDatoAdicional da in obj.MaestroFijoDatoAdicional)
                        ctx.Entry(da).State = oldDatoAdicional.Count(x => x.MetaDatoId == da.MetaDatoId) > 0 ? EntityState.Modified : EntityState.Added;
                    oldDatoAdicional.ToList()
                        .ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });


                    var oldNotaCiudadano = ctx.MaestroFijoNotaCiudadano.Where(x => x.MaestroFijoId == obj.MaestroFijoId);

                    foreach (MaestroFijoNotaCiudadano cd in obj.MaestroFijoNotaCiudadano)
                        ctx.Entry(cd).State = oldNotaCiudadano.Count(x => x.NotaCiudadanoId == cd.NotaCiudadanoId) > 0 ? EntityState.Modified : EntityState.Added;

                    oldNotaCiudadano.ToList().
                        ForEach(x =>
                        {
                            if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                        });


                }

                ctx.Entry(obj).State = obj.MaestroFijoId > 0 ? EntityState.Modified : EntityState.Added;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FijarDatoAdicional(long ExpedienteId, MaestroFijoDatoAdicional da)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var ProcIds = ctx.Procedimiento
                    .Where(x => x.ExpedienteId == ExpedienteId && x.Operacion != OperacionExpediente.Eliminacion)
                    .Select(x => x.ProcedimientoId)
                    .ToList();

                var das = ctx.ProcedimientoDatoAdicional
                    .Where(x => ProcIds.Contains(x.ProcedimientoId) && x.MetaDatoId == da.MetaDatoId)
                    .ToList();

                var idsExistentes = das.Select(x => x.ProcedimientoId).ToList();

                var idsNoExistentes = ProcIds.Where(x => !idsExistentes.Contains(x)).ToList();

                foreach (var a in das)
                {
                    a.Comentario = da.Comentario;
                    a.Checked = da.Checked; 
                    ctx.Entry(a).State = EntityState.Modified;
                }
                foreach (var id in idsNoExistentes)
                {
                    ProcedimientoDatoAdicional item = new ProcedimientoDatoAdicional()
                    {
                        ProcedimientoId = id,
                        Comentario = da.Comentario,
                        MetaDatoId = da.MetaDatoId,
                        Checked = da.Checked
                    };
                    ctx.Entry(item).State = EntityState.Added;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public void FijarDatoConsultaTramite(long ExpedienteId, MaestroFijo model)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var lista = ctx.Procedimiento
                    .Where(x => x.ExpedienteId == ExpedienteId && x.Operacion != OperacionExpediente.Eliminacion);

                foreach (var p in lista)
                {
                    if (!string.IsNullOrEmpty(model.Telefono))
                    {
                        p.Telefono = model.Telefono;
                        ctx.Entry(p).State = EntityState.Modified;
                    }
                    if (!string.IsNullOrEmpty(model.Anexo))
                    {
                        p.Anexo = model.Anexo;
                        ctx.Entry(p).State = EntityState.Modified;
                    }
                    if (!string.IsNullOrEmpty(model.Correo))
                    {
                        p.Correo = model.Correo;
                        ctx.Entry(p).State = EntityState.Modified;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public void FijarSede(long ExpedienteId, MaestroFijoSede sede)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var ProcIds = ctx.Procedimiento
                    .Where(x => x.ExpedienteId == ExpedienteId && x.Operacion != OperacionExpediente.Eliminacion)
                    .Select(x => x.ProcedimientoId)
                    .ToList();

                var sedes = ctx.ProcedimientoSede
                    .Where(x => ProcIds.Contains(x.ProcedimientoId) && x.SedeId == sede.SedeId)
                    .ToList();

                var idsExistentes = sedes.Select(x => x.ProcedimientoId).ToList();

                var idsNoExistentes = ProcIds.Where(x => !idsExistentes.Contains(x)).ToList();

                foreach (var id in idsNoExistentes)
                {
                    ProcedimientoSede item = new ProcedimientoSede()
                    {
                        ProcedimientoId = id,
                        SedeId = sede.SedeId
                    };
                    ctx.Entry(item).State = EntityState.Added;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public void FijarPasoSeguir(long ExpedienteId, MaestroFijoPasoSeguir ps)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var procs = ctx.Procedimiento
                    .Include(x => x.PasoSeguir)
                    .Where(x => x.ExpedienteId == ExpedienteId && x.Operacion != OperacionExpediente.Eliminacion)
                    .ToList();

                var ProcIds = procs.Select(x => x.ProcedimientoId).ToList();

                foreach (var id in ProcIds)
                {
                    PasoSeguir item = new PasoSeguir()
                    {
                        PasoSeguirId = procs.First(x => x.ProcedimientoId == id).PasoSeguir.Count() + 1,
                        ProcedimientoId = id,
                        Descripcion = ps.Descripcion
                    };
                    ctx.Entry(item).State = EntityState.Added;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public void FijarNotaCiudadano(long ExpedienteId, MaestroFijoNotaCiudadano ps)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var procs = ctx.Procedimiento
                    .Include(x => x.NotaCiudadano)
                    .Where(x => x.ExpedienteId == ExpedienteId && x.Operacion != OperacionExpediente.Eliminacion)
                    .ToList();

                var ProcIds = procs.Select(x => x.ProcedimientoId).ToList();

                foreach (var id in ProcIds)
                {
                    NotaCiudadano item = new NotaCiudadano()
                    {
                        NotaCiudadanoId = procs.First(x => x.ProcedimientoId == id).NotaCiudadano.Count() + 1,
                        ProcedimientoId = id,
                        Nota = ps.Comentario,
                        TipoNotaId = ps.MetaDatoTipoNotaId
                        
                    };
                    ctx.Entry(item).State = EntityState.Added;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }
    }
}
