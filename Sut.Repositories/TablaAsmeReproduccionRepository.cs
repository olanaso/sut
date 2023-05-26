using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class TablaAsmeReproduccionRepository : BaseRepository<TablaAsmeReproduccion>, ITablaAsmeReproduccionRepository
    {
        public TablaAsmeReproduccionRepository(IContext context) : base(context) { }

        public TablaAsmeReproduccion GetOne(long ReproduccionId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.TablaAsmeReproduccion
                        .SingleOrDefault(x => x.ReproduccionId == ReproduccionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TablaAsmeReproduccion> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.TablaAsmeReproduccion 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TablaAsmeReproduccion> GetAll(long TablaAsmeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.TablaAsmeReproduccion
                        .Where(x=>x.TablaAsmeId == TablaAsmeId)
                        .OrderBy(x => x.ReproduccionId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnexoIdentificable> GetAllAnexoIdentificable(long TablaAsmeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.AnexoIdentificable
                        .Where(x => x.TablaAsmeId == TablaAsmeId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnexoNoIdentificable> GetAllAnexoNoIdentificable(long TablaAsmeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.AnexoNoIdentificable
                        .Where(x => x.TablaAsmeId == TablaAsmeId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnexoPersonal> GetAllAnexoPersonal(long TablaAsmeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.AnexoPersonal
                        .Where(x => x.TablaAsmeId == TablaAsmeId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Actividad> GetAllActividad(long TablaAsmeId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Actividad
                        .Where(x => x.TablaAsmeId == TablaAsmeId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Eliminar(long ReproduccionId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vReproduccionId = ctx.TablaAsmeReproduccion.Where(x => x.ReproduccionId == ReproduccionId);

                foreach (TablaAsmeReproduccion o in vReproduccionId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarAnexoIdentificable(long AnexoIdentificableId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vAnexoIdentificableId = ctx.AnexoIdentificable.Where(x => x.AnexoIdentificableId == AnexoIdentificableId);

                foreach (AnexoIdentificable o in vAnexoIdentificableId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarAnexoNoIdentificable(long AnexoNoIdentificableId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vAnexoNoIdentificable = ctx.AnexoNoIdentificable.Where(x => x.AnexoNoIdentificableId == AnexoNoIdentificableId);

                foreach (AnexoNoIdentificable o in vAnexoNoIdentificable)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarAnexoPersonal(long AnexoPersonalId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vAnexoPersonal = ctx.AnexoPersonal.Where(x => x.AnexoPersonalId == AnexoPersonalId);

                foreach (AnexoPersonal o in vAnexoPersonal)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void EliminarActividad(long ActividadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vActividad = ctx.Actividad.Where(x => x.ActividadId == ActividadId);

                foreach (Actividad o in vActividad)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(TablaAsmeReproduccion obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ReproduccionId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
