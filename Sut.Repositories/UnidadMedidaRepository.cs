using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class UnidadMedidaRepository : BaseRepository<UnidadMedida>, IUnidadMedidaRepository
    {
        public UnidadMedidaRepository(IContext context) : base(context) { }

        public UnidadMedida GetOne(long UnidadMedidaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.UnidadMedida
                        .SingleOrDefault(x => x.UnidadMedidaId == UnidadMedidaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UnidadMedida> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.UnidadMedida
                        .Where(x => x.Activo)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(UnidadMedida obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.UnidadMedidaId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
