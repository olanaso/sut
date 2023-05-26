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
    public class GrupoEntidadRepository : BaseRepository<GrupoEntidad>, IGrupoEntidadRepository
    {
        public GrupoEntidadRepository(IContext context) : base(context) { }

        public GrupoEntidad GetOne(long GrupoEntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.GrupoEntidad
                        .SingleOrDefault(x => x.GrupoEntidadId == GrupoEntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoEntidad> GetAll(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.GrupoEntidad
                        .Where(x => x.EntidadId == EntidadId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(GrupoEntidad obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.GrupoEntidadId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long GrupoEntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vGrupoEntidadId = ctx.GrupoEntidad.Where(x => x.GrupoEntidadId == GrupoEntidadId);

                foreach (GrupoEntidad o in vGrupoEntidadId)
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
