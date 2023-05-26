using Sut.IApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Entities;
using Sut.IRepositories;

namespace Sut.ApplicationServices
{
    public class FeriadosAnualesService : IFeriadosAnualesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeriadosAnualesRepository _FeriadosAnualesRepository;

        public FeriadosAnualesService(IUnitOfWork unitOfWork,
                            IFeriadosAnualesRepository FeriadosAnualesRepository)
        {
            _unitOfWork = unitOfWork;
            _FeriadosAnualesRepository = FeriadosAnualesRepository;
        }

        public List<FeriadosAnuales> GetByLista()
        {
            try
            {
                return _FeriadosAnualesRepository.GetByLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FeriadosAnuales> GetAllLikePagin(FeriadosAnuales filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<FeriadosAnuales> query = _FeriadosAnualesRepository.GetAll().ToList();

                var data = query;

                data = data.OrderBy(x => x.FeriadosAnualesId).ToList();

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
        public List<FeriadosAnuales> GetAllLikePaginAsistencia(FeriadosAnuales filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<FeriadosAnuales> query = _FeriadosAnualesRepository.GetAll();

                var data = query;



                data = data.OrderBy(x => x.FeriadosAnualesId).ToList(); 

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

        

        public List<FeriadosAnuales> GetByParent(long PadreId)
        {
            try
            {
                return _FeriadosAnualesRepository.GetByParent(PadreId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FeriadosAnuales GetOne(long FeriadosAnualesId)
        {
            try
            {
                return _FeriadosAnualesRepository.GetOne(FeriadosAnualesId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(FeriadosAnuales obj)
        {
            try
            {
                _FeriadosAnualesRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long FeriadosAnualesId)
        {
            try
            {
                _FeriadosAnualesRepository.Eliminar(FeriadosAnualesId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
