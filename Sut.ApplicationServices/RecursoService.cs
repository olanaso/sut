using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sut.ApplicationServices
{
    public class RecursoService : IRecursoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecursoRepository _recursoRepository;

        public RecursoService(IUnitOfWork unitOfWork,
                                    IRecursoRepository recursoService)
        {
            _unitOfWork = unitOfWork;
            _recursoRepository = recursoService;
        }

        public List<Recurso> GetAll(long EntidadId, TipoRecurso tipo)
        {
            try
            {
                return _recursoRepository.GetAll(EntidadId, tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Recurso GetOne(long RecursoId)
        {
            try
            {
                return _recursoRepository.GetOne(RecursoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Recurso> GetAllLikePagin(Recurso filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Recurso> query = _recursoRepository.GetAll(filtro.EntidadId, filtro.TipoRecurso);

                if (query == null)
                    return null;

                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper()))
                            .OrderBy(x => x.Nombre);

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

        public List<Recurso> GetAllLikePagin(Recurso filtro, int tipo, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Recurso> query;

                if (tipo > 0)
                {
                    filtro.TipoRecurso = (TipoRecurso)Enum.Parse(typeof(TipoRecurso), tipo.ToString());
                    query = _recursoRepository.GetAll(filtro.EntidadId, filtro.TipoRecurso);
                }
                else
                {
                    query = _recursoRepository.GetAll(filtro.EntidadId);
                }

                var data = query.Where(x => x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper()))
                            .OrderBy(x => x.TipoRecurso)
                            .ThenBy(x => x.Nombre);

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

        public void Save(Recurso obj)
        {
            try
            {
                _recursoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long idRecurso)
        {
            try
            {
                _recursoRepository.Eliminar(idRecurso);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
