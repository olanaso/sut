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
    public class UsuarioRolService : IUsuarioRolService
    {
        private readonly ILogService<UsuarioRolService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRolRepository _UsuarioRolRepository;

        public UsuarioRolService(IUnitOfWork unitOfWork,
                            IUsuarioRolRepository UsuarioRolRepository)
        {
            _logger = new LogService<UsuarioRolService>();
            _unitOfWork = unitOfWork;
            _UsuarioRolRepository = UsuarioRolRepository;
        }



        
        public List<UsuarioRol> GetByUsuarioRol(long UsuarioId)
        {
            try
            {
                return _UsuarioRolRepository.GetByUsuarioRol(UsuarioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<UsuarioRol> GetByEntidad(long EntidadId)
        {
            try
            {
                return _UsuarioRolRepository.GetByEntidad(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsuarioRol GetByone(long UsuarioId)
        {
            try
            {
                return _UsuarioRolRepository.GetByone(UsuarioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(UsuarioRol obj)
        {
            try
            {
                _UsuarioRolRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long UsuarioRolId)
        {
            try
            {
                _UsuarioRolRepository.Delete(new UsuarioRol() { UsuarioRolId = UsuarioRolId });
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
