using System.Web.Mvc;

namespace Sut.Web.Areas.Simplificacion
{
    public class SimplificacionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Simplificacion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Simplificacion_default",
                "Simplificacion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}