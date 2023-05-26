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
    public class CorreosRepository : BaseRepository<Correos>, ICorreosRepository
    {
        private readonly ILogService<CorreosRepository> _log;

        public CorreosRepository(IContext context) : base(context) {
            _log = new LogService<CorreosRepository>();
        }



        
        public List<Correos> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Correos  
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
                var vRolId = ctx.Correos.Where(x => x.id_correo == rolId);

                foreach (Correos o in vRolId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Correos GetByone(long RolId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Correos
                        .FirstOrDefault(x => x.id_correo == RolId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 
        public new void Save(Correos obj)
        {
            try
            { 
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.id_correo > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
