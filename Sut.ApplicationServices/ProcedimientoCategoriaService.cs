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
    public class ProcedimientoCategoriaService : IProcedimientoCategoriaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProcedimientoCategoriaRepository _ProcedimientoCategoriaRepository;

        public ProcedimientoCategoriaService(IUnitOfWork unitOfWork,
                            IProcedimientoCategoriaRepository ProcedimientoCategoriaRepository)
        {
            _unitOfWork = unitOfWork;
            _ProcedimientoCategoriaRepository = ProcedimientoCategoriaRepository;
        }

        public ProcedimientoCategoria GetOne (long ProcedimientoCategoriaid)
        {
            try
            {
                return _ProcedimientoCategoriaRepository.GetOne(ProcedimientoCategoriaid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ProcedimientoCategoria> Lsitaprocedimientocategoria(long ExpedienteId, long ProcedimientoCategoriaid)
        {
            try
            {
                return _ProcedimientoCategoriaRepository.Lsitaprocedimientocategoria(ExpedienteId, ProcedimientoCategoriaid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimientoCategoria> Lsitaprocedimientocategoriavalor0(long ExpedienteId)
        {
            try
            {
                return _ProcedimientoCategoriaRepository.Lsitaprocedimientocategoriavalor0(ExpedienteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public List<ProcedimientoCategoria> Lsitaprocedimientocategoriavalor(long ProcedimientoId)
        {
            try
            {
                return _ProcedimientoCategoriaRepository.Lsitaprocedimientocategoriavalor(ProcedimientoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(ProcedimientoCategoria obj)
        {
            try
            {
                _ProcedimientoCategoriaRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(ProcedimientoCategoria obj)
        {
            try
            {
                _ProcedimientoCategoriaRepository.Modificar(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public void Eliminar(ProcedimientoCategoria obj)
        {
            try
            {
                _ProcedimientoCategoriaRepository.Eliminar(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
