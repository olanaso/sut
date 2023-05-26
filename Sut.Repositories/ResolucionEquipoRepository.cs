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
    public class ResolucionEquipoRepository : BaseRepository<ResolucionEquipo>, IResolucionEquipoRepository
    {
        public ResolucionEquipoRepository(IContext context) : base(context) { }

        public IEnumerable<ResolucionEquipo> GetByEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ResolucionEquipo.Where(x => x.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResolucionEquipo GetOne(long ResolucionEquipoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ResolucionEquipo
                            .Include(x => x.ArchivoAdjunto)
                            .SingleOrDefault(x => x.ResolucionEquipoId == ResolucionEquipoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResolucionEquipo GetOneEntidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ResolucionEquipo
                            .Include(x => x.ArchivoAdjunto)
                            .SingleOrDefault(x => x.EntidadId == EntidadId && x.Principal == "1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Save(ResolucionEquipo obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ResolucionEquipoId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
