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
    public class BandejaService : IBandejaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBandejaRepository _BandejaRepository; 
        private readonly IUnidadOrganicaRepository _unidadOrganicaRepository;

        public BandejaService(IUnitOfWork unitOfWork,
                                    IBandejaRepository BandejaRepository, 
                                    IUnidadOrganicaRepository unidadOrganicaRepository)
        {
            _unitOfWork = unitOfWork;
            _BandejaRepository = BandejaRepository; 
            _unidadOrganicaRepository = unidadOrganicaRepository;
        }

        public List<Bandeja> GetAll(long ExpedienteId)
        {
            try
            {
                return _BandejaRepository.GetAll(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Bandeja> GetAllusuario(long UsuarioId)
        {
            try
            {
                return _BandejaRepository.GetAllusuario(UsuarioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public Bandeja GetOne(long BandejaId)
        {
            try
            {
                return _BandejaRepository.GetOne(BandejaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Bandeja> GetAllLikePagin(Bandeja filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Bandeja> query = _BandejaRepository.GetAll(filtro.UsuarioId);

                var data = query.Where(x => x.Usuario.NroDocumento.ToUpper().Contains((string.IsNullOrEmpty(filtro.Usuario.NroDocumento) ? x.Usuario.NroDocumento : filtro.Usuario.NroDocumento).ToUpper()))
                            .OrderBy(x => x.BandejaId);

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
        public void Eliminar(long ExpedienteId)
        {
            try
            {
                _BandejaRepository.Eliminar(ExpedienteId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save(Bandeja obj)
        {
            try
            {
                _BandejaRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save2(List<Bandeja> obj)
        {
            try
            {
                _BandejaRepository.Save2(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
