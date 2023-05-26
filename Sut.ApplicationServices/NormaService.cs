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
    public class NormaService : INormaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INormaRepository _normaRepository;

        public NormaService(IUnitOfWork unitOfWork,
                                INormaRepository normaService)
        {
            _unitOfWork = unitOfWork;
            _normaRepository = normaService;
        }

        public List<Norma> GetAll()
        {
            try
            {
                return _normaRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Norma GetOne(long NormaId)
        {
            try
            {
                return _normaRepository.GetOne(NormaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Norma> GetAllLikePagin(Norma filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Norma> query = _normaRepository.GetAll();

                var data = query.Where(x => x.Numero.ToUpper().Contains((string.IsNullOrEmpty(filtro.Numero) ? x.Numero : filtro.Numero).ToUpper())
                                                                                && (filtro.TipoNormaId == 0 || filtro.TipoNormaId == x.TipoNormaId)
                                        && (String.IsNullOrEmpty(filtro.Descripcion) || filtro.Descripcion.ToUpper() == x.Descripcion.ToUpper()))
                            .OrderBy(x => x.FechaPublicacion).ThenBy(x => x.Numero);

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

        public void Save(Norma obj)
        {
            try
            {
                _normaRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
