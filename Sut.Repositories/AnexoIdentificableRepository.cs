using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class AnexoIdentificableRepository : BaseRepository<AnexoIdentificable>, IAnexoIdentificableRepository
    {
        public AnexoIdentificableRepository(IContext context) : base(context) { }
        
       public List<AnexoIdentificable> GetAnexoIdentificableByExpedienteTablaASMEId(long ExpedienteId, TipoRecurso tipo, long TablaASMEId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.AnexoIdentificable
                        .Include("Expediente.Entidad")
                        .Include("TablaAsme.Procedimiento.UndOrgResponsable")
                        .Where(x => x.ExpedienteId == ExpedienteId
                                && x.TipoRecurso == tipo && x.TablaAsmeId==TablaASMEId)
                        .OrderBy(x => x.TablaAsmeId)
                        .ThenBy(x => x.Nro)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AnexoIdentificable> GetByExpediente(long ExpedienteId, TipoRecurso tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var ver = ctx.AnexoIdentificable
                      .Include("Expediente.Entidad")
                      .Include("TablaAsme.Procedimiento.UndOrgResponsable")
                      .Where(x => x.ExpedienteId == ExpedienteId
                              && x.TipoRecurso == tipo)
                      .OrderBy(x => x.TablaAsmeId)
                      .ThenBy(x => x.Nro);

                return ctx.AnexoIdentificable
                        .Include("Expediente.Entidad")
                        .Include("TablaAsme.Procedimiento.UndOrgResponsable")
                        .Where(x => x.ExpedienteId == ExpedienteId
                                && x.TipoRecurso == tipo)
                        .OrderBy(x => x.TablaAsmeId)
                        .ThenBy(x => x.Nro)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
