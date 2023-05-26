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
    public class InformeService : IInformeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInformeRepository _InformeRepository;

        public InformeService(IUnitOfWork unitOfWork,
                            IInformeRepository InformeRepository)
        {
            _unitOfWork = unitOfWork;
            _InformeRepository = InformeRepository;
        }

        public Informe GetOne (long Informeid)
        {
            try
            {
                return _InformeRepository.GetOne(Informeid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public List<Informe> listaGetOne(long Informeid)
        {
            try
            {
                return _InformeRepository.listaGetOne(Informeid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Informe LsitaGetOne( )
        {
            try
            {
                return _InformeRepository.LsitaGetOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Informe> GetAllLikePagin(Informe filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Informe> query = _InformeRepository.GetAll();

                var data = query.OrderByDescending(x => x.InformeID);

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

        public void Save(Informe obj)
        {
            try
            {
                _InformeRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
