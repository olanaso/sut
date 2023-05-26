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
    public class VCalidadRequisitos_CABECERAEXANTERepository : BaseRepository<VCalidadRequisitos_CABECERAEXANTE>, IVCalidadRequisitos_CABECERAEXANTERepository
    {
        public VCalidadRequisitos_CABECERAEXANTERepository(IContext context) : base(context) { }

        public VCalidadRequisitos_CABECERAEXANTE GetOne(long ICODREQUISITO)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos_CABECERAEXANTE
                        .SingleOrDefault(x => x.ICODCALIDADEXANTE == ICODREQUISITO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        public List<VCalidadRequisitos_CABECERAEXANTE> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.VCalidadRequisitos_CABECERAEXANTE 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VCalidadRequisitos_CABECERAEXANTE> GetAllCOD(Int32 CodEntidad)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.VCalidadRequisitos_CABECERAEXANTE
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
