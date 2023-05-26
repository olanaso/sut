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
    public class PreguntasFrecuentesRepository : BaseRepository<PreguntasFrecuentes>, IPreguntasFrecuentesRepository
    {
        public PreguntasFrecuentesRepository(IContext context) : base(context) { }

        public PreguntasFrecuentes GetOne(long preguntasID)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.PreguntasFrecuentes
                        .SingleOrDefault(x => x.PreguntasID == preguntasID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PreguntasFrecuentes LsitaGetOne()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.PreguntasFrecuentes
                        .SingleOrDefault(x => x.Estado == "1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PreguntasFrecuentes> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.PreguntasFrecuentes 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntasFrecuentes> GetAllEntidad(PreguntasFrecuentes filtro)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.PreguntasFrecuentes
                      .Where(x => x.EntidadId == filtro.EntidadId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntasFrecuentes> GetAllAcordion(PreguntasFrecuentes filtro)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.PreguntasFrecuentes
                      .Where(x => x.EstadoPublicar == filtro.EstadoPublicar)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public new void Save(PreguntasFrecuentes obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.PreguntasID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
