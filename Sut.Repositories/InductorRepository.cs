using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class BandejaRepository : BaseRepository<Bandeja>, IBandejaRepository
    {
        public BandejaRepository(IContext context) : base(context) { }

        public Bandeja GetOne(long BandejaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Bandeja
                        .SingleOrDefault(x => x.BandejaId == BandejaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Bandeja> GetAll(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Bandeja

                        .Where(x => x.ExpedienteId == ExpedienteId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Bandeja> GetAllusuario(long UsuarioId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Bandeja

                        .Where(x => x.UsuarioId == UsuarioId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public void Save(Bandeja obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.BandejaId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save2(List<Bandeja> obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                //ctx.Entry(obj).State = obj.id > 0 ? EntityState.Modified : EntityState.Added;
                foreach (Bandeja o in obj)
                {
                    ctx.Entry(o).State = EntityState.Added;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vBandejaId = ctx.Bandeja.Where(x => x.ExpedienteId == ExpedienteId);

                foreach (Bandeja o in vBandejaId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
    }
}
