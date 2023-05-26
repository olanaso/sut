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
    public class UnidadOrganicaRepository : BaseRepository<UnidadOrganica>, IUnidadOrganicaRepository
    {
        public UnidadOrganicaRepository(IContext context) : base(context) { }

        public UnidadOrganica GetOne(long UnidadOrganicaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.UnidadOrganica
                        .SingleOrDefault(x => x.UnidadOrganicaId == UnidadOrganicaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UnidadOrganica> GetAll(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.UnidadOrganica
                        .Where(x => x.EntidadId == EntidadId && x.Activo)
                        .OrderByDescending(x => x.Nombre)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(UnidadOrganica obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.UnidadOrganicaId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UnidadOrganica> GetByExpediente(long ExpedienteId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.ActividadRecurso
                        .Where(x => x.Actividad.TablaAsme.Procedimiento.ExpedienteId == ExpedienteId
                                    && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Eliminacion
                                    && x.Actividad.TablaAsme.Procedimiento.Operacion != OperacionExpediente.Ninguna
                                    && x.Recurso.TipoRecurso != TipoRecurso.Personal
                                    && x.Recurso.TipoRecurso != TipoRecurso.MaterialFungible
                                    && x.Recurso.TipoRecurso != TipoRecurso.ServicioIdentificable && x.Actividad.TablaAsme.Procedimiento.Estado != 3
                                    //&& (
                                    //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Normal
                                    //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento != TipoProcedimiento.Estandar)
                                    //    ||
                                    //        (x.Actividad.TablaAsme.Procedimiento.Expediente.Entidad.TipoTupa == TipoTupa.Estandar
                                    //        && x.Actividad.TablaAsme.Procedimiento.TipoProcedimiento == TipoProcedimiento.Estandar)
                                    //    )
                                    )
                        .Select(x => x.Actividad.UnidadOrganica)
                        .Distinct()
                        .OrderBy(x => x.Nombre)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long UnidadOrganicaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                var vUnidadOrganicaId = ctx.UnidadOrganica.Where(x => x.UnidadOrganicaId == UnidadOrganicaId);

                foreach (UnidadOrganica o in vUnidadOrganicaId)
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
