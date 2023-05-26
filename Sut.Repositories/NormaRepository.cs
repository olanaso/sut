using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class NormaRepository : BaseRepository<Norma>, INormaRepository
    {
        public NormaRepository(IContext context) : base(context) { }

        public Norma GetOne(long NormaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Norma
                        .Include(x => x.ArchivoAdjunto)
                        .SingleOrDefault(x => x.NormaId == NormaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Norma> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Norma
                        .Include(x => x.TipoNorma)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Norma obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.NormaId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
