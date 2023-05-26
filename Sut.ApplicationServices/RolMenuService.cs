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
    public class RolMenuService : IRolMenuService
    {
        private readonly ILogService<RolMenuService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRolMenuRepository _RolMenuRepository;

        public RolMenuService(IUnitOfWork unitOfWork,
                            IRolMenuRepository RolMenuRepository)
        {
            _logger = new LogService<RolMenuService>();
            _unitOfWork = unitOfWork;
            _RolMenuRepository = RolMenuRepository;
        }




        public void Guardarrolmenu(List<RolMenu> lstRolMenu)
        {
            try
            {
                _RolMenuRepository.Guardarrolmenu(lstRolMenu);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RolMenu> GetByRolMenu(long rolId)
        {
            try
            {
                return _RolMenuRepository.GetByRolMenu(rolId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<RolMenu> GetByRolMenuid(long rolId, long menuId)
        {
            try
            {
                return _RolMenuRepository.GetByRolMenuid(rolId, menuId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     


        public void Save(RolMenu obj)
        {
            try
            {
                _RolMenuRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Eliminar(long rolId)
        {
            try
            {
                _RolMenuRepository.Eliminar(rolId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
