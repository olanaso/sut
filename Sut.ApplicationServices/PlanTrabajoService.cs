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
    public class PlanTrabajoService : IPlanTrabajoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlanTrabajoRepository _planTrabajoRepository;

        public PlanTrabajoService(IUnitOfWork unitOfWork,
                                    IPlanTrabajoRepository planTrabajoRepository)
        {
            _unitOfWork = unitOfWork;
            _planTrabajoRepository = planTrabajoRepository;
        }

        public List<PlanTrabajo> GetAllLikePagin(PlanTrabajo filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                IEnumerable<PlanTrabajo> query = _planTrabajoRepository.GetByEntidad(filtro.EntidadId);

                var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                            .OrderByDescending(x => x.PlanTrabajoId);

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

        public PlanTrabajo GetOne(long PlanTrabajoId)
        {
            try
            {
                return _planTrabajoRepository.GetOne(PlanTrabajoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PlanTrabajo GetOneEntidad(long EntidadId)
        {
            try
            {
                return _planTrabajoRepository.GetOneEntidad(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(PlanTrabajo obj)
        {
            try
            {
                _planTrabajoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
