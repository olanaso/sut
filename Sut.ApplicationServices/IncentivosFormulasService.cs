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
    public class IncentivosFormulasService : IIncentivosFormulasService
    {
        private readonly ILogService<IncentivosFormulasService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIncentivosFormulasRepository _IncentivosFormulasRepository;

        public IncentivosFormulasService(IUnitOfWork unitOfWork,
                            IIncentivosFormulasRepository IncentivosFormulasRepository)
        {
            _logger = new LogService<IncentivosFormulasService>();
            _unitOfWork = unitOfWork;
            _IncentivosFormulasRepository = IncentivosFormulasRepository;
        }



        public List<IncentivosFormulas> GetByIncentivosFormulas(IncentivosFormulas filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<IncentivosFormulas> query = _IncentivosFormulasRepository.GetAll();

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

        public IncentivosFormulas GetByone(long EntidadId)
        {
            try
            {
                return _IncentivosFormulasRepository.GetByone(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(IncentivosFormulas obj)
        {
            try
            {
                _IncentivosFormulasRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
