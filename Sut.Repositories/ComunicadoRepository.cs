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
    public class ComunicadoRepository : BaseRepository<Comunicado>, IComunicadoRepository
    {
        public ComunicadoRepository(IContext context) : base(context) { }

        public Comunicado GetOne(long Comunicadoid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Comunicado
                        .SingleOrDefault(x => x.ComunicadoID == Comunicadoid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Comunicado LsitaGetOne()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Comunicado
                        .SingleOrDefault(x => x.Estado == Respuesta.Si);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Comunicado> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Comunicado
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Comunicado> GetAllBaner()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Comunicado
                    .Include(x=> x.DocumentosNorma)
                     .Where(x => x.Estado == Respuesta.Si)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Save(Comunicado obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ComunicadoID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
