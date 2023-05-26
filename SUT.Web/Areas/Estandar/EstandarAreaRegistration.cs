using System.Web.Mvc;

namespace Sut.Web.Areas.Estandar
{
    public class EstandarAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Estandar";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Estandar_default",
                "Estandar/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}