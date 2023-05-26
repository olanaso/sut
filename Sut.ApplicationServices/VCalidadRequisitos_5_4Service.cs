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
    public class VCalidadRequisitos_5_4Service : IVCalidadRequisitos_5_4Service
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVCalidadRequisitos_5_4Repository _VCalidadRequisitos_5_4Repository;

        public VCalidadRequisitos_5_4Service(IUnitOfWork unitOfWork,
                            IVCalidadRequisitos_5_4Repository VCalidadRequisitos_5_4Repository)
        {
            _unitOfWork = unitOfWork;
            _VCalidadRequisitos_5_4Repository = VCalidadRequisitos_5_4Repository;
        }

        public VCalidadRequisitos_5_4 GetOne (long VCalidadRequisitos_5_4id)
        {
            try
            {
                return _VCalidadRequisitos_5_4Repository.GetOne(VCalidadRequisitos_5_4id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
        public List<VCalidadRequisitos_5_4> GetAllLikePagin(VCalidadRequisitos_5_4 filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<VCalidadRequisitos_5_4> query = _VCalidadRequisitos_5_4Repository.GetAll();
                 

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

        public List<VCalidadRequisitos_5_4> GetAll(Int32 CodEntidad)
        {
            try
            {
                return _VCalidadRequisitos_5_4Repository.GetAllCOD(CodEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



    }
}
