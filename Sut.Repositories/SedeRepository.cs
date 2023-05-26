using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class SedeRepository : BaseRepository<Sede>, ISedeRepository
    {
        public SedeRepository(IContext context) : base(context) { }

        public Sede GetOne(long SedeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Sede
                        .Include("Distrito.Provincia.Departamento")
                        .Include("SedeUnidadOrganica.UnidadOrganica")
                        .SingleOrDefault(x => x.SedeId == SedeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Sede> GetAll(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Sede
                        .Include("Distrito.Provincia.Departamento")
                        .Include("SedeUnidadOrganica.UnidadOrganica")
                        .Where(x => x.EntidadId == EntidadId
                                    && x.Activo)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
       public List<Sede> GetAllgrupoCarga(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Sede
                        .Include("Distrito.Provincia.Departamento")
                        .Include("SedeUnidadOrganica.UnidadOrganica")
                        .Where(x => x.EntidadId == EntidadId && x.Tipogrupo == 0
                                    && x.Activo)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Sede> GetAllgrupo(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Sede
                        .Include("Distrito.Provincia.Departamento")
                        .Include("SedeUnidadOrganica.UnidadOrganica")
                        .Where(x => x.EntidadId == EntidadId && x.Tipogrupo == 1
                                    && x.Activo)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(int SedeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var sede = ctx.Sede.Where(x => x.SedeId == SedeId);

                foreach (Sede o in sede)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Sede obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var oldUO = ctx.SedeUnidadOrganica.Where(x => x.SedeId == obj.SedeId).ToList();
                foreach (SedeUnidadOrganica uo in obj.SedeUnidadOrganica)
                    if (oldUO.Count(x => x.UnidadOrganicaId == uo.UnidadOrganicaId) == 0)
                        ctx.Entry(uo).State = EntityState.Added;
                oldUO.ForEach(x => {
                    if (obj.SedeUnidadOrganica.Count(s => s.UnidadOrganicaId == x.UnidadOrganicaId) == 0) ctx.Entry(x).State = EntityState.Deleted;
                });

                ctx.Entry(obj).State = obj.SedeId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
