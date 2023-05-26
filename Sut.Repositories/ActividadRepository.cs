using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Sut.Repositories
{
    public class ActividadRepository : BaseRepository<Actividad>, IActividadRepository
    {
        public ActividadRepository(IContext context) : base(context) { }
        

       public void ActualizarActividad(Actividad obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ActividadId > 0 ? EntityState.Modified : EntityState.Added;

            

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveOnlyActividad(Actividad obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ActividadId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Guardar(Actividad obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var old = ctx.ActividadRecurso.Where(x => x.ActividadId == obj.ActividadId);

                foreach (ActividadRecurso item in obj.ActividadRecurso)
                    ctx.Entry(item).State = old.Count(x => x.RecursoId == item.RecursoId) > 0 ? EntityState.Modified : EntityState.Added;
                old.ToList()
                    .ForEach(x =>
                    {
                        if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                    });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GuardarRecursosEliminar(Actividad obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var old = ctx.Actividad.Where(x => x.ActividadId == obj.ActividadId);
     
                old.ToList()
                    .ForEach(x =>
                    {
                       ctx.Entry(x).State = EntityState.Deleted;
                    });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Actividad> GetAllBy(System.Linq.Expressions.Expression<Func<Actividad, bool>> predicate)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Actividad
                        .Include("TablaAsme.Procedimiento.Expediente")
                        .Where(predicate)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
