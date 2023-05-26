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
    public class ProcedimientoUndOrgResponsableService : IProcedimientoUndOrgResponsableService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProcedimientoUndOrgResponsableRepository _ProcedimientoUndOrgResponsableRepository;

        public ProcedimientoUndOrgResponsableService(IUnitOfWork unitOfWork,
                            IProcedimientoUndOrgResponsableRepository ProcedimientoUndOrgResponsableRepository)
        {
            _unitOfWork = unitOfWork;
            _ProcedimientoUndOrgResponsableRepository = ProcedimientoUndOrgResponsableRepository;
        }

        public ProcedimientoUndOrgResponsable GetOne (long ProcedimientoUndOrgResponsableid)
        {
            try
            {
                return _ProcedimientoUndOrgResponsableRepository.GetOne(ProcedimientoUndOrgResponsableid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long ProcedimientoUndOrgResponsableID)
        {
            try
            {
                _ProcedimientoUndOrgResponsableRepository.Eliminar(ProcedimientoUndOrgResponsableID);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProcedimientoUndOrgResponsable LsitaGetOne(long procedimientoId)
        {
            try
            {
                return _ProcedimientoUndOrgResponsableRepository.LsitaGetOne(procedimientoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProcedimientoUndOrgResponsable> GetAll(long procedimientoId)
        {
            try
            {
                return _ProcedimientoUndOrgResponsableRepository.GetAll(procedimientoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public List<ProcedimientoUndOrgResponsable> GetAllLikePagin(ProcedimientoUndOrgResponsable filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<ProcedimientoUndOrgResponsable> query = _ProcedimientoUndOrgResponsableRepository.GetAll();

                //var data = query.Where(x => x.CargoOtros.ToUpper().Contains((string.IsNullOrEmpty(filtro.CargoOtros) ? x.CargoOtros : filtro.CargoOtros).ToUpper()))
                //            .OrderByDescending(x => x.ProcedimientoUndOrgResponsableID);

                //totalRows = data.Count();
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

        public void Save(ProcedimientoUndOrgResponsable obj)
        {
            try
            {
                _ProcedimientoUndOrgResponsableRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
