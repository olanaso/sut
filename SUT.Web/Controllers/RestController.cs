using Newtonsoft.Json;
using Sut.IApplicationServices;
using Sut.Log;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
/*jaime*/
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.ClientServices.Providers;
using Sut.Entities;
using Sut.Security;
using Sut.Web.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using CaptchaMvc.HtmlHelpers;
using System.Configuration;  
/*fin*/



namespace Sut.Web.Controllers
{

    [AllowAnonymous]
    public class RestController : Controller
    {
        private readonly ILogService<RestController> _log;
        private readonly IUsuarioService _usuarioService;
        private readonly IEntidadService _entidadService;
        private readonly IUsuarioRolService _usuariorolService;
        private readonly IMetaDatoService _metaDatoService;
        private readonly IIngresoSUTService _ingresoSUTService;
        private readonly IMenuService _menuService;
        private readonly IRolMenuService _rolMenuService;

        public RestController(IUsuarioService usuarioService,
                                    IEntidadService entidadService,
                                    IMetaDatoService metaDatoService,
                                    IUsuarioRolService usuariorolService,
                                    IIngresoSUTService ingresoSUTService,
                                    IMenuService menuService,
                                    IRolMenuService rolMenuService)
        {
            _log = new LogService<RestController>();
            _usuarioService = usuarioService;
            _entidadService = entidadService;
            _usuariorolService = usuariorolService;
            _metaDatoService = metaDatoService;
            _ingresoSUTService = ingresoSUTService;
            _menuService = menuService;
            _rolMenuService = rolMenuService;
        }



        [HttpGet]
        [AllowAnonymous]
        //public ActionResult resolver(string t)
        //{
        //    string returnUrl = "http://desa210.pcm.gob.pe:4200/login";
          
        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", t);
        //    var url = "http://desa210.pcm.gob.pe:21000/api/Seguridad/autorizacionSUT";
        //    var response =   client.GetAsync(url).Result;
        //    var adas = response;

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        Console.WriteLine("Error: " + response.StatusCode);

               
        //        return Redirect(returnUrl);
                

        //    }



        //    var content = response.Content.ReadAsStringAsync().Result;
        //    var serializer = new JavaScriptSerializer();
        //    var dataRespuesta = serializer.Deserialize<Root>(content);




        //    LoginModel model = new LoginModel();

        //    if (dataRespuesta.d.tipo_documento == 1) 
        //    {
        //        model.TipoDoc = 148;
        //    }
        //    else { 
        //        model.TipoDoc = 149;
        //    }


        //    ////**************nuevo codigo jaime******************//
        //    model.UserName = "11111111";
        //    model.Password = "P@ssw0rd11111111";


        //    if (model.PasswordOld != null && model.PasswordOld != string.Empty)
        //    {

        //        model.PasswordOld = AESEncrytDecry.DecryptStringAES(model.PasswordOld);
        //    }

        //    if (model.PasswordNew != null && model.PasswordNew != string.Empty)
        //    {
        //        model.PasswordNew = AESEncrytDecry.DecryptStringAES(model.PasswordNew);
        //    }


        //    ////**************fin jaime******************//

        //    /*if (this.IsCaptchaValid("Validar captcha"))
        //    {
        //        ViewBag.ErrMessage = "Validation Messgae";
        //    }*/
        //    ////**************nuevo codigo******************//
        //    /*  model.UserName =  AESEncrytDecry.DecryptStringAES(model.UserNameEncrypt);
        //      model.Password = AESEncrytDecry.DecryptStringAES(model.Password);

        //      var clave = Cryptography.Decrypt("TKVkfsfIfO+mj7+/HJP4GQ==");*/

        //    List<MetaDato> listtipodoc = _metaDatoService.GetByParent(38);


        //    if (model.PasswordNew != null && model.PasswordOld != null)
        //    {
        //        Usuario userOld = _usuarioService.Validar(model.UserName, model.PasswordOld, model.TipoDoc);
        //        if (userOld != null)
        //        {
        //            model.NombreCompleto = userOld.MiembroEquipo.NombreCompleto;

        //            if (CambiarContraseña(model))
        //            {
        //                model.Password = model.PasswordNew;
        //            }
        //            else
        //            {
        //                ViewBag.TipoDoc = listtipodoc.Select(x => new SelectListItem()
        //                {
        //                    Value = x.MetaDatoId.ToString(),
        //                    Text = x.Nombre
        //                }).ToList();

        //                // model.JavascriptToRun = "FMostrarModal()";
        //                ModelState.Clear();

