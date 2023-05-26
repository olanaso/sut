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
    public class IncentivosFormulasCorteService : IIncentivosFormulasCorteService
    {
        private readonly ILogService<IncentivosFormulasCorteService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIncentivosFormulasCorteRepository _IncentivosFormulasCorteRepository;

        public IncentivosFormulasCorteService(IUnitOfWork unitOfWork,
                            IIncentivosFormulasCorteRepository IncentivosFormulasCorteRepository)
        {
            _logger = new LogService<IncentivosFormulasCorteService>();
            _unitOfWork = unitOfWork;
            _IncentivosFormulasCorteRepository = IncentivosFormulasCorteRepository;
        }



        public List<IncentivosFormulasCorte> GetByIncentivosFormulasCorte(IncentivosFormulasCorte filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<IncentivosFormulasCorte> query = _IncentivosFormulasCorteRepository.GetAll();

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

        public IncentivosFormulasCorte GetByone(long EntidadId)
        {
            try
            {
                return _IncentivosFormulasCorteRepository.GetByone(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(IncentivosFormulasCorte obj)
        {
            try
            {
                _IncentivosFormulasCorteRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
