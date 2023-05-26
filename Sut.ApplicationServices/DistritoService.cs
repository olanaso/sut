using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using Sut.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class DistritoService : IDistritoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistritoRepository _distritoRepository;

        public DistritoService(IUnitOfWork unitOfWork,
                                IDistritoRepository distritoRepository)
        {
            _unitOfWork = unitOfWork;
            _distritoRepository = distritoRepository;
        }

        public List<Distrito> GetByProvincia(long ProvinciaId)
        {
            try
            {
                return _distritoRepository.GetByProvincia(ProvinciaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Distrito> GetByLista()
        {
            try
            {
                return _distritoRepository.GetByLista();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Distrito> GetAllLikePagin(Distrito filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                //List<Distrito> query = _distritoRepository.GetAll().Where(x => x.ProvinciaId != 197).ToList();


                List<Distrito> query = _distritoRepository.GetByLista();

                var data = query.Where(x => x.Nombre.ToUpper().Contains((filtro.Nombre ?? x.Nombre).ToUpper()));

            

                if (filtro.Provincia.DepartamentoId > 0)
                {
                    data = data.Where(x => x.Provincia.Departamento.DepartamentoId == filtro.Provincia.DepartamentoId);
                    if (filtro.ProvinciaId > 0) data = data.Where(x => x.ProvinciaId == filtro.ProvinciaId);
                }


                data = data.OrderBy(x => x.DistritoId)
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

        public List<Distrito> GetByParent(long PadreId)
        {
            try
            {
                return _distritoRepository.GetByParent(PadreId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Distrito GetOne(long DistritoId)
        {
            try
            {
                return _distritoRepository.GetOne(DistritoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Distrito obj)
        {
            try
            {
                _distritoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Eliminar(long DistritoId)
        {
            try
            {
                _distritoRepository.Eliminar(DistritoId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
