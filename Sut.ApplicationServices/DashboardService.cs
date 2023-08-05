using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using Sut.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDashboardRepository _DashboardRepository;

        public DashboardService(IUnitOfWork unitOfWork,
                            IDashboardRepository DashboardRepository)
        {
            _unitOfWork = unitOfWork;
            _DashboardRepository = DashboardRepository;
        }

        public Dashboard getDashboard(Dashboard dashboard)
        {
            try
            {
                return _DashboardRepository.getDashboard(dashboard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Dashboard getDashboardCalendario(Dashboard dashboard)
        {
            try
            {
                return _DashboardRepository.getDashboardCalendario(dashboard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dashboard RegistrarVideo(Dashboard dashboard)
        {
            try
            {
                return _DashboardRepository.RegistrarVideo(dashboard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dashboard ListarVideo(Dashboard dashboard)
        {
            try
            {
                return _DashboardRepository.ListarVideo(dashboard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dashboard EditarVideo(Dashboard dashboard)
        {
            try
            {
                return _DashboardRepository.EditarVideo(dashboard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dashboard EliminarVideo(Dashboard dashboard)
        {
            try
            {
                return _DashboardRepository.EliminarVideo(dashboard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
