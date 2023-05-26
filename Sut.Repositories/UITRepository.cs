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
    public class UITRepository : BaseRepository<UIT>, IUITRepository
    {
        public UITRepository(IContext context) : base(context) { }

        public UIT GetOne(long UITid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.UIT
                        .SingleOrDefault(x => x.UITID == UITid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UIT LsitaGetOne()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.UIT
                        .SingleOrDefault(x => x.Estado == "1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UIT> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.UIT 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public new void Save(UIT obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.UITID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
