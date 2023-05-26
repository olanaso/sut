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
    public class ComunicadoService : IComunicadoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IComunicadoRepository _ComunicadoRepository;

        public ComunicadoService(IUnitOfWork unitOfWork,
                            IComunicadoRepository ComunicadoRepository)
        {
            _unitOfWork = unitOfWork;
            _ComunicadoRepository = ComunicadoRepository;
        }

        public Comunicado GetOne (long Comunicadoid)
        {
            try
            {
                return _ComunicadoRepository.GetOne(Comunicadoid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Comunicado LsitaGetOne( )
        {
            try
            {
                return _ComunicadoRepository.LsitaGetOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Comunicado> GetAllLikePagin(Comunicado filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Comunicado> query = _ComunicadoRepository.GetAll();

                var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                            .OrderByDescending(x => x.ComunicadoID);

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

        public List<Comunicado> GetAllLikePaginBaner(Comunicado filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Comunicado> query = _ComunicadoRepository.GetAllBaner();

                var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                            .OrderByDescending(x => x.ComunicadoID);

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

        public void Save(Comunicado obj)
        {
            try
            {
                _ComunicadoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
