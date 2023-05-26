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
    public class ArchivoAdjuntoRepository : BaseRepository<ArchivoAdjunto>, IArchivoAdjuntoRepository
    {
        public ArchivoAdjuntoRepository(IContext context) : base(context) { }

        public ArchivoAdjunto GetOne(long ArchivoAdjuntoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ArchivoAdjunto
                        .SingleOrDefault(x => x.ArchivoAdjuntoId == ArchivoAdjuntoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(ArchivoAdjunto obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ArchivoAdjuntoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
