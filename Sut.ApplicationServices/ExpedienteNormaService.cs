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
    public class ExpedienteNormaService : IExpedienteNormaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpedienteNormaRepository _expedienteNormaRepository;

        public ExpedienteNormaService(IUnitOfWork unitOfWork,
                                    IExpedienteNormaRepository expedienteNormaRepository)
        {
            _unitOfWork = unitOfWork;
            _expedienteNormaRepository = expedienteNormaRepository;
        }

        public List<ExpedienteNorma> GetAllLikePagin(ExpedienteNorma filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                IEnumerable<ExpedienteNorma> query = _expedienteNormaRepository.GetByExpediente(filtro.ExpedienteId);

                var data = query
                            .OrderByDescending(x => x.ExpedienteId);

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

        public ExpedienteNorma GetOne(long NormaId)
        {
            try
            {
                return _expedienteNormaRepository.GetOne(NormaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ExpedienteNorma GetOneexpediente(long ExpedienteId)
        {
            try
            {
                return _expedienteNormaRepository.GetOneexpediente(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ExpedienteNorma> GetByExpedientenorma(long ExpedienteId)
        {
            try
            {
                return _expedienteNormaRepository.GetByExpedientenorma(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(ExpedienteNorma obj)
        {
            try
            {
                _expedienteNormaRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminar(long id)
        {
            try
            {
                _expedienteNormaRepository.eliminar(id);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