        //                model = new LoginModel();
        //                ModelState.AddModelError(string.Empty, "La contraseña debe contener al menos 10 caracteres, una letra mayúscula y una minúscula, un número y un carácter especial  \"-(@/&$#!%_?*)");
        //                return View(model);
        //            }


        //        }
        //        else
        //        {
        //            ViewBag.TipoDoc = listtipodoc.Select(x => new SelectListItem()
        //            {
        //                Value = x.MetaDatoId.ToString(),
        //                Text = x.Nombre
        //            }).ToList();

        //            //model.JavascriptToRun = "ShowValidatedPassword()";
        //            ModelState.AddModelError(string.Empty, "La contraseña es incorrecta.");
        //            return View(model);
        //        }

        //    }

        //    Usuario user = _usuarioService.Validar(model.UserName, model.Password, model.TipoDoc);

        //    if (user != null)
        //    {


        //        var regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[(\-)#?!@$%/^&*_\""]).{10,}$");

        //        if (!regex.IsMatch(model.Password) && model.NombreCompleto != "omitir")
        //        {
        //            ViewBag.TipoDoc = listtipodoc.Select(x => new SelectListItem()
        //            {
        //                Value = x.MetaDatoId.ToString(),
        //                Text = x.Nombre
        //            })
        //            .ToList();
        //            // ModelState.AddModelError(string.Empty, "Se requiere el cambio de clave");

        //            model.JavascriptToRun = "ShowValidatedPassword()";
        //            model.NombreCompleto = user.MiembroEquipo.NombreCompleto;
        //            string fechaMaximaCambioClave = ConfigurationManager.AppSettings["Sut.LastDateChangePassword"].ToString();

        //            TimeSpan difFechas = Convert.ToDateTime(fechaMaximaCambioClave) - Convert.ToDateTime(DateTime.Now.ToShortDateString());
        //            model.DaysChangePassword = difFechas.Days.ToString();
        //            model.UserName = "";
        //            model.Password = "";
        //            return View(model);

        //        }

        //        List<UsuarioRol> usurol = _usuariorolService.GetByUsuarioRol(user.UsuarioId).OrderBy(x => x.Rol).ToList();
        //        List<SelectListItem> rol = new List<SelectListItem>();
        //        int valroles = 0;
        //        foreach (UsuarioRol roles in usurol)
        //        {

        //            if ((short)roles.Rol != valroles)
        //            {
        //                string valor = Convert.ToString((short)roles.Rol);
        //                rol.Add(new SelectListItem() { Text = Enum.GetName(typeof(Rol), roles.Rol), Value = valor });
        //            }
        //            valroles = (short)roles.Rol;

        //        }


        //        UsuarioInfo data = new UsuarioInfo();
        //        data.UsuarioId = user.UsuarioId;
        //        data.NroDocumento = user.NroDocumento;
        //        data.ApePaterno = user.MiembroEquipo.ApePaterno;
        //        data.ApeMaterno = user.MiembroEquipo.ApeMaterno;
        //        data.Nombres = user.MiembroEquipo.Nombres;
        //        data.NombreCompleto = user.MiembroEquipo.NombreCompleto;
        //        data.Correo = user.MiembroEquipo.Correo;
        //        //if ((short)user.Rol == 10)
        //        //{
        //        //    data.Rol = 1;
        //        //}
        //        //else { data.Rol = (short)user.Rol; }


        //        data.Rol = (short)user.Rol; /// 


        //        data.Sexo = (short)user.MiembroEquipo.Sexo;
        //        data.NomRol = Enum.GetName(typeof(Rol), user.Rol);
        //        //if ((short)user.Rol == 10)
        //        //{
        //        //    data.MultipleRol = 0;
        //        //}
        //        //else { data.MultipleRol = rol.Count; }

        //        data.MultipleRol = rol.Count;

        //        data.Ayuda = user.Ayuda;
        //        ViewBag.rolmultiples = rol;


        //        foreach (var md in (rol as List<SelectListItem>))
        //        {
        //            if (md.Text == "Coordinador")
        //            {
        //                md.Text = "Administrador Sut";
        //            }
        //        }

        //        System.Web.HttpContext.Current.Session["roles"] = rol;


        //        Entidad entidad = user.Entidad;
        //        data.EntidadId = entidad.EntidadId;
        //        data.CodEntidad = entidad.Codigo;
        //        data.NomEntidad = entidad.Nombre;
        //        data.TipoTupa = (short)entidad.TipoTupa;
        //        data.Sector = (short)entidad.SectorId;
        //        data.CodPadre = entidad.CodPadre;
        //        data.procesogratuito = entidad.ProcesoGratuito;
        //        data.EstadoMinistrio = (short)entidad.EstadoMinistrio;
        //        data.ActivarMultiUsuario = entidad.ActivarMultiUsuario;
        //        data.ActivarExpediente = entidad.ActivarExpediente;
        //        data.Bandeja = entidad.Bandeja;
        //        if (entidad.ProvinciaId != null)
        //        {

