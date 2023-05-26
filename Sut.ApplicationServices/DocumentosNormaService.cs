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
    public class DocumentosNormaService : IDocumentosNormaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentosNormaRepository _DocumentosNormaRepository;

        public DocumentosNormaService(IUnitOfWork unitOfWork,
                            IDocumentosNormaRepository DocumentosNormaRepository)
        {
            _unitOfWork = unitOfWork;
            _DocumentosNormaRepository = DocumentosNormaRepository;
        }

        public DocumentosNorma GetOne (long DocumentosNormaid)
        {
            try
            {
                return _DocumentosNormaRepository.GetOne(DocumentosNormaid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentosNorma LsitaGetOne( )
        {
            try
            {
                return _DocumentosNormaRepository.LsitaGetOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentosNorma> GetAllLikePagin(DocumentosNorma filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<DocumentosNorma> query = _DocumentosNormaRepository.GetAll();

                var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                            .OrderByDescending(x => x.FechaPublicacion);

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
        public List<DocumentosNorma> GetAll()
        {
            try
            {
                return _DocumentosNormaRepository.GetAllBy(x => x.Estado == Respuesta.Si);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DocumentosNorma> GetAllLikePaginAdmin(DocumentosNorma filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<DocumentosNorma> query = _DocumentosNormaRepository.GetAllAdmin();

                var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                            .OrderByDescending(x => x.FechaPublicacion);

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

        public void Save(DocumentosNorma obj)
        {
            try
            {
                _DocumentosNormaRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
