using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using Sut.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class IncentivosFormulasCorteRepository : BaseRepository<IncentivosFormulasCorte>, IIncentivosFormulasCorteRepository
    {
        private readonly ILogService<IncentivosFormulasCorteRepository> _log;

        public IncentivosFormulasCorteRepository(IContext context) : base(context) {
            _log = new LogService<IncentivosFormulasCorteRepository>();
        }



        
        public List<IncentivosFormulasCorte> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.IncentivosFormulasCorte  
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        public IncentivosFormulasCorte GetByone(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.IncentivosFormulasCorte
                        .FirstOrDefault(x => x.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public new void Save(IncentivosFormulasCorte obj)
        {
            try
            { 
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.IncentivosFormulasCorteId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
