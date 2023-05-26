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
    public class InductorValorRepository : BaseRepository<InductorValor>, IInductorValorRepository
    {
        public InductorValorRepository(IContext context) : base(context) { }

        public void Guardar(List<InductorValor> lista)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                foreach (InductorValor item in lista)
                {
                    ctx.Entry(item).State = item.InductorValorId > 0 ? EntityState.Modified : EntityState.Added;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<InductorValor> GetOneExpediente(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.InductorValor
                         .Include("Inductor")
                         .Include("UnidadOrganica")
                         .Where(x => x.ExpedienteId == ExpedienteId)
                         .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public InductorValor GetOne(long InductorId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.InductorValor
                        .SingleOrDefault(x => x.InductorId == InductorId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InductorValor GetOneValor(long InductorId, long ExpedienteId, long UnidadOrganicaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.InductorValor
                        .SingleOrDefault(x => x.InductorId == InductorId && x.ExpedienteId== ExpedienteId && x.UnidadOrganicaId== UnidadOrganicaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public List<InductorValor> listGetOne(long InductorId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.InductorValor
                        .Where(x => x.InductorId == InductorId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
