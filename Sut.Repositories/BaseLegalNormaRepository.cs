using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sut.Repositories
{
    public class BaseLegalNormaRepository : BaseRepository<BaseLegalNorma>, IBaseLegalNormaRepository
    {
        public BaseLegalNormaRepository(IContext context) : base(context) { }

        public List<BaseLegalNorma> GetDetails(long BaseLegalId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.BaseLegalNorma
                        .Include("Norma.TipoNorma")
                        .Include("ArchivoAdjunto")
                        .Include("TipoNorma")
                        .Where(x => x.BaseLegalId == BaseLegalId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseLegalNorma GetOne(long BaseLegalId, long BaseLegalNormaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.BaseLegalNorma
                        .Include("Norma.TipoNorma")
                        .Include("ArchivoAdjunto")
                        .Include("TipoNorma")
                        .SingleOrDefault(x => x.BaseLegalId == BaseLegalId
                                            && x.BaseLegalNormaId == BaseLegalNormaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BaseLegalNorma> GetByExpediente(List<long> ids)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.BaseLegalNorma
                        .Include("Norma.TipoNorma")
                        .Include("TipoNorma")
                        .Where(x => ids.Contains(x.BaseLegalId))
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
