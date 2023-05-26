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
    public class MenuayudaRepository : BaseRepository<Menuayuda>, IMenuayudaRepository
    {
        private readonly ILogService<MenuayudaRepository> _log;

        public MenuayudaRepository(IContext context) : base(context) {
            _log = new LogService<MenuayudaRepository>();
        }



        
        public List<Menuayuda> GetByMenuayuda(long EntidadId, int Ruta, int menuid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Menuayuda
                        .Where(x => x.EntidadId == EntidadId && x.Estado==1 && x.Ruta==Ruta && x.MenuId== menuid)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


      


        public List<Menuayuda> GetByEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Menuayuda
                         .Include(x => x.Usuario) 
                        //.Include(x => x.MiembroEquipo)
                        .Include("Usuario.MiembroEquipo")
                        .Include("Usuario.MiembroEquipo.RolEquipo")
                        //.Include("Usuario.MiembroEquipo")
                        .Where(x => x.EntidadId == EntidadId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Menuayuda GetByone(int menuid, long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Menuayuda 
                        .FirstOrDefault(x => x.MenuId == menuid && x.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Menuayuda GetByoneid(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Menuayuda
                        .FirstOrDefault(x => x.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        

        public new void Save(Menuayuda obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
