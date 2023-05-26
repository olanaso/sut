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
    public class EnumeradoRepository : BaseRepository<EnumeradoRepository> ,IEnumeradoRepository
    {
        public EnumeradoRepository(IContext context) : base(context) { }

        public List<Enumerado> GetByTipo(TipoEnumerado tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                int t = (int)tipo;
                return ctx.Enumerado
                        .Where(x => (int)x.ID_TIPO_ENUMERADO == t && x.ES_BORRADO == false && x.ES_ACTIVO == true)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
