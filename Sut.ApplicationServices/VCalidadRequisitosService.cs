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
    public class VCalidadRequisitosService : IVCalidadRequisitosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVCalidadRequisitosRepository _VCalidadRequisitosRepository;

        public VCalidadRequisitosService(IUnitOfWork unitOfWork,
                            IVCalidadRequisitosRepository VCalidadRequisitosRepository)
        {
            _unitOfWork = unitOfWork;
            _VCalidadRequisitosRepository = VCalidadRequisitosRepository;
        }

        public VCalidadRequisitos GetOne (long VCalidadRequisitosid)
        {
            try
            {
                return _VCalidadRequisitosRepository.GetOne(VCalidadRequisitosid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
        public List<VCalidadRequisitos> GetAllLikePagin(VCalidadRequisitos filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<VCalidadRequisitos> query = _VCalidadRequisitosRepository.GetAll();
                 

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

   
 
    }
}
