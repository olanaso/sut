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
    public class InformeDocumentosRepository : BaseRepository<InformeDocumentos>, IInformeDocumentosRepository
    {
        public InformeDocumentosRepository(IContext context) : base(context) { }

        public InformeDocumentos GetOne(long InformeDocumentosid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.InformeDocumentos
                        .SingleOrDefault(x => x.InformeDocumentosID == InformeDocumentosid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InformeDocumentos LsitaGetOne()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.InformeDocumentos
                        .SingleOrDefault(x => x.InformeDocumentosID == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<InformeDocumentos> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.InformeDocumentos 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public new void Save(InformeDocumentos obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.InformeDocumentosID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
