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
    public class NotificacionService : INotificacionService
    {
        private readonly ILogService<NotificacionService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificacionRepository _NotificacionRepository;

        public NotificacionService(IUnitOfWork unitOfWork,
                            INotificacionRepository NotificacionRepository)
        {
            _logger = new LogService<NotificacionService>();
            _unitOfWork = unitOfWork;
            _NotificacionRepository = NotificacionRepository;
        }



        public List<Notificacion> GetByNotificacion(Notificacion filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Notificacion> query = _NotificacionRepository.GetAll();

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

        public Notificacion GetByone(long notificacionId)
        {
            try
            {
                return _NotificacionRepository.GetByone(notificacionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Notificacion obj)
        {
            try
            {
                _NotificacionRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
