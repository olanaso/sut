using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using Sut.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class NotificacionExpedientesRepository : BaseRepository<NotificacionExpedientes>, INotificacionExpedientesRepository
    {
        private readonly ILogService<NotificacionExpedientesRepository> _log;

        public NotificacionExpedientesRepository(IContext context) : base(context) {
            _log = new LogService<NotificacionExpedientesRepository>();
        }



        
        public List<NotificacionExpedientes> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.NotificacionExpedientes
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotificacionExpedientes> GetByListaExpedinte(long expedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.NotificacionExpedientes
                        .Where(x => x.ExpedienteId == expedienteId && x.Estado == 0 && x.EstadoExpediente == 4).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NotificacionExpedientes GetByoneExpedinte(long expedienteId, int nivel)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.NotificacionExpedientes
                        .FirstOrDefault(x => x.ExpedienteId == expedienteId && x.Estado==0 && x.Nivel==nivel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public NotificacionExpedientes GetByoneIdExpEnt(long ExpedientesId, long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.NotificacionExpedientes
                        .FirstOrDefault(x => x.ExpedienteId == ExpedientesId && x.EntidadId== EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NotificacionExpedientes GetByone(long notificacionExpedientesId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.NotificacionExpedientes
                        .FirstOrDefault(x => x.NotificacionExpedientesId == notificacionExpedientesId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public new void Save(NotificacionExpedientes obj)
        {
            try
            { 
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.NotificacionExpedientesId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<NotificacionExpedientes> GetByEntidadRatificadorEval(long? ProvinciaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;



                return ctx.NotificacionExpedientes
                        .Include(x => x.Entidad)
                        .Include(x => x.Expediente)
                        .Where(x => x.ProvinciaId == ProvinciaId && x.Expediente.FecPresentacion != null && x.Estado==0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotificacionExpedientes> GetAllLikePaginEvaluadorPCMEval(EstadoExpediente estadoExpediente)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.NotificacionExpedientes
                        .Include(x => x.Entidad)
                        .Include(x => x.Expediente)
                        .Where(x => x.Expediente.EstadoEvaluadorMinisterio == estadoExpediente && x.Nivel == 2  &&  (x.Entidad.CodPadre == "1" || x.Entidad.CodPadre == "2") && x.Estado == 0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        public List<NotificacionExpedientes> GetAllLikePaginEvaluadorMEFEval(EstadoExpediente estadoExpediente)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.NotificacionExpedientes
                        .Include(x => x.Entidad)
                        .Include(x => x.Expediente)
                        .Where(x => x.Expediente.EstadoEvaluadorMinisterio == estadoExpediente && x.Nivel == 3 && (x.Entidad.CodPadre == "1" || x.Entidad.CodPadre == "2") && x.Estado == 0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotificacionExpedientes> GetByEntidadEvaluadorEval(long SectorId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.NotificacionExpedientes
                    .Include(x => x.Entidad)
                    .Include(x => x.Expediente)
                    //.Where(x => x.SectorId == SectorId && x.Entidad.CodPadre!="2")
                    .Where(x => x.SectorId == SectorId && x.Nivel==1 && x.Expediente.EstadoExpediente != EstadoExpediente.EnProceso && x.Expediente.EstadoExpediente != EstadoExpediente.Anulado && x.Expediente.EstadoExpediente != EstadoExpediente.Publicado && x.Estado==0 )
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotificacionExpedientes> GetByEntidadFiscalizadorEval(EstadoExpediente estadoExpediente)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;



                return ctx.NotificacionExpedientes
                        .Include(x => x.Entidad)
                        .Include(x => x.Expediente)
                        .Where(x => x.Expediente.EstadoExpediente == estadoExpediente && x.Estado == 0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
