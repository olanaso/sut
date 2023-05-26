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
    public class ProcedimientoCargosOtrosService : IProcedimientoCargosOtrosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProcedimientoCargosOtrosRepository _ProcedimientoCargosOtrosRepository;

        public ProcedimientoCargosOtrosService(IUnitOfWork unitOfWork,
                            IProcedimientoCargosOtrosRepository ProcedimientoCargosOtrosRepository)
        {
            _unitOfWork = unitOfWork;
            _ProcedimientoCargosOtrosRepository = ProcedimientoCargosOtrosRepository;
        }

        public ProcedimientoCargosOtros GetOne (long ProcedimientoCargosOtrosid)
        {
            try
            {
                return _ProcedimientoCargosOtrosRepository.GetOne(ProcedimientoCargosOtrosid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long ProcedimientoCargosOtrosID)
        {
            try
            {
                _ProcedimientoCargosOtrosRepository.Eliminar(ProcedimientoCargosOtrosID);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ProcedimientoCargosOtros> GetOnelista(long ProcedimientoCargosApeid)
        {
            try
            {
                return _ProcedimientoCargosOtrosRepository.GetOnelista(ProcedimientoCargosApeid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProcedimientoCargosOtros LsitaGetOne( )
        {
            try
            {
                return _ProcedimientoCargosOtrosRepository.LsitaGetOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoCargosOtros> GetAllLikePagin(ProcedimientoCargosOtros filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<ProcedimientoCargosOtros> query = _ProcedimientoCargosOtrosRepository.GetAll();

                var data = query.Where(x => x.CargoOtros.ToUpper().Contains((string.IsNullOrEmpty(filtro.CargoOtros) ? x.CargoOtros : filtro.CargoOtros).ToUpper()))
                            .OrderByDescending(x => x.ProcedimientoCargosOtrosID);

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

        public void Save(ProcedimientoCargosOtros obj)
        {
            try
            {
                _ProcedimientoCargosOtrosRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
