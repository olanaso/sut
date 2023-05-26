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
    public class VCalidadRequisitos_4_2Service : IVCalidadRequisitos_4_2Service
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVCalidadRequisitos_4_2Repository _VCalidadRequisitos_4_2Repository;

        public VCalidadRequisitos_4_2Service(IUnitOfWork unitOfWork,
                            IVCalidadRequisitos_4_2Repository VCalidadRequisitos_4_2Repository)
        {
            _unitOfWork = unitOfWork;
            _VCalidadRequisitos_4_2Repository = VCalidadRequisitos_4_2Repository;
        }

        public VCalidadRequisitos_4_2 GetOne (long VCalidadRequisitos_4_2id)
        {
            try
            {
                return _VCalidadRequisitos_4_2Repository.GetOne(VCalidadRequisitos_4_2id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
        public List<VCalidadRequisitos_4_2> GetAllLikePagin(VCalidadRequisitos_4_2 filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<VCalidadRequisitos_4_2> query = _VCalidadRequisitos_4_2Repository.GetAll();
                 

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

        public List<VCalidadRequisitos_4_2> GetAll(Int32 CodEntidad)
        {
            try
            {
                return _VCalidadRequisitos_4_2Repository.GetAllCOD(CodEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



    }
}
