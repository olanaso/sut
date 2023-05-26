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
    public class IngresoSUTRepository : BaseRepository<IngresoSUT>, IIngresoSUTRepository
    {
        public IngresoSUTRepository(IContext context) : base(context) { }

       

        public new void Save(IngresoSUT obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.IngresoID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
