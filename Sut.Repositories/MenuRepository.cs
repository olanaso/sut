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
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        private readonly ILogService<MenuRepository> _log;

        public MenuRepository(IContext context) : base(context) {
            _log = new LogService<MenuRepository>();
        }

         
  
        public List<Menu> GetByMenu()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Menu
                           .Where(x => x.Orden != 0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Menu> GetAllid(long id)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Menu
                           .Where(x => x.Orden != 0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Menu> GetByMenuidpadre(long menuid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Menu
                        .Where(x => x.MenuId == menuid && x.IdPadreMenu == menuid)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Menu> GetByMenusub()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Menu
                        .Where(x => x.IdPadreMenu != 0)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public new void Save(Menu obj)
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
