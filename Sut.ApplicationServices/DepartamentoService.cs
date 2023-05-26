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
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoService(IUnitOfWork unitOfWork,
                                    IDepartamentoRepository departamentoRepository)
        {
            _unitOfWork = unitOfWork;
            _departamentoRepository = departamentoRepository;
        }

        public List<Departamento> GetAll()
        {
            try
            {
                return _departamentoRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
