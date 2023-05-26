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
    public class RolesRepository : BaseRepository<Roles>, IRolesRepository
    {
        private readonly ILogService<RolesRepository> _log;

        public RolesRepository(IContext context) : base(context) {
            _log = new LogService<RolesRepository>();
        }



        
        public List<Roles> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Roles  
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void Eliminar(long rolId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vRolId = ctx.Roles.Where(x => x.RolId == rolId);

                foreach (Roles o in vRolId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Roles GetByone(long RolId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Roles
                        .FirstOrDefault(x => x.RolId == RolId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 
        public new void Save(Roles obj)
        {
            try
            { 
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.RolId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
