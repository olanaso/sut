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
    public class MetaDatoService : IMetaDatoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMetaDatoRepository _metaDatoRepository;

        public MetaDatoService(IUnitOfWork unitOfWork,
                            IMetaDatoRepository metaDatoRepository)
        {
            _unitOfWork = unitOfWork;
            _metaDatoRepository = metaDatoRepository;
        }

        public List<MetaDato> GetByLista()
        {
            try
            {
                return _metaDatoRepository.GetByLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MetaDato> GetAllLikePagin(MetaDato filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<MetaDato> query = _metaDatoRepository.GetAll().Where(x => x.PadreId== 47).ToList();

                var data = query.Where(x => x.Nombre.ToUpper().Contains((filtro.Nombre ?? x.Nombre).ToUpper())); 

                data = data.OrderBy(x => x.MetaDatoId)
                    .ThenBy(x => x.Nombre);

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
        public List<MetaDato> GetAllLikePaginAsistencia(MetaDato filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<MetaDato> query = _metaDatoRepository.GetAll();

                var data = query.Where(x => x.Nombre.ToUpper().Contains((filtro.Nombre ?? x.Nombre).ToUpper()));


                if (filtro.MetaDatoId > 0) data = data.Where(x => x.PadreId == filtro.MetaDatoId);


                data = data.OrderBy(x => x.MetaDatoId)
                    .ThenBy(x => x.Nombre);

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

        

        public List<MetaDato> GetByParent(long PadreId)
        {
            try
            {
                return _metaDatoRepository.GetByParent(PadreId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MetaDato GetOne(long MetaDatoId)
        {
            try
            {
                return _metaDatoRepository.GetOne(MetaDatoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(MetaDato obj)
        {
            try
            {
                _metaDatoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long MetaDatoId)
        {
            try
            {
                _metaDatoRepository.Eliminar(MetaDatoId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
