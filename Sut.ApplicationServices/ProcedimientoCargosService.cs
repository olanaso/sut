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
    public class ProcedimientoCargosService : IProcedimientoCargosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProcedimientoCargosRepository _ProcedimientoCargosRepository;

        public ProcedimientoCargosService(IUnitOfWork unitOfWork,
                            IProcedimientoCargosRepository ProcedimientoCargosRepository)
        {
            _unitOfWork = unitOfWork;
            _ProcedimientoCargosRepository = ProcedimientoCargosRepository;
        }

        public ProcedimientoCargos GetOne (long ProcedimientoCargosid)
        {
            try
            {
                return _ProcedimientoCargosRepository.GetOne(ProcedimientoCargosid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProcedimientoCargos> GetOnelista(long ProcedimientoCargosid)
        {
            try
            {
                return _ProcedimientoCargosRepository.GetOnelista(ProcedimientoCargosid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long ProcedimientoCargosID)
        {
            try
            {
                _ProcedimientoCargosRepository.Eliminar(ProcedimientoCargosID);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProcedimientoCargos LsitaGetOne( )
        {
            try
            {
                return _ProcedimientoCargosRepository.LsitaGetOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoCargos> GetAllLikePagin(ProcedimientoCargos filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<ProcedimientoCargos> query = _ProcedimientoCargosRepository.GetAll();

                var data = query.Where(x => x.Cargo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Cargo) ? x.Cargo : filtro.Cargo).ToUpper()))
                            .OrderByDescending(x => x.ProcedimientoCargosID);

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

        public void Save(ProcedimientoCargos obj)
        {
            try
            {
                _ProcedimientoCargosRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
