using Sut.Entities;
using Sut.IApplicationServices;
using Sut.IRepositories;
using Sut.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.ApplicationServices
{
    public class MiembroEquipoService : IMiembroEquipoService
    { 
        private readonly ILogService<MiembroEquipoService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMiembroEquipoRepository _miembroEquipoRepository;

        public MiembroEquipoService(IUnitOfWork unitOfWork,
                            IMiembroEquipoRepository miembroEquipoRepository)
        {
            _logger = new LogService<MiembroEquipoService>();
            _unitOfWork = unitOfWork;
            _miembroEquipoRepository = miembroEquipoRepository;
        }

        public List<MiembroEquipo> GetAllLikePagin(MiembroEquipo filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<MiembroEquipo> query = _miembroEquipoRepository.GetAll();

                var data = query.Where(x => x.NroDocumento.ToUpper().Contains((filtro.NroDocumento ?? x.NroDocumento).ToUpper())
                                        && (x.Cargo ?? "").ToUpper().Contains((filtro.Cargo ?? (x.Cargo ?? "")).ToUpper())
                                        && (x.Correo ?? "").ToUpper().Contains((filtro.Correo ?? (x.Correo ?? "")).ToUpper()) 
                                        && string.Format("{0} {1}, {2}", x.ApePaterno, x.ApeMaterno, x.Nombres).ToUpper().Contains((filtro.Nombres ?? string.Format("{0} {1}, {2}", x.ApePaterno, x.ApeMaterno, x.Nombres)).ToUpper())
                );

                if (filtro.RolEquipoId > 0)
                {
                    data = data.Where(x => x.RolEquipoId == filtro.RolEquipoId);
                }
                if (filtro.FecCreaciontexto != null) { 
                    if (filtro.FecCreaciontexto.ToString().Trim() != "")
                    {

                        data = data.Where(x => x.FecCreacion.ToString().Contains((filtro.FecCreaciontexto ?? x.FecCreacion.ToString()).ToUpper()));
                    }
                }

                if (filtro.ProvinciaId > 0)
                {
                    data = data.Where(x => x.Entidad.ProvinciaId == filtro.ProvinciaId);
                }
                
                if (filtro.EstadoId > 0)
                {
                    if (filtro.EstadoId == 1)
                    {
                        data = data.Where(x => x.Estado == EstadoMiembroEquipo.Activo);
                    }
                    else {
                        data = data.Where(x => x.Estado == EstadoMiembroEquipo.Baja);
                    }
                }

                if (filtro.EntidadId != 0)
                {
                    data = data.Where(x => x.EntidadId == filtro.EntidadId);
                }
                else if (filtro.Entidad != null)
                {
                    if (!string.IsNullOrEmpty(filtro.Entidad.Nombre))
                        data = data.Where(x => x.Entidad != null)
                                    .Where(x => x.Entidad.Nombre.ToUpper().Contains((filtro.Entidad.Nombre ?? x.Entidad.Nombre).ToUpper()));
                }
                
                data = data.OrderBy(x => x.EntidadId)
                    .ThenBy(x => x.NroDocumento);

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

        public List<MiembroEquipo> GetByEntidad(long EntidadId)
        {
            try
            {
                return _miembroEquipoRepository.GetByEntidad(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MiembroEquipo GetOne(long MiembroEquipoId)
        {
            try
            {
                return _miembroEquipoRepository.GetOne(MiembroEquipoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MiembroEquipo GetOne(string NroDocumento)
        {
            try
            {
                return _miembroEquipoRepository.GetOne(NroDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(MiembroEquipo obj)
        {
            try
            {
                _miembroEquipoRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long id)
        {
            try
            {
                _miembroEquipoRepository.Delete(new MiembroEquipo() { MiembroEquipoId = id });
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
