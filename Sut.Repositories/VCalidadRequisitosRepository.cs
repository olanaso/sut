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
    public class VCalidadRequisitosRepository : BaseRepository<VCalidadRequisitos>, IVCalidadRequisitosRepository
    {
        public VCalidadRequisitosRepository(IContext context) : base(context) { }

        public VCalidadRequisitos GetOne(long ICODREQUISITO)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos
                        .SingleOrDefault(x => x.ICODREQUISITO == ICODREQUISITO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        public List<VCalidadRequisitos> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.VCalidadRequisitos 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
          
     
    }
}
