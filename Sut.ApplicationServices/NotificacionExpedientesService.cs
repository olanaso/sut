using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using Sut.Log;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class NotificacionExpedientesService : INotificacionExpedientesService
    {
        private readonly ILogService<NotificacionExpedientesService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificacionExpedientesRepository _NotificacionExpedientesRepository;

        public NotificacionExpedientesService(IUnitOfWork unitOfWork,
                            INotificacionExpedientesRepository NotificacionExpedientesRepository)
        {
            _logger = new LogService<NotificacionExpedientesService>();
            _unitOfWork = unitOfWork;
            _NotificacionExpedientesRepository = NotificacionExpedientesRepository;
        }



        public List<NotificacionExpedientes> GetByNotificacionExpedientes(NotificacionExpedientes filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<NotificacionExpedientes> query = _NotificacionExpedientesRepository.GetAll();

                totalRows = query.Count();
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
        public NotificacionExpedientes GetByoneIdExpEnt(long ExpedientesId, long EntidadId)
        {
            try
            {
                return _NotificacionExpedientesRepository.GetByoneIdExpEnt(ExpedientesId, EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NotificacionExpedientes GetByone(long notificacionExpedientesId)
        {
            try
            {
                return _NotificacionExpedientesRepository.GetByone(notificacionExpedientesId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NotificacionExpedientes GetByoneExpedinte(long expedienteId, int nivel)
        {
            try
            {
                return _NotificacionExpedientesRepository.GetByoneExpedinte(expedienteId, nivel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NotificacionExpedientes> GetByListaExpedinte(long expedienteId)
        {
            try
            {
                return _NotificacionExpedientesRepository.GetByListaExpedinte(expedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public List<NotificacionExpedientes> GetAllLikePaginRatificadorEval(NotificacionExpedientes filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<NotificacionExpedientes> query = _NotificacionExpedientesRepository.GetByEntidadRatificadorEval(filtro.ProvinciaId);

                //var data = query.Where(x => x.Expediente.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Codigo) ? x.Expediente.Codigo : filtro.Expediente.Codigo).ToUpper()))
                //                .OrderByDescending(x => x.ExpedienteId);

                //totalRows = data.Count();
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


        public List<NotificacionExpedientes> GetAllLikePaginEvaluadorMEFPCMEval(NotificacionExpedientes filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<NotificacionExpedientes> query;
                if (filtro.EntidadId == 18497) {
                     query = _NotificacionExpedientesRepository.GetAllLikePaginEvaluadorMEFEval(EstadoExpediente.Aprobado);
                } else
                {
                      query = _NotificacionExpedientesRepository.GetAllLikePaginEvaluadorPCMEval(EstadoExpediente.Aprobado);
                }

                //var data = query.Where(x => x.Expediente.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Codigo) ? x.Expediente.Codigo : filtro.Expediente.Codigo).ToUpper()))
                //            .OrderByDescending(x => x.ExpedienteId);

                //totalRows = data.Count();
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


        public List<NotificacionExpedientes> GetAllLikePaginEvaluadorEval(NotificacionExpedientes filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<NotificacionExpedientes> query = _NotificacionExpedientesRepository.GetByEntidadEvaluadorEval(filtro.SectorId);

                //var data = query.Where(x => x.Expediente.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Codigo) ? x.Expediente.Codigo : filtro.Expediente.Codigo).ToUpper()))
                //          .OrderByDescending(x => x.ExpedienteId);

                //totalRows = data.Count();
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



        public List<NotificacionExpedientes> GetAllLikePaginFiscalizadorEval(NotificacionExpedientes filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<NotificacionExpedientes> query = _NotificacionExpedientesRepository.GetByEntidadFiscalizadorEval(EstadoExpediente.Publicado);

                //var data = query.Where(x => x.Expediente.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Expediente.Codigo) ? x.Expediente.Codigo : filtro.Expediente.Codigo).ToUpper()))
                //                 .OrderByDescending(x => x.ExpedienteId);

                totalRows = query.Count();
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






        public void Save(NotificacionExpedientes obj)
        {
            try
            {
                _NotificacionExpedientesRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
