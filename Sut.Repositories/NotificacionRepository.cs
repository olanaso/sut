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
    public class NotificacionRepository : BaseRepository<Notificacion>, INotificacionRepository
    {
        private readonly ILogService<NotificacionRepository> _log;

        public NotificacionRepository(IContext context) : base(context) {
            _log = new LogService<NotificacionRepository>();
        }



        
        public List<Notificacion> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Notificacion
                    .Where(x => x.Estado == 1)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 


        public Notificacion GetByone(long notificacionId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Notificacion
                        .FirstOrDefault(x => x.NotificacionId == notificacionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public new void Save(Notificacion obj)
        {
            try
            { 
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.NotificacionId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
