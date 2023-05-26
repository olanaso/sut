using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sut.Repositories
{
    public class UbigeoRepository : BaseRepository<Ubigeo>, IUbigeoRepository
    {
        public UbigeoRepository(IContext context) : base(context) { }

        public List<Ubigeo> GetDepartamentos()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Ubigeo
                        .GroupBy(x => new {
                            DepartamentoId = x.DepartamentoId,
                            Departamento = x.Departamento
                        })
                        .ToList()
                        .Select(x => new Ubigeo()
                        {
                            DepartamentoId = x.Key.DepartamentoId,
                            Departamento = x.Key.Departamento
                        })
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Ubigeo> GetProvincias(int DepartamentoId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Ubigeo
                        .Where(x => x.DepartamentoId == DepartamentoId)
                        .GroupBy(x => new {
                            ProvinciaId = x.ProvinciaId,
                            Provincia = x.Provincia
                        })
                        .ToList()
                        .Select(x => new Ubigeo()
                        {
                            ProvinciaId = x.Key.ProvinciaId,
                            Provincia = x.Key.Provincia
                        })
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Ubigeo> GetDistritos(int DepartamentoId, int ProvinciaId)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Ubigeo
                        .Where(x => x.DepartamentoId == DepartamentoId
                                    && x.ProvinciaId == ProvinciaId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
