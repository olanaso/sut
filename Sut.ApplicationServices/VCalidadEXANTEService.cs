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
    public class VCalidadEXANTEService : IVCalidadEXANTEService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVCalidadEXANTERepository _VCalidadEXANTERepository;

        public VCalidadEXANTEService(IUnitOfWork unitOfWork,
                            IVCalidadEXANTERepository VCalidadEXANTERepository)
        {
            _unitOfWork = unitOfWork;
            _VCalidadEXANTERepository = VCalidadEXANTERepository;
        }

        public VCalidadEXANTE GetOne (long VCalidadEXANTEid)
        {
            try
            {
                return _VCalidadEXANTERepository.GetOne(VCalidadEXANTEid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<VCalidadEXANTE> GetAllexante(int ICODCALIDADEXANTE)
        {
            try
            {
                return _VCalidadEXANTERepository.GetAllexante(ICODCALIDADEXANTE);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public List<VCalidadEXANTE> GetAll(long CodEntidad)
        {
            try
            {
                return _VCalidadEXANTERepository.GetAll(CodEntidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }

        public List<VCalidadEXANTE> GetAllLikePagin(VCalidadEXANTE filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<VCalidadEXANTE> query = _VCalidadEXANTERepository.GetAll(filtro.EntidadId);
                

                var data = query.Where(x => x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                                        && x.Nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.Nombre) ? x.Nombre : filtro.Nombre).ToUpper())
                                        && x.Acronimo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Acronimo) ? x.Acronimo : filtro.Acronimo).ToUpper())
                                        && x.nomproyecto.ToUpper().Contains((string.IsNullOrEmpty(filtro.nomproyecto) ? x.nomproyecto : filtro.nomproyecto).ToUpper())
                                    && x.Denominacion.ToUpper().Contains((string.IsNullOrEmpty(filtro.Denominacion) ? x.Denominacion : filtro.Denominacion).ToUpper())
                                    );

                //if (filtro.NivelGobiernoId > 0) data = data.Where(x => x.NivelGobiernoId == filtro.NivelGobiernoId);
                //if (filtro.SectorId > 0) data = data.Where(x => x.SectorId == filtro.SectorId);

                data = data.OrderBy(x => x.Denominacion);

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


    }
}
