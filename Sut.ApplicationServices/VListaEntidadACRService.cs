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
    public class VListaEntidadACRService : IVListaEntidadACRService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVListaEntidadACRRepository _VListaEntidadACRRepository;

        public VListaEntidadACRService(IUnitOfWork unitOfWork,
                            IVListaEntidadACRRepository VListaEntidadACRRepository)
        {
            _unitOfWork = unitOfWork;
            _VListaEntidadACRRepository = VListaEntidadACRRepository;
        }

      
    

        public List<VListaEntidadACR> GetAll()
        {
            try
            {
                return _VListaEntidadACRRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
             
        }

        public List<VListaEntidadACR> GetAllLikePagin(VListaEntidadACR filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<VListaEntidadACR> query = _VListaEntidadACRRepository.GetAllproceso();

                var data = query.Where(x => x.nombre.ToUpper().Contains((string.IsNullOrEmpty(filtro.nombre) ? x.nombre : filtro.nombre).ToUpper())
                                        && x.Codigo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Codigo) ? x.Codigo : filtro.Codigo).ToUpper())
                                        && x.Acronimo.ToUpper().Contains((string.IsNullOrEmpty(filtro.Acronimo) ? x.Acronimo : filtro.Acronimo).ToUpper())
                                    );

                //if (filtro.NivelGobiernoId > 0) data = data.Where(x => x.NivelGobiernoId == filtro.NivelGobiernoId);
                //if (filtro.SectorId > 0) data = data.Where(x => x.SectorId == filtro.SectorId);

                data = data.OrderBy(x => x.nombre);

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
