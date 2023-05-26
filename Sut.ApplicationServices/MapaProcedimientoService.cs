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
    public class MapaProcedimientoService : IMapaProcedimientoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapaProcedimientoRepository _mapaProcedimientoRepository;

        public MapaProcedimientoService(IUnitOfWork unitOfWork,
                                    IMapaProcedimientoRepository mapaProcedimientoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapaProcedimientoRepository = mapaProcedimientoRepository;
        }

        public List<MapaProcedimiento> GetAllLikePagin(MapaProcedimiento filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                IQueryable<MapaProcedimiento> query = _mapaProcedimientoRepository.GetAllBy(x => 
                                                        x.Codigo.Contains(filtro.Codigo) 
                                                    && x.Nombre.ToUpper().Contains((filtro.Nombre).ToUpper())
                                                );

                var data = query.OrderBy(x => x.Codigo);

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

        public MapaProcedimiento GetOne(long MapaProcedimeintoId)
        {
            try
            {
                return _mapaProcedimientoRepository.GetOne(MapaProcedimeintoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MapaProcedimiento> GetByParent(long PadreId)
        {
            try
            {
                return _mapaProcedimientoRepository.GetAllBy(x => x.PadreId == PadreId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(MapaProcedimiento obj)
        {
            try
            {
                _mapaProcedimientoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
