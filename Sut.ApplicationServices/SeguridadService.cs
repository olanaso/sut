using Sut.IApplicationServices;
using Sut.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sut.Entities;
using Sut.IRepositories;

namespace Sut.ApplicationServices
{
    public class SeguridadService : ISeguridadService
    {
        //private readonly ICauClient _cauClient;
        private readonly IEntidadRepository _entidadRepository;
        private readonly IInductorRepository _inductorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SeguridadService (//ICauClient cauClient,
                                IEntidadRepository entidadRepository,
                                IInductorRepository inductorRepository,
                                IUnitOfWork unitOfWork)
        {
            //_cauClient = cauClient;
            _entidadRepository = entidadRepository;
            _inductorRepository = inductorRepository;
            _unitOfWork = unitOfWork;
        }

        public Usuario GetUsuario(string tipo, string usuario)
        {
            try
            {
                //Dictionary<string, string> d = _cauClient.GetUsuario(tipo, usuario);
                Usuario user = new Usuario();
                //Usuario user = new Usuario()
                //{
                //    TipoDocumento = d["TipoDocumento"],
                //    NroDocumento = d["NroDocumento"],
                //    Cargo = d["Cargo"],
                //    ApePaterno = d["ApePaterno"],
                //    ApeMaterno = d["ApeMaterno"],
                //    Nombres = d["Nombres"],
                //    Correo = d["Correo"]
                //};

                //Entidad oEntidad = _entidadRepository.GetOne(d["CodEntidad"]);
                //if (oEntidad == null)
                //{
                //    oEntidad = new Entidad()
                //    {
                //        Codigo = d["CodEntidad"],
                //        Nombre = d["NomEntidad"],
                //        TipoTupa = TipoTupa.Normal
                //    };
                //    _entidadRepository.Guardar(oEntidad);
                //    _unitOfWork.SaveChanges();

                //    Inductor ind = new Inductor()
                //    {
                //        Codigo = "01",
                //        Nombre = "Cantidad de Personas",
                //        Activo = true,
                //        EntidadId = oEntidad.EntidadId,
                //        FecCreacion = DateTime.Now,
                //        FecModificacion = DateTime.Now,
                //        UserCreacion = user.NroDocumento,
                //        UserModificacion = user.NroDocumento
                //    };
                //    _inductorRepository.Insert(ind);
                //    _unitOfWork.SaveChanges();
                //}

                //user.Entidad = oEntidad;
                //user.Rol = (Rol)(short.Parse(d["CodRol"]));

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidarUsuario(string tipo, string usuario, string clave)
        {
            //return _cauClient.ValidarUsuario(tipo, usuario, clave);
            return true;
        }
    }
}
