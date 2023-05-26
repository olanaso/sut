using Sut.IApplicationServices;
using System;
using System.Collections.Generic;
using Sut.Entities;
using Sut.IRepositories;
using System.Linq;

namespace Sut.ApplicationServices
{
    public class UnidadOrganicaService : IUnidadOrganicaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnidadOrganicaRepository _unidadOrganicaRepository;

        public UnidadOrganicaService(IUnitOfWork unitOfWork,
                                    IUnidadOrganicaRepository unidadOrganicaRepository)
        {
            _unitOfWork = unitOfWork;
            _unidadOrganicaRepository = unidadOrganicaRepository;
        }

        public List<UnidadOrganica> GetAll(long EntidadId)
        {
            try
            {
                return _unidadOrganicaRepository.GetAll(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UnidadOrganica GetOne(long UnidadOrganicaId)
        {
            try
            {
                return _unidadOrganicaRepository.GetOne(UnidadOrganicaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UnidadOrganica> GetAllLikePagin(UnidadOrganica filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<UnidadOrganica> query = _unidadOrganicaRepository.GetAll(filtro.EntidadId);

                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper()))
                            .OrderBy(x => x.Nombre);

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

        public void Save(UnidadOrganica obj)
        {
            try
            {
                _unidadOrganicaRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UnidadOrganica> GetByExpediente(long ExpedienteId)
        {
            try
            {
                return _unidadOrganicaRepository.GetByExpediente(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long UnidadOrganicaId)
        {
            try
            {
                _unidadOrganicaRepository.Eliminar(UnidadOrganicaId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
