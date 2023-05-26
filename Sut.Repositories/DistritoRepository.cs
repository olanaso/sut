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
    public class DistritoRepository : BaseRepository<Distrito>, IDistritoRepository
    {
        public DistritoRepository(IContext context) : base(context) { }

        public List<Distrito> GetByProvincia(long ProvinciaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Distrito
                        .Include("Provincia.Departamento")
                        .Where(x => x.ProvinciaId == ProvinciaId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Distrito GetOne(long DistritoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Distrito
                        .Include("Provincia")
                        .SingleOrDefault(x => x.DistritoId == DistritoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Distrito> GetByLista()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Distrito
                        .Include("Provincia.Departamento")
                        .Include("Provincia")
                        .Where(x => x.ProvinciaId != 197)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Distrito> GetByParent(long PadreId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Distrito
                        .Where(x => x.DistritoId == PadreId)
                        .OrderBy(o => o.DistritoId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Distrito obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.DistritoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long DistritoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vDistritoId = ctx.Distrito.Where(x => x.DistritoId == DistritoId);

                foreach (Distrito o in vDistritoId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
