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
    public class VCalidadRequisitos_CABECERAService : IVCalidadRequisitos_CABECERAService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVCalidadRequisitos_CABECERARepository _VCalidadRequisitos_CABECERARepository;

        public VCalidadRequisitos_CABECERAService(IUnitOfWork unitOfWork,
                            IVCalidadRequisitos_CABECERARepository VCalidadRequisitos_CABECERARepository)
        {
            _unitOfWork = unitOfWork;
            _VCalidadRequisitos_CABECERARepository = VCalidadRequisitos_CABECERARepository;
        }

        public VCalidadRequisitos_CABECERA GetOne (long VCalidadRequisitos_CABECERAid)
        {
            try
            {
                return _VCalidadRequisitos_CABECERARepository.GetOne(VCalidadRequisitos_CABECERAid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
        public List<VCalidadRequisitos_CABECERA> GetAllLikePagin(VCalidadRequisitos_CABECERA filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<VCalidadRequisitos_CABECERA> query = _VCalidadRequisitos_CABECERARepository.GetAll();
                 

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

        public List<VCalidadRequisitos_CABECERA> GetAll(Int32 CodEntidad)
        {
            try
            {
                return _VCalidadRequisitos_CABECERARepository.GetAllCOD(CodEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



    }
}
