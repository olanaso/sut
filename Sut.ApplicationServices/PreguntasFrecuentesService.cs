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
    public class PreguntasFrecuentesService : IPreguntasFrecuentesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPreguntasFrecuentesRepository _PreguntasFrecuentesRepository;

        public PreguntasFrecuentesService(IUnitOfWork unitOfWork,
                            IPreguntasFrecuentesRepository PreguntasFrecuentesRepository)
        {
            _unitOfWork = unitOfWork;
            _PreguntasFrecuentesRepository = PreguntasFrecuentesRepository;
        }

        public PreguntasFrecuentes GetOne (long PreguntasFrecuentesid)
        {
            try
            {
                return _PreguntasFrecuentesRepository.GetOne(PreguntasFrecuentesid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PreguntasFrecuentes LsitaGetOne( )
        {
            try
            {
                return _PreguntasFrecuentesRepository.LsitaGetOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntasFrecuentes> GetAllLikePagin(PreguntasFrecuentes filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<PreguntasFrecuentes> query = _PreguntasFrecuentesRepository.GetAll();

                var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                            .OrderByDescending(x => x.PreguntasID);

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

        public List<PreguntasFrecuentes> GetAllLikePaginAcordion(PreguntasFrecuentes filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<PreguntasFrecuentes> query = _PreguntasFrecuentesRepository.GetAllAcordion( filtro);

                var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                            .OrderByDescending(x => x.PreguntasID);

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

        public List<PreguntasFrecuentes> GetAllLikePaginEntidad(PreguntasFrecuentes filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<PreguntasFrecuentes> query = _PreguntasFrecuentesRepository.GetAllEntidad(filtro);

                var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                            .OrderByDescending(x => x.Estado);

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

        public void Save(PreguntasFrecuentes obj)
        {
            try
            {
                _PreguntasFrecuentesRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
