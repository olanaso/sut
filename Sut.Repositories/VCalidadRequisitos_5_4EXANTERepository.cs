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
    public class VCalidadRequisitos_5_4EXANTERepository : BaseRepository<VCalidadRequisitos_5_4EXANTE>, IVCalidadRequisitos_5_4EXANTERepository
    {
        public VCalidadRequisitos_5_4EXANTERepository(IContext context) : base(context) { }

        public VCalidadRequisitos_5_4EXANTE GetOne(long ICODREQUISITO)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos_5_4EXANTE
                        .SingleOrDefault(x => x.ICODREQUISITOALTEREXANTE == ICODREQUISITO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        public List<VCalidadRequisitos_5_4EXANTE> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.VCalidadRequisitos_5_4EXANTE 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VCalidadRequisitos_5_4EXANTE> GetAllCOD(Int32 CodEntidad)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos_5_4EXANTE
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
