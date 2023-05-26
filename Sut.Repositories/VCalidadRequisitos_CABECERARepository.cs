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
    public class VCalidadRequisitos_CABECERARepository : BaseRepository<VCalidadRequisitos_CABECERA>, IVCalidadRequisitos_CABECERARepository
    {
        public VCalidadRequisitos_CABECERARepository(IContext context) : base(context) { }

        public VCalidadRequisitos_CABECERA GetOne(long ICODREQUISITO)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos_CABECERA
                        .SingleOrDefault(x => x.ICODCALIDAD == ICODREQUISITO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        public List<VCalidadRequisitos_CABECERA> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.VCalidadRequisitos_CABECERA 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VCalidadRequisitos_CABECERA> GetAllCOD(Int32 CodEntidad)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos_CABECERA
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
