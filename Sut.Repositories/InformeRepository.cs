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
    public class InformeRepository : BaseRepository<Informe>, IInformeRepository
    {
        public InformeRepository(IContext context) : base(context) { }

        public Informe GetOne(long Informeid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Informe
                        .SingleOrDefault(x => x.InformeID == Informeid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

       public List<Informe> listaGetOne(long Informeid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Informe
                        .Where(x => x.Estado == "1").ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Informe LsitaGetOne()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Informe
                        .SingleOrDefault(x => x.Estado == "1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Informe> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.Informe 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public new void Save(Informe obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.InformeID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
