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
    public class CorreosService : ICorreosService
    {
        private readonly ILogService<CorreosService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICorreosRepository _CorreosRepository;

        public CorreosService(IUnitOfWork unitOfWork,
                            ICorreosRepository CorreosRepository)
        {
            _logger = new LogService<CorreosService>();
            _unitOfWork = unitOfWork;
            _CorreosRepository = CorreosRepository;
        }



        public List<Correos> GetByCorreos(Correos filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Correos> query = _CorreosRepository.GetAll();

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


        public List<Correos> GetAll()
        {
            try
            {
                return _CorreosRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public Correos GetByone(long rolId)
        {
            try
            {
                return _CorreosRepository.GetByone(rolId);
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
                _CorreosRepository.Eliminar(rolId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Correos obj)
        {
            try
            {
                _CorreosRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
