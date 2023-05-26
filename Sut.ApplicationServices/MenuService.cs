using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using Sut.Log;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class MenuService : IMenuService
    {
        private readonly ILogService<MenuService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuRepository _MenuRepository;

        public MenuService(IUnitOfWork unitOfWork,
                            IMenuRepository MenuRepository)
        {
            _logger = new LogService<MenuService>();
            _unitOfWork = unitOfWork;
            _MenuRepository = MenuRepository;
        }



        
        public List<Menu> GetByMenu()
        {
            try
            {
                return _MenuRepository.GetByMenu();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Menu> GetByMenuConfig(long id, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Menu> query = _MenuRepository.GetAllid(id);

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



        public List<Menu> GetByMenuidpadre(long menuid)
        {
            try
            {
                return _MenuRepository.GetByMenuidpadre(menuid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<Menu> GetByMenusub()
        {
            try
            {
                return _MenuRepository.GetByMenusub();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(Menu obj)
        {
            try
            {
                _MenuRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long MenuId)
        {
            try
            {
                _MenuRepository.Delete(new Menu() { MenuId = MenuId });
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
