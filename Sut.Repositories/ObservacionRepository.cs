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
    public class ObservacionRepository : BaseRepository<Observacion>, IObservacionRepository
    {
        public ObservacionRepository(IContext context) : base(context) { }

        public Observacion GetOne(long Observacionid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Observacion
                        .SingleOrDefault(x => x.ObservacionId == Observacionid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public Observacion GetOneGlobalobs(Observacion filtro)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Observacion
                        .SingleOrDefault(x => x.ExpedienteId == filtro.ExpedienteId && x.CodValidacion == filtro.CodValidacion && x.Estado == filtro.Estado
                        && x.ProcedimientoId == filtro.ProcedimientoId && x.RequisitoId == filtro.RequisitoId && x.TablaAsmeId == filtro.TablaAsmeId && x.EntidadId == filtro.EntidadId
                        && x.NombreCampo == filtro.NombreCampo && x.BaseLegalId == filtro.BaseLegalId && x.BaseLegalNormaId == filtro.BaseLegalNormaId && x.ActividadId == filtro.ActividadId 
                        && x.RecursoCostoId == filtro.RecursoCostoId && x.RecursoId == filtro.RecursoId && (x.TipoEstado == "1" || x.TipoEstado == "2" || x.TipoEstado == "3"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Observacion GetOneGlobal(Observacion filtro)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Observacion
                        .SingleOrDefault(x => x.ExpedienteId== filtro.ExpedienteId && x.CodValidacion==filtro.CodValidacion && x.Estado==filtro.Estado
                        && x.ProcedimientoId == filtro.ProcedimientoId && x.RequisitoId == filtro.RequisitoId && x.TablaAsmeId == filtro.TablaAsmeId  
                        && x.NombreCampo == filtro.NombreCampo && x.BaseLegalId == filtro.BaseLegalId && x.BaseLegalNormaId == filtro.BaseLegalNormaId && x.ActividadId == filtro.ActividadId && x.RecursoCostoId == filtro.RecursoCostoId && x.RecursoId == filtro.RecursoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
       public List<Observacion> Getlstentidad(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Observacion
                        .Where(x => x.EntidadId == EntidadId && x.Estado == "1")
                          .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Observacion> GetlstentidadExp(long EntidadId, long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Observacion
                        .Where(x => x.EntidadId == EntidadId && x.Estado == "1" && x.ExpedienteId== ExpedienteId)
                          .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        public List<Observacion> GetlstentidadTotal(long EntidadId, string Pantalla)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Observacion
                        .Where(x => x.EntidadId == EntidadId && x.CodValidacion == "2" && x.Estado == "1" &&  x.Pantalla == Pantalla)
                          .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Observacion> GetOneVerObservacion(Observacion filtro)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Observacion
                        .Where( x => x.ExpedienteId == filtro.ExpedienteId && x.Estado == filtro.Estado && x.EntidadId == filtro.EntidadId && x.CodValidacion==filtro.CodValidacion
                        && x.ProcedimientoId == filtro.ProcedimientoId && x.Pantalla == filtro.Pantalla && (x.TipoEstado == "1"|| x.TipoEstado == "2" || x.TipoEstado == "3")) 
                          .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Observacion> GetOneGlobalObservacion(Observacion filtro)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Observacion
                        .Where(x => x.ExpedienteId == filtro.ExpedienteId && x.Estado == filtro.Estado
                       && x.ProcedimientoId == filtro.ProcedimientoId && x.Pantalla == filtro.Pantalla)
                          .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Observacion LsitaGetOne()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Observacion
                        .SingleOrDefault(x => x.Estado == "1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Observacion> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.Observacion
                        .Include("Expediente")
                        .Include("Entidad") 
                        .Where(x=>x.TipoEstado=="2" && x.CodValidacion == "5")                        
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public new void Save(Observacion obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.ObservacionId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Observacion> GetByExpedienteListapro(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Observacion
                        .Where(x => x.ExpedienteId == ExpedienteId && x.Estado == "1"
                       && x.CodValidacion == "2" && x.ProcedimientoId != 0) 
                          .ToList();

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
