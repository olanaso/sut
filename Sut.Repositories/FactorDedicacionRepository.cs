using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class FactorDedicacionRepository : BaseRepository<FactorDedicacion>, IFactorDedicacionRepository
    {
        public FactorDedicacionRepository(IContext context) : base(context) { }


        public List<FactorDedicacion> GetByFactorDedicacionF1(long ExpedienteId, TipoRecurso tiporecurso)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.FactorDedicacion
                        .Include(x => x.UnidadOrganica)
                        .Include(x => x.Expediente)
                        .Include(x => x.Recurso)
                        .Where(x => x.ExpedienteId == ExpedienteId
                                && x.Recurso.TipoRecurso == tiporecurso)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<FactorDedicacion> GetByFactorDedicacion(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.FactorDedicacion
                        .Include(x => x.UnidadOrganica)
                        .Include(x => x.Expediente)
                        .Include(x => x.Recurso)
                        .Where(x => x.ExpedienteId == ExpedienteId
                                && x.Recurso.TipoRecurso != TipoRecurso.Personal
                                && x.Recurso.TipoRecurso != TipoRecurso.MaterialFungible
                                && x.Recurso.TipoRecurso != TipoRecurso.ServicioIdentificable) 
                        .ToList(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Guardar(List<FactorDedicacion> lista)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                foreach (FactorDedicacion item in lista)
                {
                    ctx.FactorDedicacion.RemoveRange(ctx.FactorDedicacion.Where(x => x.FactorDedicacionId == item.FactorDedicacionId)); 
                    ctx.Entry(item).State = EntityState.Added;
                    //ctx.Entry(item).State = item.FactorDedicacionId > 0 ? EntityState.Modified : EntityState.Added;
 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

   
        public void Eliminar(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.FactorDedicacion.RemoveRange(ctx.FactorDedicacion.Where(x => x.ExpedienteId == ExpedienteId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
