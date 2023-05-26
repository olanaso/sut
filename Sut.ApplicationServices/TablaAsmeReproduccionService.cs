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
    public class TablaAsmeReproduccionService : ITablaAsmeReproduccionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITablaAsmeReproduccionRepository _TablaAsmeReproduccionRepository;

        public TablaAsmeReproduccionService(IUnitOfWork unitOfWork,
                                    ITablaAsmeReproduccionRepository TablaAsmeReproduccionRepository)
        {
            _unitOfWork = unitOfWork;
            _TablaAsmeReproduccionRepository = TablaAsmeReproduccionRepository;
        }

        public List<TablaAsmeReproduccion> GetAll()
        {
            try
            {
                return _TablaAsmeReproduccionRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TablaAsmeReproduccion> GetAll(long TablaAsmeId)
        {
            try
            {
                return _TablaAsmeReproduccionRepository.GetAll(TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnexoIdentificable> GetAllAnexoIdentificable(long TablaAsmeId)
        {
            try
            {
                return _TablaAsmeReproduccionRepository.GetAllAnexoIdentificable(TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnexoNoIdentificable> GetAllAnexoNoIdentificable(long TablaAsmeId)
        {
            try
            {
                return _TablaAsmeReproduccionRepository.GetAllAnexoNoIdentificable(TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AnexoPersonal> GetAllAnexoPersonal(long TablaAsmeId)
        {
            try
            {
                return _TablaAsmeReproduccionRepository.GetAllAnexoPersonal(TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Actividad> GetAllActividad(long TablaAsmeId)
        {
            try
            {
                return _TablaAsmeReproduccionRepository.GetAllActividad(TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TablaAsmeReproduccion GetOne(long TablaAsmeReproduccionId)
        {
            try
            {
                return _TablaAsmeReproduccionRepository.GetOne(TablaAsmeReproduccionId);
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
                _TablaAsmeReproduccionRepository.Eliminar(ReproduccionId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarAnexoIdentificable(long AnexoIdentificableId)
        {
            try
            {
                _TablaAsmeReproduccionRepository.EliminarAnexoIdentificable(AnexoIdentificableId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarAnexoNoIdentificable(long AnexoNoIdentificableId)
        {
            try
            {
                _TablaAsmeReproduccionRepository.EliminarAnexoNoIdentificable(AnexoNoIdentificableId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarAnexoPersonal(long AnexoPersonalId)
        {
            try
            {
                _TablaAsmeReproduccionRepository.EliminarAnexoPersonal(AnexoPersonalId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarActividad(long ActividadId)
        {
            try
            {
                _TablaAsmeReproduccionRepository.EliminarActividad(ActividadId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(TablaAsmeReproduccion obj)
        {
            try
            {
                _TablaAsmeReproduccionRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
