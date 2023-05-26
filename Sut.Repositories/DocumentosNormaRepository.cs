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
    public class DocumentosNormaRepository : BaseRepository<DocumentosNorma>, IDocumentosNormaRepository
    {
        public DocumentosNormaRepository(IContext context) : base(context) { }

        public DocumentosNorma GetOne(long DocumentosNormaid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.DocumentosNorma
                       .Include(x => x.ArchivoAdjunto)
                        .SingleOrDefault(x => x.DocumentosNormaID == DocumentosNormaid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentosNorma LsitaGetOne()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.DocumentosNorma
                        .SingleOrDefault(x => x.Estado == Respuesta.Si);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DocumentosNorma> GetAll()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                 
                return ctx.DocumentosNorma 
                            .Include(x => x.ArchivoAdjunto) 
                        .Where(x => x.Estado == Respuesta.Si)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentosNorma> GetAllAdmin()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.DocumentosNorma
                    .Include(x => x.ArchivoAdjunto)
                            .Include(x => x.ArchivoAdjunto) 
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public new void Save(DocumentosNorma obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.DocumentosNormaID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
