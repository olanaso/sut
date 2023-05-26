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
    public class UITService : IUITService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUITRepository _UITRepository;

        public UITService(IUnitOfWork unitOfWork,
                            IUITRepository UITRepository)
        {
            _unitOfWork = unitOfWork;
            _UITRepository = UITRepository;
        }

        public UIT GetOne (long UITid)
        {
            try
            {
                return _UITRepository.GetOne(UITid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UIT LsitaGetOne( )
        {
            try
            {
                return _UITRepository.LsitaGetOne();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UIT> GetAllLikePagin(UIT filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<UIT> query = _UITRepository.GetAll();

                var data = query.Where(x => x.Descripcion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Descripcion) ? x.Descripcion : filtro.Descripcion).ToUpper()))
                            .OrderByDescending(x => x.UITID);

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

        public void Save(UIT obj)
        {
            try
            {
                _UITRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
