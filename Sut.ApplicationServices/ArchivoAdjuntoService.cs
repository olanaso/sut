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
    public class ArchivoAdjuntoService : IArchivoAdjuntoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArchivoAdjuntoRepository _archivoAdjuntoService;

        public ArchivoAdjuntoService (IUnitOfWork unitOfWork,
                                        IArchivoAdjuntoRepository archivoAdjuntoRepository)
        {
            _unitOfWork = unitOfWork;
            _archivoAdjuntoService = archivoAdjuntoRepository;
        }

        public ArchivoAdjunto GetOne (long ArchivoAdjuntoId)
        {
            try
            {
                return _archivoAdjuntoService.GetOne(ArchivoAdjuntoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(ArchivoAdjunto obj)
        {
            try
            {
                _archivoAdjuntoService.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
