using System.Web.Mvc;

namespace Sut.Web.Areas.Formulacion
{
    public class FormulacionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Formulacion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Formulacion_default",
                "Formulacion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}