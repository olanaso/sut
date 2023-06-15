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
    public class UsuarioService : IUsuarioService
    {
        private readonly ILogService<UsuarioService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUnitOfWork unitOfWork,
                            IUsuarioRepository usuarioRepository)
        {
            _logger = new LogService<UsuarioService>();
            _unitOfWork = unitOfWork;
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Validar(string NroDocumento, string Clave, int TipoDoc) 
        {
            try
            {
                Usuario user = _usuarioRepository.GetOne2(NroDocumento, TipoDoc);
                if (user == null) return null;

                string ver=Cryptography.Decrypt("JLrLLVYoKvP1q6/RZfX4Yg==");
                if (user.Clave == Cryptography.Encrypt(Clave)) return user;

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> GetAllLikePagin(Usuario filtro, int pageIndex, int pageSize, ref int totalRows)
        {
            try
            {
                List<Usuario> query = _usuarioRepository.GetAll();
                
                var data = query.Where(x => x.NroDocumento.ToUpper().Contains((filtro.NroDocumento ?? x.NroDocumento).ToUpper()));
                
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

                /*FVN*/
                if (filtro.MiembroEquipo != null)
                {
                    if (!string.IsNullOrEmpty(filtro.MiembroEquipo.NombreCompletoBuscar))
                    {
                        var dataNombres = data.Where(x => x.MiembroEquipo != null && x.MiembroEquipo.Nombres != null)
                            .Where(x => x.MiembroEquipo.Nombres.ToUpper().Contains((filtro.MiembroEquipo.NombreCompletoBuscar ?? x.MiembroEquipo.Nombres).ToUpper()));

                        var dataApePat = data.Where(x => x.MiembroEquipo != null && x.MiembroEquipo.ApePaterno != null)
                            .Where(x => x.MiembroEquipo.ApePaterno.ToUpper().Contains((filtro.MiembroEquipo.NombreCompletoBuscar ?? x.MiembroEquipo.ApePaterno).ToUpper()));

                        var dataApeMat = data.Where(x => x.MiembroEquipo != null && x.MiembroEquipo.ApeMaterno != null)
                            .Where(x => x.MiembroEquipo.ApeMaterno.ToUpper().Contains((filtro.MiembroEquipo.NombreCompletoBuscar ?? x.MiembroEquipo.ApeMaterno).ToUpper()));

                        if (dataNombres.Count() > 0)
                        {
                            data = dataNombres;
                        }
                        if (dataApePat.Count() > 0)
                        {
                            data = dataApePat;
                        }
                        if (dataApeMat.Count() > 0)
                        {
                            data = dataApeMat;
                        }


                    }
                }

                if (filtro.FilterRol > 0)
                {
                    data = data.Where(x => (short)x.Rol == filtro.FilterRol);
                }

                if (filtro.FecCreaciontexto != null)
                {
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
                        data = data.Where(x => x.Estado == EstadoUsuario.Activo);
                    }
                    else if (filtro.EstadoId == 2)
                    {
                        data = data.Where(x => x.Estado == EstadoUsuario.Suspendido);
                    }
                    else 
                    {
                        data = data.Where(x => x.Estado == EstadoUsuario.Baja);
                    } 
              
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
        
        public Usuario GetOne(long UsuarioId)
        {
            try
            {
                return _usuarioRepository.GetOne(UsuarioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario GetOnecompleto(long UsuarioId)
        {
            try
            {
                return _usuarioRepository.GetOnecompleto(UsuarioId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario GetOne(string NroDocumento)
        {
            try
            {
                return _usuarioRepository.GetOne(NroDocumento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario GetOnerol(string NroDocumento, Rol rol)
        {
            try
            {
                return _usuarioRepository.GetOnerol(NroDocumento, rol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Usuario> GetByEntidad(long EntidadId)
        {
            try
            {
                return _usuarioRepository.GetByEntidad(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsuarioRol> GetByEntidadROL(long EntidadId)
        {
            try
            {
                return _usuarioRepository.GetByEntidadROL(EntidadId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(Usuario obj)
        {
            try
            {
                _usuarioRepository.Save(obj);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveRoles(Usuario obj)
        {
            try
            {
                _usuarioRepository.SaveRoles(obj);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long UsuarioId)
        {
            try
            {
                _usuarioRepository.Delete(new Usuario() { UsuarioId = UsuarioId });
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
