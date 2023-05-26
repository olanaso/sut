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
    public class DatoRepository : BaseRepository<Dato>, IDatoRepository
    {
        public DatoRepository(IContext context) : base(context) { }

        public Dato GetOne(long MetaDatoId, TipoDato tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Dato
                        .SingleOrDefault(x => x.MetaDatoId == MetaDatoId && x.Tipo == tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Dato> GetByTipo(TipoDato tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                int t = (int)tipo;
                return ctx.Dato
                        .Where(x => (int)x.Tipo == t)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Save(Dato obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.DatoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
