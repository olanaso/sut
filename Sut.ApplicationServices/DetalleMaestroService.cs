using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sut.ApplicationServices
{
    public class DetalleMaestroService : IDetalleMaestroService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDetalleMaestroRepository _DetalleMaestroRepository;

        public DetalleMaestroService(IUnitOfWork unitOfWork,
                                    IDetalleMaestroRepository DetalleMaestroService)
        {
            _unitOfWork = unitOfWork;
            _DetalleMaestroRepository = DetalleMaestroService;
        }

        public List<DetalleMaestro> GetAllXTIPO(TipoDetalleMaestro tipo)
        {
            try
            {
                return _DetalleMaestroRepository.GetAllXTIPO(tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DetalleMaestro GetOne(long DetalleMaestroId)
        {
            try
            {
                return _DetalleMaestroRepository.GetOne(DetalleMaestroId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         

        public void Save(DetalleMaestro obj)
        {
            try
            {
                _DetalleMaestroRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long id)
        {
            try
            {
                _DetalleMaestroRepository.Eliminar(id);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
