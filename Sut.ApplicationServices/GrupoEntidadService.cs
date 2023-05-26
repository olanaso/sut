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
    public class GrupoEntidadService : IGrupoEntidadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGrupoEntidadRepository _GrupoEntidadRepository; 
        private readonly IUnidadOrganicaRepository _unidadOrganicaRepository;
        private readonly IActividadRecursoRepository _actividadRecursoRepository;
        private readonly IAuditoriaRepository _auditoriaRepository;

        public GrupoEntidadService(IUnitOfWork unitOfWork,
                                    IGrupoEntidadRepository GrupoEntidadRepository, 
                                    IUnidadOrganicaRepository unidadOrganicaRepository,
                                    IActividadRecursoRepository actividadRecursoRepository,
                                     IAuditoriaRepository auditoriaRepository)
        {
            _unitOfWork = unitOfWork;
            _GrupoEntidadRepository = GrupoEntidadRepository; 
            _unidadOrganicaRepository = unidadOrganicaRepository;
            _actividadRecursoRepository = actividadRecursoRepository;
            _auditoriaRepository = auditoriaRepository;
        }

        public List<GrupoEntidad> GetAll(long EntidadId)
        {
            try
            {
                return _GrupoEntidadRepository.GetAll(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GrupoEntidad GetOne(long GrupoEntidadId)
        {
            try
            {
                return _GrupoEntidadRepository.GetOne(GrupoEntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoEntidad> GetAllLikePagin(GrupoEntidad filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<GrupoEntidad> query = _GrupoEntidadRepository.GetAll(filtro.EntidadId);

                var data = query.Where(x => x.Entidad.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Entidad.Nombre) ? x.Entidad.Nombre : filtro.Entidad.Nombre).ToUpper()))
                            .OrderBy(x => x.GrupoEntidadId);

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
        public void Eliminar(long GrupoEntidadId)
        {
            try
            {
                _GrupoEntidadRepository.Eliminar(GrupoEntidadId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save(GrupoEntidad obj)
        {
            try
            {
                _GrupoEntidadRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
    }
}