        //            data.Provincia = (short)entidad.ProvinciaId;
        //        }
        //        data.CodPadre = entidad.CodPadre;
        //        data.DJ = entidad.DJ;
        //        /*Ingreso Al Aplicativo*/
        //        IngresoSUT ingreso = new IngresoSUT();
        //        ingreso.Nombre = user.NroDocumento + " - " + user.MiembroEquipo.NombreCompleto;
        //        ingreso.Usuarioid = user.UsuarioId;
        //        ingreso.FechaHoraEntrada = DateTime.Now;
        //        _ingresoSUTService.Save(ingreso);
        //        /*Ingreso Al Aplicativo*/



        //        string userData = JsonConvert.SerializeObject(data);
        //        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "ticket", DateTime.Now, DateTime.Now.AddMinutes(220d), false, userData, FormsAuthentication.FormsCookiePath);
        //        string encryptedTicket = FormsAuthentication.Encrypt(ticket);
        //        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //        authCookie.Expires = DateTime.Now.AddMinutes(220d);
        //        //authCookie.Expires = DateTime.Now.AddMinutes(10d);

        //        Response.Cookies.Add(authCookie);

        //        //FormsAuthentication.SetAuthCookie(model.UserName, true);

        //        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
        //            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
        //        {
        //            return Redirect(returnUrl);
        //        }

        //        return Redirect(FormsAuthentication.GetRedirectUrl(user.NroDocumento, false));
        //        //return RedirectToAction("Index", "Inicio");
        //    }

        //    ViewBag.TipoDoc = listtipodoc.Select(x => new SelectListItem()
        //    {
        //        Value = x.MetaDatoId.ToString(),
        //        Text = x.Nombre
        //    })
        //    .ToList();
        //    ModelState.AddModelError(string.Empty, "El tipo documento o usuario y contraseña son incorrectos");
        //    model = new LoginModel();
        //    return View(model);




        //}




        public ActionResult roles(UsuarioInfo user, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            ViewBag.rolmultiples = System.Web.HttpContext.Current.Session["roles"];


            var asd = 0;
            foreach (var mode in ViewBag.rolmultiples)
            {
                asd = asd + 1;
            }
            if (asd > 1)
            {
                user.MultipleRol = asd;
            }


            string userData = JsonConvert.SerializeObject(user);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "ticket", DateTime.Now, DateTime.Now.AddMinutes(220d), false, userData, FormsAuthentication.FormsCookiePath);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authCookie.Expires = DateTime.Now.AddMinutes(220d);
            //authCookie.Expires = DateTime.Now.AddMinutes(10d);

            Response.Cookies.Add(authCookie);

