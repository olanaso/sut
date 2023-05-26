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
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnidadMedidaRepository _unidadMedidaRepository;

        public UnidadMedidaService(IUnitOfWork unitOfWork,
                                    IUnidadMedidaRepository unidadMedidaRepository)
        {
            _unitOfWork = unitOfWork;
            _unidadMedidaRepository = unidadMedidaRepository;
        }

        public List<UnidadMedida> GetAll()
        {
            try
            {
                return _unidadMedidaRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UnidadMedida GetOne(long UnidadMedidaId)
        {
            try
            {
                return _unidadMedidaRepository.GetOne(UnidadMedidaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UnidadMedida> GetAllLikePagin(UnidadMedida filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<UnidadMedida> query = _unidadMedidaRepository.GetAll();

                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper())
                                            && x.Abreviatura.ToUpper().Contains((string.IsNullOrEmpty(filtro.Abreviatura) ? x.Abreviatura : filtro.Abreviatura).ToUpper()))
                            .OrderByDescending(x => x.UnidadMedidaId);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(UnidadMedida obj)
        {
            try
            {
                _unidadMedidaRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
