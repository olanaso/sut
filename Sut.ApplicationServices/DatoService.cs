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
    public class DatoService : IDatoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatoRepository _datoRepository;

        public DatoService(IUnitOfWork unitOfWork,
                            IDatoRepository datoRepository)
        {
            _unitOfWork = unitOfWork;
            _datoRepository = datoRepository;
        }

        public List<Dato> GetByTipo(TipoDato tipo)
        {
            try
            {
                return _datoRepository.GetByTipo(tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dato GetOne(long MetaDatoId, TipoDato tipo)
        {
            try
            {
                return _datoRepository.GetOne(MetaDatoId, tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Dato obj)
        {
            try
            {
                _datoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
