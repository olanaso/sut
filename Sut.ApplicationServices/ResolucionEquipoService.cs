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
    public class ResolucionEquipoService : IResolucionEquipoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResolucionEquipoRepository _resolucionEquipoRepository;

        public ResolucionEquipoService(IUnitOfWork unitOfWork,
                                    IResolucionEquipoRepository resolucionEquipoRepository)
        {
            _unitOfWork = unitOfWork;
            _resolucionEquipoRepository = resolucionEquipoRepository;
        }

        public List<ResolucionEquipo> GetAllLikePagin(ResolucionEquipo filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                IEnumerable<ResolucionEquipo> query = _resolucionEquipoRepository.GetByEntidad(filtro.EntidadId);

                var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                            .OrderByDescending(x => x.ResolucionEquipoId);

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

        public ResolucionEquipo GetOne(long ResolucionEquipoId)
        {
            try
            {
                return _resolucionEquipoRepository.GetOne(ResolucionEquipoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResolucionEquipo GetOneEntidad(long EntidadId)
        {
            try
            {
                return _resolucionEquipoRepository.GetOneEntidad(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(ResolucionEquipo obj)
        {
            try
            {
                _resolucionEquipoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
