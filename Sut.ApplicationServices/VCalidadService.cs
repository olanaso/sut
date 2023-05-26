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
    public class VCalidadService : IVCalidadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVCalidadRepository _VCalidadRepository;

        public VCalidadService(IUnitOfWork unitOfWork,
                            IVCalidadRepository VCalidadRepository)
        {
            _unitOfWork = unitOfWork;
            _VCalidadRepository = VCalidadRepository;
        }

        public VCalidad GetOne (long VCalidadid)
        {
            try
            {
                return _VCalidadRepository.GetOne(VCalidadid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

   

        public List<VCalidad> GetAll(string CodEntidad)
        {
            try
            {
                return _VCalidadRepository.GetAll(CodEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }
 
    }
}
