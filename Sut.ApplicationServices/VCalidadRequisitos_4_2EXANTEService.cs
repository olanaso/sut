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
    public class VCalidadRequisitos_4_2EXANTEService : IVCalidadRequisitos_4_2EXANTEService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVCalidadRequisitos_4_2EXANTERepository _VCalidadRequisitos_4_2EXANTERepository;

        public VCalidadRequisitos_4_2EXANTEService(IUnitOfWork unitOfWork,
                            IVCalidadRequisitos_4_2EXANTERepository VCalidadRequisitos_4_2EXANTERepository)
        {
            _unitOfWork = unitOfWork;
            _VCalidadRequisitos_4_2EXANTERepository = VCalidadRequisitos_4_2EXANTERepository;
        }

        public VCalidadRequisitos_4_2EXANTE GetOne (long VCalidadRequisitos_4_2EXANTEid)
        {
            try
            {
                return _VCalidadRequisitos_4_2EXANTERepository.GetOne(VCalidadRequisitos_4_2EXANTEid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
        public List<VCalidadRequisitos_4_2EXANTE> GetAllLikePagin(VCalidadRequisitos_4_2EXANTE filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<VCalidadRequisitos_4_2EXANTE> query = _VCalidadRequisitos_4_2EXANTERepository.GetAll();
                 

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

        public List<VCalidadRequisitos_4_2EXANTE> GetAll(Int32 CodEntidad)
        {
            try
            {
                return _VCalidadRequisitos_4_2EXANTERepository.GetAllCOD(CodEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



    }
}
