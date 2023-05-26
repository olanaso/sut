using Sut.IApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Entities;
using Sut.IRepositories;
using Sut.Security;

namespace Sut.ApplicationServices
{
    public class ActividadService : IActividadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActividadRepository _actividadRepository;
        private readonly IActividadRecursoRepository _actividadRecursoRepository;

        public ActividadService(IUnitOfWork unitOfWork,
                                    IActividadRepository actividadRepository,
                                    IActividadRecursoRepository actividadRecursoRepository)
        {
            _unitOfWork = unitOfWork;
            _actividadRepository = actividadRepository;
            _actividadRecursoRepository = actividadRecursoRepository;
        }

        public List<Actividad> GetByTablaAsme(long TablaAsmeId)
        {
            try
            {
                return _actividadRepository.GetAllBy(x => x.TablaAsmeId == TablaAsmeId)
                                            .OrderBy(x => x.Orden)
                                            .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Actividad> GetByTablaAsmever(Filtros filtros, long TablaAsmeId, string des)
        {
            try
            {
                var liota =_actividadRepository.GetAllBy(x => x.TablaAsmeId == TablaAsmeId)
                                            .OrderBy(x => x.Orden)
                                            .ToList();
                 
                if (filtros.actividad!=0 ) {

                  liota= _actividadRepository.GetAllBy(x => x.TablaAsmeId == TablaAsmeId)
                                                .Where(x => x.TipoActividad == filtros.actividad)
                                            .OrderBy(x => x.Orden)
                                            .ToList();
                }
                if (filtros.valor != 0)
                {

                    liota = _actividadRepository.GetAllBy(x => x.TablaAsmeId == TablaAsmeId)
                                              .OrderBy(x => x.Orden)
                                                .Where(x => x.TipoValor == filtros.valor)
                                              .ToList();
                }
                if (filtros.actividad != 0 && filtros.valor != 0)
                {

                    liota = _actividadRepository.GetAllBy(x => x.TablaAsmeId == TablaAsmeId)
                                                .Where(x => x.TipoActividad == filtros.actividad && x.TipoValor == filtros.valor)
                                              .OrderBy(x => x.Orden)
                                              .ToList();
                }
                if (des != "" && des != null)
                {
                    liota = _actividadRepository.GetAllBy(x => x.TablaAsmeId == TablaAsmeId && x.Descripcion != null)
                        .Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(des) ? x.Descripcion : des).ToUpper())).OrderBy(x => x.Orden)
                                              .OrderBy(x => x.Orden) 
                                              .ToList();
                  
                }
                return liota;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void guardarnumeroprocedimiento(Actividad obj)
        {
            try
            {
                Actividad proc = _actividadRepository.GetOne(x => x.ActividadId == obj.ActividadId);
                proc.Orden = obj.Orden; 

                _actividadRepository.SaveOnlyActividad(proc);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Actividad GetOne(long ActividadId)
        {
            try
            {
                return _actividadRepository.GetOne(x => x.ActividadId == ActividadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Actividad GetOneasme(long TablaAsmeId)
        {
            try
            {
                return _actividadRepository.GetOne(x => x.TablaAsmeId == TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(long TablaAsmeId, long ActividadId)
        {
            try
            {
                _actividadRecursoRepository.Delete(new ActividadRecurso() { ActividadId = ActividadId });
                _actividadRepository.Delete(new Actividad() { TablaAsmeId = TablaAsmeId });
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Deleteactividad(long TablaAsmeId, long ActividadId)
        {
            try
            {
                _actividadRecursoRepository.Delete(new ActividadRecurso() { ActividadId = ActividadId });
                _actividadRepository.Delete(new Actividad() { TablaAsmeId = TablaAsmeId });
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<ActividadRecurso> GetByActividad(long ActividadId)
        {
            try
            {
                return _actividadRecursoRepository.GetByActividad(ActividadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarRecursos(Actividad obj)
        {
            try
            {
                _actividadRepository.Guardar(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarActividad(Actividad obj)
        {
            try
            {
                _actividadRepository.ActualizarActividad(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
        public void GuardarRecursosEliminar(Actividad obj)
        {
            try
            {
                _actividadRepository.GuardarRecursosEliminar(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public List<ActividadRecurso> GetDataByTablaAsme(long TablaAsmeId)
        {
            try
            {
                return _actividadRecursoRepository.GetByTablaAsme(TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadRecurso> GetDataByTablaAsmeTotal(long ExpedienteId)
        {
            try
            {
                return _actividadRecursoRepository.GetByTablaAsmeTotal(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadRecurso> GetActividadRecursoLista2(long ExpedienteId)
        {
            try
            {
                return _actividadRecursoRepository.GetActividadRecursoLista2(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Actividad> GetDataByTablaAsmeActividad(long TablaAsmeId)
        {
            try
            {
                return _actividadRecursoRepository.GetDataByTablaAsmeActividad(TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Recurso> GetDataByTablaAsmeRecursos(long IdEntidad)
        {
            try
            {
                return _actividadRecursoRepository.GetDataByTablaAsmeRecursos(IdEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
