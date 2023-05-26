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
    public class RolesService : IRolesService
    {
        private readonly ILogService<RolesService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRolesRepository _RolesRepository;

        public RolesService(IUnitOfWork unitOfWork,
                            IRolesRepository RolesRepository)
        {
            _logger = new LogService<RolesService>();
            _unitOfWork = unitOfWork;
            _RolesRepository = RolesRepository;
        }



        public List<Roles> GetByRoles(Roles filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Roles> query = _RolesRepository.GetAll();

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


        public List<Roles> GetAll()
        {
            try
            {
                return _RolesRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public Roles GetByone(long rolId)
        {
            try
            {
                return _RolesRepository.GetByone(rolId);
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
                _RolesRepository.Eliminar(rolId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Roles obj)
        {
            try
            {
                _RolesRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
