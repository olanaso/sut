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
    public class VCalidadRequisitos_4_2Repository : BaseRepository<VCalidadRequisitos_4_2>, IVCalidadRequisitos_4_2Repository
    {
        public VCalidadRequisitos_4_2Repository(IContext context) : base(context) { }

        public VCalidadRequisitos_4_2 GetOne(long ICODREQUISITO)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos_4_2
                        .SingleOrDefault(x => x.ICODREQUISITO == ICODREQUISITO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        public List<VCalidadRequisitos_4_2> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.VCalidadRequisitos_4_2 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VCalidadRequisitos_4_2> GetAllCOD(Int32 CodEntidad)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos_4_2 
                        .Where(x => x.ICODCALIDAD == CodEntidad)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
