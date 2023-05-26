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
    public class VCalidadRequisitos_4_2EXANTERepository : BaseRepository<VCalidadRequisitos_4_2EXANTE>, IVCalidadRequisitos_4_2EXANTERepository
    {
        public VCalidadRequisitos_4_2EXANTERepository(IContext context) : base(context) { }

        public VCalidadRequisitos_4_2EXANTE GetOne(long ICODREQUISITO)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos_4_2EXANTE
                        .SingleOrDefault(x => x.ICODREQUISITOEXANTE == ICODREQUISITO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        public List<VCalidadRequisitos_4_2EXANTE> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.VCalidadRequisitos_4_2EXANTE 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VCalidadRequisitos_4_2EXANTE> GetAllCOD(Int32 CodEntidad)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos_4_2EXANTE 
                        .Where(x => x.ICODCALIDADEXANTE == CodEntidad)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
