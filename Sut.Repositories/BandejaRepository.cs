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
    public class InductorRepository : BaseRepository<Inductor>, IInductorRepository
    {
        public InductorRepository(IContext context) : base(context) { }

        public Inductor GetOne(long InductorId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Inductor
                        .SingleOrDefault(x => x.InductorId == InductorId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Inductor> GetAll(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Inductor
                        .Where(x => x.EntidadId == EntidadId
                                    && x.Activo)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Inductor obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.InductorId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long InductorId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vInductorId = ctx.Inductor.Where(x => x.InductorId == InductorId);

                foreach (Inductor o in vInductorId)
                {
                    ctx.Entry(o).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Inductor> GetByExpediente(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                long EntidadId = ctx.Expediente.First(x => x.ExpedienteId == ExpedienteId).EntidadId;
                var inductores = ctx.Inductor.Where(x => x.EntidadId == EntidadId);
                if (inductores.Count() > 0)
                {
                    long minId = inductores.Min(x => x.InductorId);
                    // se añadio en el filtro la entidada
                    var ind = ctx.Inductor.SingleOrDefault(x => x.InductorId == minId && x.EntidadId == EntidadId);
                     
                    var listInd = ctx.ActividadRecurso
                            .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                        && x.Recurso.TipoRecurso != TipoRecurso.Personal
                                        && x.Recurso.TipoRecurso != TipoRecurso.MaterialFungible
                                        && x.Recurso.TipoRecurso != TipoRecurso.ServicioIdentificable
                                        && x.Actividad.TablaAsme.Procedimiento.Estado != 3 && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                        && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Ninguna
                                        //&& (
                                        //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                        //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar
                                        //        )
                                        //    ||
                                        //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                        //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar
                                        //        )
                                        //    )
                                        )
                            .Select(x => x.Recurso)
                            .Distinct()
                            .Select(x => x.RecursoCosto.FirstOrDefault(a => a.ExpedienteId == ExpedienteId))
                            .Select(x => x.Inductor)
                            .ToList();

                    listInd.Add(ind);

                    var salir = 0;
                    for (var i = 0; i < listInd.Count; i++)
                    {
                        if (listInd[i] == null) {
                            salir = 1;
                        }

                    }

                    if (salir == 1)
                    { 
                        return listInd
                                .Distinct()
                                //.OrderBy(x => x.Nombre)
                                .ToList();
                    }
                    else
                    { 
                        return listInd
                                .Distinct()
                                .OrderBy(x => x.Nombre)
                                .ToList();
                    }

                }
                else
                {
                    return new List<Inductor>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
