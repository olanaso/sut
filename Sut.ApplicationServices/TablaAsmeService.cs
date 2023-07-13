using Sut.IApplicationServices;
using System;
using System.Collections.Generic;
using Sut.Entities;
using Sut.IRepositories;
using System.Linq;

namespace Sut.ApplicationServices
{
    public class TablaAsmeService : ITablaAsmeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITablaAsmeRepository _tablaAsmeRepository;

        public TablaAsmeService(IUnitOfWork unitOfWork,
                                    ITablaAsmeRepository tablaAsmeRepository)
        {
            _unitOfWork = unitOfWork;
            _tablaAsmeRepository = tablaAsmeRepository;
        }

        public List<TablaAsme> GetByExpediente(long ExpedienteId)
        {
            try
            {
                return _tablaAsmeRepository.GetByExpediente(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TablaAsme> GetByExpedienteSinEliminados(long ExpedienteId)
        {
            try
            {
                return _tablaAsmeRepository.GetByExpedienteSinEliminados(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TablaAsme> GetByMef(TablaAsme filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            { 
                List<TablaAsme> query = _tablaAsmeRepository.GetByMef();

                //var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                //            .OrderBy(x => x.TablaAsmeId);

                totalRows = query.Count();
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

        public List<TablaAsme> GetByProcedimiento(long ProcedimientoId)
        {
            try
            {
                return _tablaAsmeRepository.GetAllBy(x => x.ProcedimientoId == ProcedimientoId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TablaAsme GetOne(long TablaAsmeId)
        {
            try
            {
                return _tablaAsmeRepository.GetOne(TablaAsmeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public void Guardar(TablaAsme obj)
        {
            try
            {
                _tablaAsmeRepository.Guardar(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Guardarexcel(TablaAsme obj)
        {
            try
            {
                _tablaAsmeRepository.Guardarexcel(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public void Guardardelete(TablaAsme obj)
        {
            try
            {
                _tablaAsmeRepository.Guardardelete(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void  delete(TablaAsme obj)
        {
            try
            {
                _tablaAsmeRepository.delete(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TablaAsme> ResumenGetAllLikePagin(TablaAsme filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<TablaAsme> query = _tablaAsmeRepository.GetResumenCostos(filtro.Procedimiento.ExpedienteId);

                var data = query.Where(x => x.Procedimiento.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Procedimiento.Codigo) ? x.Procedimiento.Codigo : filtro.Procedimiento.Codigo).ToUpper())
                                            && x.Procedimiento.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Procedimiento.Denominacion) ? x.Procedimiento.Denominacion : filtro.Procedimiento.Denominacion).ToUpper()))
                            .OrderBy(x => x.Procedimiento.UndOrgResponsable.Nombre);

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

        public void GuardarSubvencion(List<TablaAsme> lstSubvencion)
        {
            try
            {
                _tablaAsmeRepository.GuardarSubvencion(lstSubvencion);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(TablaAsme obj)
        {
            try
            {
                _tablaAsmeRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
