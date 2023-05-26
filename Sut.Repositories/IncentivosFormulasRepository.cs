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
    public class IncentivosFormulasRepository : BaseRepository<IncentivosFormulas>, IIncentivosFormulasRepository
    {
        private readonly ILogService<IncentivosFormulasRepository> _log;

        public IncentivosFormulasRepository(IContext context) : base(context) {
            _log = new LogService<IncentivosFormulasRepository>();
        }



        
        public List<IncentivosFormulas> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.IncentivosFormulas  
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        public IncentivosFormulas GetByone(long EntidadId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.IncentivosFormulas
                        .FirstOrDefault(x => x.EntidadId == EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public new void Save(IncentivosFormulas obj)
        {
            try
            { 
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.IncentivosFormulasId > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
