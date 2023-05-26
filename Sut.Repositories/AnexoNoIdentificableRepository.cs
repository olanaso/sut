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
    public class AnexoNoIdentificableRepository : BaseRepository<AnexoNoIdentificable>, IAnexoNoIdentificableRepository
    {
        public AnexoNoIdentificableRepository(IContext context) : base(context) { }

        public List<AnexoNoIdentificable> GetByExpediente(long ExpedienteId, TipoRecurso tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
      

                return ctx.AnexoNoIdentificable
                        .Include("Expediente.Entidad")
                        .Include("TablaAsme.Procedimiento.UndOrgResponsable")
                        .Where(x => x.ExpedienteId == ExpedienteId
                                && x.TipoRecurso == tipo)
                        .OrderBy(x => x.TablaAsmeId)
                        //.ThenBy(x => Convert.ToString(x.Nombre.ToString()))
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AnexoNoIdentificable> GetByExpedientep1(long ExpedienteId, TipoRecurso tipo)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;


                return ctx.AnexoNoIdentificable
                        .Include("Expediente.Entidad")
                        .Include("TablaAsme.Procedimiento.UndOrgResponsable") 
                        .Where(x => x.ExpedienteId == ExpedienteId
                                && x.TipoRecurso == tipo)
                        .OrderBy(x => x.TablaAsmeId) 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
