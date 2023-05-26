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
    public class BaseLegalRepository : BaseRepository<BaseLegal>, IBaseLegalRepository
    {
        public BaseLegalRepository(IContext context) : base(context) { }

        public void Save(BaseLegal obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                var oldBL = ctx.BaseLegalNorma.Where(x => x.BaseLegalId == obj.BaseLegalId);
                foreach (BaseLegalNorma bln in obj.BaseLegalNorma)
                    ctx.Entry(bln).State = oldBL.Count(x => x.BaseLegalNormaId == bln.BaseLegalNormaId) > 0 ? EntityState.Modified : EntityState.Added;
                oldBL.ToList()
                    .ForEach(x => {
                        if (ctx.Entry(x).State == EntityState.Unchanged) ctx.Entry(x).State = EntityState.Deleted;
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
