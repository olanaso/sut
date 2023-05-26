using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using Sut.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class CalendarioActividadesService : ICalendarioActividadesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICalendarioActividadesRepository _CalendarioActividadesRepository; 
        private readonly IUnidadOrganicaRepository _unidadOrganicaRepository;
        private readonly IActividadRecursoRepository _actividadRecursoRepository;
        private readonly IAuditoriaRepository _auditoriaRepository;

        public CalendarioActividadesService(IUnitOfWork unitOfWork,
                                    ICalendarioActividadesRepository CalendarioActividadesRepository, 
                                    IUnidadOrganicaRepository unidadOrganicaRepository,
                                    IActividadRecursoRepository actividadRecursoRepository,
                                     IAuditoriaRepository auditoriaRepository)
        {
            _unitOfWork = unitOfWork;
            _CalendarioActividadesRepository = CalendarioActividadesRepository; 
            _unidadOrganicaRepository = unidadOrganicaRepository;
            _actividadRecursoRepository = actividadRecursoRepository;
            _auditoriaRepository = auditoriaRepository;
        }

        public List<CalendarioActividades> GetAll(long EntidadId)
        {
            try
            {
                return _CalendarioActividadesRepository.GetAll(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CalendarioActividades GetOne(long CalendarioActividadesId)
        {
            try
            {
                return _CalendarioActividadesRepository.GetOne(CalendarioActividadesId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CalendarioActividades> GetAllLikePagin(CalendarioActividades filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<CalendarioActividades> query = _CalendarioActividadesRepository.GetAll(filtro.CalendarioActividadesId);

                var data = query.OrderBy(x => x.CalendarioActividadesId);
                //Where(x => x.LugarEjecucion.ToUpper().Contains((string.IsNullOrEmpty(filtro.LugarEjecucion) ? x.LugarEjecucion : filtro.LugarEjecucion).ToUpper()))
                          

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
        public void Eliminar(long CalendarioActividadesId)
        {
            try
            {
                _CalendarioActividadesRepository.Eliminar(CalendarioActividadesId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save(CalendarioActividades obj)
        {
            try
            {
                _CalendarioActividadesRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
    }
}
