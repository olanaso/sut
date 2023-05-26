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
    public class VListaEntidadACRRepository : BaseRepository<VListaEntidadACR>, IVListaEntidadACRRepository
    {
        public VListaEntidadACRRepository(IContext context) : base(context) { }

        
       
        public List<VListaEntidadACR> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.VListaEntidadACR
                        .Where(x => x.ActivarACR == 1)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VListaEntidadACR> GetAllproceso()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VListaEntidadACR
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
