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
    public class MenuayudaService : IMenuayudaService
    {
        private readonly ILogService<MenuayudaService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuayudaRepository _MenuayudaRepository;

        public MenuayudaService(IUnitOfWork unitOfWork,
                            IMenuayudaRepository MenuayudaRepository)
        {
            _logger = new LogService<MenuayudaService>();
            _unitOfWork = unitOfWork;
            _MenuayudaRepository = MenuayudaRepository;
        }



        
        public List<Menuayuda> GetByMenuayuda(long EntidadId, int Ruta, int menuid)
        {
            try
            {
                return _MenuayudaRepository.GetByMenuayuda(EntidadId, Ruta, menuid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


     

        public List<Menuayuda> GetByEntidad(long EntidadId)
        {
            try
            {
                return _MenuayudaRepository.GetByEntidad(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public Menuayuda GetByone(int menuid, long EntidadId)
        {
            try
            {
                return _MenuayudaRepository.GetByone(menuid, EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Menuayuda GetByoneid(long EntidadId)
        {
            try
            {
                return _MenuayudaRepository.GetByoneid( EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save(Menuayuda obj)
        {
            try
            {
                _MenuayudaRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long MenuayudaId)
        {
            try
            {
                _MenuayudaRepository.Delete(new Menuayuda() { MenuayudaId = MenuayudaId });
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
