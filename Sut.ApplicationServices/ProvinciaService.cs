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
    public class ProvinciaService : IProvinciaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProvinciaRepository _provinciaRepository;

        public ProvinciaService(IUnitOfWork unitOfWork,
                                IProvinciaRepository provinciaRepository)
        {
            _unitOfWork = unitOfWork;
            _provinciaRepository = provinciaRepository;
        }

        public List<Provincia> GetByDepartamento(long DepartamentoId)
        {
            try
            {
                return _provinciaRepository.GetByDepartamento(DepartamentoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
