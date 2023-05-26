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
    public class VCalidadRequisitos_5_4EXANTEService : IVCalidadRequisitos_5_4EXANTEService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVCalidadRequisitos_5_4EXANTERepository _VCalidadRequisitos_5_4EXANTERepository;

        public VCalidadRequisitos_5_4EXANTEService(IUnitOfWork unitOfWork,
                            IVCalidadRequisitos_5_4EXANTERepository VCalidadRequisitos_5_4EXANTERepository)
        {
            _unitOfWork = unitOfWork;
            _VCalidadRequisitos_5_4EXANTERepository = VCalidadRequisitos_5_4EXANTERepository;
        }

        public VCalidadRequisitos_5_4EXANTE GetOne (long VCalidadRequisitos_5_4EXANTEid)
        {
            try
            {
                return _VCalidadRequisitos_5_4EXANTERepository.GetOne(VCalidadRequisitos_5_4EXANTEid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
        public List<VCalidadRequisitos_5_4EXANTE> GetAllLikePagin(VCalidadRequisitos_5_4EXANTE filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<VCalidadRequisitos_5_4EXANTE> query = _VCalidadRequisitos_5_4EXANTERepository.GetAll();
                 

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

        public List<VCalidadRequisitos_5_4EXANTE> GetAll(Int32 CodEntidad)
        {
            try
            {
                return _VCalidadRequisitos_5_4EXANTERepository.GetAllCOD(CodEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



    }
}
