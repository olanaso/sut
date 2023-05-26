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
    public class PlazoAtencionService : IPlazoAtencionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlazoAtencionRepository _PlazoAtencionRepository;

        public PlazoAtencionService(IUnitOfWork unitOfWork,
                                    IPlazoAtencionRepository PlazoAtencionRepository)
        {
            _unitOfWork = unitOfWork;
            _PlazoAtencionRepository = PlazoAtencionRepository;
        }

        public List<PlazoAtencion> GetAll()
        {
            try
            {
                return _PlazoAtencionRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlazoAtencion> GetAll(long TablaAsmeId)
        {
            try
            {
                return _PlazoAtencionRepository.GetAll(TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlazoAtencion GetOne(long PlazoAtencionId)
        {
            try
            {
                return _PlazoAtencionRepository.GetOne(PlazoAtencionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long ReproduccionId)
        {
            try
            {
                _PlazoAtencionRepository.Eliminar(ReproduccionId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(PlazoAtencion obj)
        {
            try
            {
                _PlazoAtencionRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
