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
    public class VCalidadRepository : BaseRepository<VCalidad>, IVCalidadRepository
    {
        public VCalidadRepository(IContext context) : base(context) { }

        public VCalidad GetOne(long ICODCALIDAD)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidad
                        .SingleOrDefault(x => x.ICODCALIDAD == ICODCALIDAD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
        public List<VCalidad> GetAll(string CodEntidad)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.VCalidad 
                        .Include(x=> x.CalidadRequisitos)
                        .Where(x=> x.CodEntidad== CodEntidad)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
         
     
    }
}
