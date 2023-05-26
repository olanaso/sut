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
    public class ProcedimientoCargosApeService : IProcedimientoCargosApeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProcedimientoCargosApeRepository _ProcedimientoCargosApeRepository;

        public ProcedimientoCargosApeService(IUnitOfWork unitOfWork,
                            IProcedimientoCargosApeRepository ProcedimientoCargosApeRepository)
        {
            _unitOfWork = unitOfWork;
            _ProcedimientoCargosApeRepository = ProcedimientoCargosApeRepository;
        }

        public ProcedimientoCargosApe GetOne (long ProcedimientoCargosApeid)
        {
            try
            {
                return _ProcedimientoCargosApeRepository.GetOne(ProcedimientoCargosApeid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoCargosApe> GetOnelista(long ProcedimientoCargosApeid)
        {
            try
            {
                return _ProcedimientoCargosApeRepository.GetOnelista(ProcedimientoCargosApeid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(int ProcedimientoID, int orden)
        {
            try
            {
                _ProcedimientoCargosApeRepository.Eliminar(ProcedimientoID, orden);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public ProcedimientoCargosApe LsitaGetOne( )
        {
            try
            {
                return _ProcedimientoCargosApeRepository.LsitaGetOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcedimientoCargosApe LsitaGetOneorden(long ProcedimientoCargosApeid, int orden)
        {
            try
            {
                return _ProcedimientoCargosApeRepository.LsitaGetOneorden(ProcedimientoCargosApeid, orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoCargosApe> GetAllLikePagin(ProcedimientoCargosApe filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<ProcedimientoCargosApe> query = _ProcedimientoCargosApeRepository.GetAll();

                var data = query.Where(x => x.CargoApe.ToUpper().Contains((string.IsNullOrEmpty(filtro.CargoApe) ? x.CargoApe : filtro.CargoApe).ToUpper()))
                            .OrderByDescending(x => x.ProcedimientoCargosApeID);

                totalRows = data.Count();
                var result = data.Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(ProcedimientoCargosApe obj)
        {
            try
            {
                _ProcedimientoCargosApeRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
