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
    public class IngresoSUTService : IIngresoSUTService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIngresoSUTRepository _IngresoSUTRepository;

        public IngresoSUTService(IUnitOfWork unitOfWork,
                            IIngresoSUTRepository IngresoSUTRepository)
        {
            _unitOfWork = unitOfWork;
            _IngresoSUTRepository = IngresoSUTRepository;
        } 
         
        public void Save(IngresoSUT obj)
        {
            try
            {
                _IngresoSUTRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
