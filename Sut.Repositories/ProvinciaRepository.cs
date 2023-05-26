using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class ProvinciaRepository : BaseRepository<Provincia>, IProvinciaRepository
    {
        public ProvinciaRepository(IContext context) : base(context) { }

        public List<Provincia> GetByDepartamento(long DepartamentoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Provincia
                        .Include(x => x.Departamento)
                        .Where(x => x.DepartamentoId == DepartamentoId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
