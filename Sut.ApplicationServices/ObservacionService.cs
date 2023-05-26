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
    public class ObservacionService : IObservacionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObservacionRepository _ObservacionRepository;

        public ObservacionService(IUnitOfWork unitOfWork,
                            IObservacionRepository ObservacionRepository)
        {
            _unitOfWork = unitOfWork;
            _ObservacionRepository = ObservacionRepository;
        }

        public Observacion GetOne (long Observacionid)
        {
            try
            {
                return _ObservacionRepository.GetOne(Observacionid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
       public Observacion GetOneGlobalobs(Observacion filtro)
        {
            try
            {
                return _ObservacionRepository.GetOneGlobalobs(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Observacion GetOneGlobal(Observacion filtro)
        {
            try
            {
                return _ObservacionRepository.GetOneGlobal(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Observacion> Getlstentidad(long EntidadId)
        {
            try
            {
                return _ObservacionRepository.Getlstentidad(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Observacion> GetlstentidadExp(long EntidadId, long ExpedienteId)
        {
            try
            {
                return _ObservacionRepository.GetlstentidadExp(EntidadId, ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Observacion> GetlstentidadTotal(long EntidadId, string Pantalla)
        {
            try
            {
                return _ObservacionRepository.GetlstentidadTotal(EntidadId, Pantalla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Observacion> GetOneGlobalObservacion(Observacion filtro)
        {
            try
            {
                return _ObservacionRepository.GetOneGlobalObservacion(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Observacion> GetOneVerObservacion(Observacion filtro)
        {
            try
            {
                return _ObservacionRepository.GetOneVerObservacion(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Observacion LsitaGetOne( )
        {
            try
            {
                return _ObservacionRepository.LsitaGetOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Observacion> GetAllLikePagin(Observacion filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Observacion> query = _ObservacionRepository.GetAll();

                var data = query.Distinct().Where(x =>  x.Expediente.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Codigo) ? x.Expediente.Codigo : filtro.Expediente.Codigo).ToUpper())               
                && x.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Entidad.Nombre) ? x.Entidad.Nombre : filtro.Entidad.Nombre).ToUpper())
                && x.ExpedienteId == filtro.ExpedienteId )
                .OrderByDescending(x => x.ObservacionId);



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

        public List<Observacion> GetAllLikePaginGeneral(Observacion filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Observacion> query = _ObservacionRepository.GetAll();

                var data = query.Distinct().Where(x => x.Expediente.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Codigo) ? x.Expediente.Codigo : filtro.Expediente.Codigo).ToUpper())
                && x.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Entidad.Nombre) ? x.Entidad.Nombre : filtro.Entidad.Nombre).ToUpper()))
                .OrderByDescending(x => x.ObservacionId);



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
        public List<Observacion> GetAllLikePaginEntidad(Observacion filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Observacion> query = _ObservacionRepository.GetAll();

                var data = query.Distinct().Where(x => x.Expediente.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Codigo) ? x.Expediente.Codigo : filtro.Expediente.Codigo).ToUpper())
                && x.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Entidad.Nombre) ? x.Entidad.Nombre : filtro.Entidad.Nombre).ToUpper())
                && x.EntidadId == filtro.EntidadId)
                .OrderBy(x => x.ExpedienteId);



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

        public void Save(Observacion obj)
        {
            try
            {
                _ObservacionRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<Observacion> GetByExpedienteListapro(long ExpedienteId)
        {
            try
            {
                return _ObservacionRepository.GetByExpedienteListapro(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
