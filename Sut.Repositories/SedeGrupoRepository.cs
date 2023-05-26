using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class SedeGrupoRepository : BaseRepository<SedeGrupo>, ISedeGrupoRepository
    {
        public SedeGrupoRepository(IContext context) : base(context) { }

        public SedeGrupo GetOne(long SedeGrupoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.SedeGrupo
                        .Include("Distrito.Provincia.Departamento")
                        .Include("SedeGrupoUnidadOrganica.UnidadOrganica")
                        .SingleOrDefault(x => x.SedeGrupoId == SedeGrupoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<SedeGrupo> GetAll(long EntidadId,long SedePadreId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.SedeGrupo
                        .Include("Sede.Distrito.Provincia.Departamento")
                        .Include("Sede.SedeUnidadOrganica.UnidadOrganica")
                         .Where(x => x.Sede.EntidadId == EntidadId && x.SedePadreId == SedePadreId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SedeGrupo> GetAllgrupo(long SedePadreId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.SedeGrupo
                        .Include("Distrito.Provincia.Departamento")
                        .Include("SedeGrupoUnidadOrganica.UnidadOrganica")
                        .Where(x => x.SedePadreId == SedePadreId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void Eliminar(long SedeGrupoId)
        {
            try
            {
              

                SutContext ctx = Context.GetContext() as SutContext;
                var SedeGrupo = ctx.SedeGrupo.Where(x => x.SedePadreId == SedeGrupoId);

                foreach (SedeGrupo o in SedeGrupo)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarGrupo(long SedeGrupoId)
        {
            try
            {


                SutContext ctx = Context.GetContext() as SutContext;
                var SedeGrupo = ctx.SedeGrupo.Where(x => x.SedeGrupoId == SedeGrupoId);

                foreach (SedeGrupo o in SedeGrupo)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(List<SedeGrupo> obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                //ctx.Entry(obj).State = obj.id > 0 ? EntityState.Modified : EntityState.Added;
                foreach (SedeGrupo o in obj)
                {
                    ctx.Entry(o).State = EntityState.Added;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
