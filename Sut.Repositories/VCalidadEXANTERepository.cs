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
    public class VCalidadEXANTERepository : BaseRepository<VCalidadEXANTE>, IVCalidadEXANTERepository
    {
        public VCalidadEXANTERepository(IContext context) : base(context) { }

        public VCalidadEXANTE GetOne(long ICODCALIDAD)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadEXANTE
                        .SingleOrDefault(x => x.ICODCALIDADEXANTE == ICODCALIDAD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VCalidadEXANTE> GetAllexante(int ICODCALIDADEXANTE)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
 
                    return ctx.VCalidadEXANTE
                            .Where(x=> x.ICODCALIDADEXANTE == ICODCALIDADEXANTE)
                            .ToList();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<VCalidadEXANTE> GetAll(long CodEntidad)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                if (CodEntidad == 0)
                {

                    return ctx.VCalidadEXANTE 
                            .ToList();
                }
                else {
                    return ctx.VCalidadEXANTE 
                    .Where(x => x.EntidadId == CodEntidad)
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
         
     
    }
}
