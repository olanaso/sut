using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sut.ApplicationServices
{
    public class SedeGrupoService : ISedeGrupoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISedeGrupoRepository _SedeGrupoRepository;

        public SedeGrupoService(IUnitOfWork unitOfWork,
                                    ISedeGrupoRepository SedeGrupoRepository)
        {
            _unitOfWork = unitOfWork;
            _SedeGrupoRepository = SedeGrupoRepository;
        }

        public List<SedeGrupo> GetAll(long EntidadId)
        {
            try
            {
                return _SedeGrupoRepository.GetAll(EntidadId,0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SedeGrupo GetOne(long SedeGrupoId)
        {
            try
            {
                return _SedeGrupoRepository.GetOne(SedeGrupoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<SedeGrupo> GetAllLikePagin(SedeGrupo filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<SedeGrupo> query = _SedeGrupoRepository.GetAll(filtro.Sede.EntidadId,filtro.SedePadreId);

                
                var result = query.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SedeGrupo> GetAllLikePaginGrupo(SedeGrupo filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<SedeGrupo> query = _SedeGrupoRepository.GetAllgrupo(filtro.SedePadreId);
 
                var result = query.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save(List<SedeGrupo> obj)
        {
            try
            {
                _SedeGrupoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(int SedeGrupoId)
        {
            try
            {
                _SedeGrupoRepository.Eliminar(SedeGrupoId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarGrupo(int SedeGrupoId)
        {
            try
            {
                _SedeGrupoRepository.EliminarGrupo(SedeGrupoId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
