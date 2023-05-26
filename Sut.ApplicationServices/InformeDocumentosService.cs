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
    public class InformeDocumentosService : IInformeDocumentosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInformeDocumentosRepository _InformeDocumentosRepository;

        public InformeDocumentosService(IUnitOfWork unitOfWork,
                            IInformeDocumentosRepository InformeDocumentosRepository)
        {
            _unitOfWork = unitOfWork;
            _InformeDocumentosRepository = InformeDocumentosRepository;
        }

        public InformeDocumentos GetOne (long InformeDocumentosid)
        {
            try
            {
                return _InformeDocumentosRepository.GetOne(InformeDocumentosid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InformeDocumentos LsitaGetOne( )
        {
            try
            {
                return _InformeDocumentosRepository.LsitaGetOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<InformeDocumentos> GetAllLikePagin(InformeDocumentos filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<InformeDocumentos> query = _InformeDocumentosRepository.GetAll();

                var data = query.OrderByDescending(x => x.InformeDocumentosID);

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

        public void Save(InformeDocumentos obj)
        {
            try
            {
                _InformeDocumentosRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
