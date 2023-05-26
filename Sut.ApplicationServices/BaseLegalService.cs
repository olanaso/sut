using Sut.IApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Entities;
using Sut.IRepositories;

namespace Sut.ApplicationServices
{
    public class BaseLegalService : IBaseLegalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseLegalRepository _baseLegalRepository;
        private readonly IBaseLegalNormaRepository _baseLegalNormaRepository;

        public BaseLegalService (IUnitOfWork unitofWork,
                                IBaseLegalRepository baseLegalRepository,
                                IBaseLegalNormaRepository baseLegalNormaRepository)
        {
            _unitOfWork = unitofWork;
            _baseLegalRepository = baseLegalRepository;
            _baseLegalNormaRepository = baseLegalNormaRepository;
        }

        public List<BaseLegalNorma> GetDetails(long BaseLegalId)
        {
            try
            {
                return _baseLegalNormaRepository.GetDetails(BaseLegalId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseLegalNorma GetOne(long BaseLegalId, long BaseLegalNormaId)
        {
            try
            {
                return _baseLegalNormaRepository.GetOne(BaseLegalId, BaseLegalNormaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BaseLegalNorma> GetByExpediente(List<long> ids)
        {
            try
            {
                return _baseLegalNormaRepository.GetByExpediente(ids);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
