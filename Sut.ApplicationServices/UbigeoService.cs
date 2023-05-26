using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class UbigeoService : IUbigeoService
    {
        private readonly IUbigeoRepository _ubigeoRepository;

        public UbigeoService(IUbigeoRepository ubigeoRepository)
        {
            _ubigeoRepository = ubigeoRepository;
        }

        public Ubigeo GetOne(long UbigeoId)
        {
            try
            {
                return _ubigeoRepository.GetOne(x => x.UbigeoId == UbigeoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ubigeo> GetDepartamentos()
        {
            try
            {
                return _ubigeoRepository.GetDepartamentos();
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
                return _ubigeoRepository.GetProvincias(DepartamentoId);
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
                return _ubigeoRepository.GetDistritos(DepartamentoId, ProvinciaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
