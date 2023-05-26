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
    public class EnumeradoService : IEnumeradoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnumeradoRepository _enumeradoRepository;

        public EnumeradoService(IUnitOfWork unitOfWork,
                          IEnumeradoRepository enumeradoRepository)
        {
            _unitOfWork = unitOfWork;
            _enumeradoRepository = enumeradoRepository;
        }

        public List<Enumerado> GetByTipo(TipoEnumerado tipo)
        {
            try
            {
                return _enumeradoRepository.GetByTipo(tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
