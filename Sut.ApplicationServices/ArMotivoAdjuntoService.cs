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
    public class ArMotivoAdjuntoService : IArMotivoAdjuntoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArMotivoAdjuntoRepository _ArMotivoAdjuntoService;

        public ArMotivoAdjuntoService (IUnitOfWork unitOfWork,
                                        IArMotivoAdjuntoRepository ArMotivoAdjuntoRepository)
        {
            _unitOfWork = unitOfWork;
            _ArMotivoAdjuntoService = ArMotivoAdjuntoRepository;
        }

        public ArMotivoAdjunto GetOne (long ArMotivoAdjuntoId)
        {
            try
            {
                return _ArMotivoAdjuntoService.GetOne(ArMotivoAdjuntoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ArMotivoAdjunto> GetAllLikePagin(ArMotivoAdjunto filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<ArMotivoAdjunto> query = new List<ArMotivoAdjunto>();

                if (filtro.NormaId == 0)
                {
                    query = _ArMotivoAdjuntoService.GetAll(filtro.ExpedienteId);
                }
                else
                {
                  query = _ArMotivoAdjuntoService.GetAllxid(filtro.ExpedienteId, filtro.NormaId);
                }
                

                var data = query.Where(x => x.NombreArchivo.ToUpper().Contains((string.IsNullOrEmpty(filtro.NombreArchivo) ? x.NombreArchivo : filtro.NombreArchivo).ToUpper()))
                            .OrderBy(x => x.ArMotivoAdjuntoId);

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

        public List<ArMotivoAdjunto> GetAllLikePaginEliminar(long ExpedienteId)
        {
            try
            { 
                return _ArMotivoAdjuntoService.GetAllLikePaginEliminar(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminar(long ArMotivoAdjuntoId)
        {
            try
            {
                _ArMotivoAdjuntoService.Eliminar(ArMotivoAdjuntoId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
        public void modificar(ArMotivoAdjunto obj)
        {
            try
            {
                _ArMotivoAdjuntoService.modificar(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save(ArMotivoAdjunto obj)
        {
            try
            {
                _ArMotivoAdjuntoService.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