            //FormsAuthentication.SetAuthCookie(model.UserName, true);

            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                return Redirect(returnUrl);
            }
            return Redirect(FormsAuthentication.GetRedirectUrl(user.NroDocumento, false));


        }


        private bool CambiarContraseña(LoginModel model)
        {
            bool resultado = false;

            Usuario user = _usuarioService.Validar(model.UserName, model.PasswordOld, model.TipoDoc);

            if (user != null)
            {
                var regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[(\-)#?!@$%/^&*_\""]).{10,}$");

                if (!regex.IsMatch(model.PasswordNew))
                {
                    return false;
                }
                else
                {
                    user.Clave = Cryptography.Encrypt(model.PasswordNew);
                    user.UserModificacion = user.NroDocumento;
                    user.FecModificacion = DateTime.Now;
                    _usuarioService.Save(user);

                    return true;
                }
            }
            return resultado;

        }
        public ActionResult Loginvalidado(string roles, UsuarioInfo data)
        {


            data.validrol = data.Rol;
            if (roles == "1")
            {
                data.Ayuda = 0;
                data.Rol = (short)Rol.Administrador;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.Administrador);
            }
            if (roles == "2")
            {
                data.Rol = (short)Rol.Coordinador;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.Coordinador);
            }
            if (roles == "3")
            {
                data.Rol = (short)Rol.RegistradorProcesos;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.RegistradorProcesos);
            }
            if (roles == "4")
            {
                data.Rol = (short)Rol.RegistradorLegal;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.RegistradorLegal);
            }
            if (roles == "5")
            {
                data.Rol = (short)Rol.RegistradorCostos;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.RegistradorCostos);
            }
            if (roles == "6")
            {
                data.Ayuda = 0;
                data.Rol = (short)Rol.Ratificador;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.Ratificador);
            }
            if (roles == "7")
            {
                data.Ayuda = 0;
                data.Rol = (short)Rol.Evaluador;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.Evaluador);
            }
            if (roles == "8")
            {
                data.Ayuda = 0;
                data.Rol = (short)Rol.EntidadFiscalizadora;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.EntidadFiscalizadora);
            }
            if (roles == "9")
            {
                data.Ayuda = 0;
                data.Rol = (short)Rol.Incentivos;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.Incentivos);
            }
            if (roles == "10")
            {
                data.Ayuda = 0;
                data.Rol = (short)Rol.usuariolocador;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.usuariolocador);
            }

            if (roles == "11")
            {
                data.Ayuda = 0;
                data.Rol = (short)Rol.AccesoEstandar;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.AccesoEstandar);
            }
            if (roles == "12")
            {
                data.Ayuda = 0;
                data.Rol = (short)Rol.EvaluadorCoordinador;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.EvaluadorCoordinador);
            }
            if (roles == "14")
            {
                data.Ayuda = 0;
                data.Rol = (short)Rol.CalendarioActividades;
                data.NomRol = Enum.GetName(typeof(Rol), Rol.CalendarioActividades);
            }


            List<Menu> lista = _menuService.GetByMenu();

            List<RolMenu> listarolmenu = _rolMenuService.GetByRolMenu(Convert.ToInt32(roles));

            for (int i = 0; i < listarolmenu.Count(); i++)
            {
                if (lista[i].MenuId == listarolmenu[i].MenuId)
                {
                    lista[i].Ver = listarolmenu[i].Ver;
                }
            }


            System.Web.HttpContext.Current.Session["rolesmenu"] = lista.Where(x => x.Ver == true && x.Estado == 1).ToList();




            //if (data.MultipleRol == 0)
            //{
            //    data.validrol = 1;
            //}

            //if (data.Rol == data.validrol)
            //{
            //    data.validrol = 0;

            //}
            data.MultipleRol = 0;

            //if (data.validrol == 1) 
            //{
            data.validrol = 0;
            //}
            //else 
            //{
            //    data.validrol = 1;
            //}

            //////////////ViewBag.rolmultiples = 0;

            //////////////System.Web.HttpContext.Current.Session["roles"] = 0;



            string userData = JsonConvert.SerializeObject(data);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "ticket", DateTime.Now, DateTime.Now.AddMinutes(220d), false, userData, FormsAuthentication.FormsCookiePath);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authCookie.Expires = DateTime.Now.AddMinutes(220d);
            Response.Cookies.Add(authCookie);

            //FormsAuthentication.SetAuthCookie(model.UserName, true); 

            return Redirect(FormsAuthentication.GetRedirectUrl(data.NroDocumento, false));
            //return RedirectToAction("Index", "Inicio");


        }

        public ActionResult DesactivarAyuda(int activar, UsuarioInfo data)
        {




            data.Ayuda = activar;

            string userData = JsonConvert.SerializeObject(data);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "ticket", DateTime.Now, DateTime.Now.AddMinutes(220d), false, userData, FormsAuthentication.FormsCookiePath);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authCookie.Expires = DateTime.Now.AddMinutes(220d);
            Response.Cookies.Add(authCookie);

            //FormsAuthentication.SetAuthCookie(model.UserName, true); 


            Usuario rep = _usuarioService.GetOne(data.NroDocumento);

            rep.Ayuda = activar;
            _usuarioService.Save(rep);

            return Redirect(FormsAuthentication.GetRedirectUrl(data.NroDocumento, false));
            //return RedirectToAction("Index", "Inicio");


        }

        [AllowAnonymous]
        public ActionResult LogOff() => Redirect(ConfigurationSecurity.LogOut(this));

        public ActionResult AccesoDenegado() => View();
         
    }
   
}
//public class D
//{
//    public int tipo_documento { get; set; }
//    public string documento { get; set; }
//    public string nombres { get; set; }
//    public string sexo { get; set; }
//    public string correo { get; set; }
//    public string celular { get; set; }
//    public int rolesTotal { get; set; }
//    public List<Role> roles { get; set; }
//}

//public class Role
//{
//    public string id { get; set; }
//    public string nombre { get; set; }
//}

//public class Root
//{
//    public int c { get; set; }
//    public object m { get; set; }
//    public D d { get; set; }
//}