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
    public class VCalidadRequisitos_CABECERAEXANTEService : IVCalidadRequisitos_CABECERAEXANTEService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVCalidadRequisitos_CABECERAEXANTERepository _VCalidadRequisitos_CABECERAEXANTERepository;

        public VCalidadRequisitos_CABECERAEXANTEService(IUnitOfWork unitOfWork,
                            IVCalidadRequisitos_CABECERAEXANTERepository VCalidadRequisitos_CABECERAEXANTERepository)
        {
            _unitOfWork = unitOfWork;
            _VCalidadRequisitos_CABECERAEXANTERepository = VCalidadRequisitos_CABECERAEXANTERepository;
        }

        public VCalidadRequisitos_CABECERAEXANTE GetOne (long VCalidadRequisitos_CABECERAEXANTEid)
        {
            try
            {
                return _VCalidadRequisitos_CABECERAEXANTERepository.GetOne(VCalidadRequisitos_CABECERAEXANTEid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
        public List<VCalidadRequisitos_CABECERAEXANTE> GetAllLikePagin(VCalidadRequisitos_CABECERAEXANTE filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<VCalidadRequisitos_CABECERAEXANTE> query = _VCalidadRequisitos_CABECERAEXANTERepository.GetAll();
                 

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

        public List<VCalidadRequisitos_CABECERAEXANTE> GetAll(Int32 CodEntidad)
        {
            try
            {
                return _VCalidadRequisitos_CABECERAEXANTERepository.GetAllCOD(CodEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



    }
}
