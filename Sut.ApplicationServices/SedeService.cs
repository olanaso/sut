using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sut.ApplicationServices
{
    public class SedeService : ISedeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISedeRepository _sedeRepository;

        public SedeService(IUnitOfWork unitOfWork,
                                    ISedeRepository sedeRepository)
        {
            _unitOfWork = unitOfWork;
            _sedeRepository = sedeRepository;
        }

        public List<Sede> GetAll(long EntidadId)
        {
            try
            {
                return _sedeRepository.GetAll(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Sede GetOne(long SedeId)
        {
            try
            {
                return _sedeRepository.GetOne(SedeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Sede> GetAllLikePagin(Sede filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Sede> query = _sedeRepository.GetAll(filtro.EntidadId);

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
        
        public List<Sede> GetAllLikePaginGrupo(Sede filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Sede> query = _sedeRepository.GetAllgrupo(filtro.EntidadId);

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
        public List<Sede> GetAllLikePaginCarga(Sede filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Sede> query = _sedeRepository.GetAllgrupoCarga(filtro.EntidadId);

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
        public void Save(Sede obj)
        {
            try
            {
                _sedeRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(int SedeId)
        {
            try
            {
                _sedeRepository.Eliminar(SedeId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
