using Sut.Entities;
using System.Web.Mvc;

namespace Sut.Web
{
    public static class ExtensionCustom
    {

        public static MvcHtmlString TipoEstadoOpciones(
         this HtmlHelper htmlHelper,
         int EstadoOpcion)
        {
            string[] tipoColor = { "bg-purple", "label-success", "label-primary", "label-info", "label-danger", "label-warning", "label-success" };
            //string[] tipoNom = { "Sin Accion", "En Proceso", "Terminado", "Presentado", "Observado", "Subsanado","Validado" };
            string[] tipoNom = { "Sin Accion", "En Revisión", "Terminado", "Presentado", "Observado", "Subsanado", "Validado" };
            var label = new TagBuilder("label");
            label.AddCssClass("label");
            label.AddCssClass(tipoColor[EstadoOpcion]);
            label.InnerHtml = tipoNom[EstadoOpcion];

            return MvcHtmlString.Create(label.ToString());
        }

        public static MvcHtmlString TipoEstadoOpciones(
            this HtmlHelper htmlHelper,
            EstadoOpcion EstadoOpcion)
        {
            return htmlHelper.TipoEstadoOpciones((int)EstadoOpcion);
        }

        /************************************************************************/
        public static MvcHtmlString TipoEstadoOpciones2(
      this HtmlHelper htmlHelper,
      int EstadoOpcion2)
        {
            string[] tipoColor2 = { "bg-purple2", "label-success2", "label-primary2", "label-info2", "label-danger2", "label-warning2", "label-success2" };
            string[] tipoNom2 = { "Sin Accion", "En Revisión", "Terminado", "Presentado", "Observado", "Subsanado", "Validado" };
            var label = new TagBuilder("label");
            label.AddCssClass("label");
            label.AddCssClass(tipoColor2[EstadoOpcion2]);
            label.InnerHtml = tipoNom2[EstadoOpcion2];

            return MvcHtmlString.Create(label.ToString());
        }

        public static MvcHtmlString TipoEstadoOpciones2(
            this HtmlHelper htmlHelper,
            EstadoOpcion EstadoOpcion2)
        {
            return htmlHelper.TipoEstadoOpciones2((int)EstadoOpcion2);
        }

        /************************************************************************/
        public static MvcHtmlString TipoProcedimiento(
            this HtmlHelper htmlHelper,
            int TipoProcedimiento)
        {
            string[] tipoColor = { "", "bg-purple", "label-primary", "label-warning", "label-warning", "label-warning" };
            string[] tipoNom = { "", "PROCEDIMIENTO ADMINISTRATIVO", "SERVICIO PRESTADO EN EXCLUSIVIDAD", "PROCEDIMIENTO ESTÁNDAR", "", "SERVICIO ESTÁNDAR" };

            var label = new TagBuilder("label");
            label.AddCssClass("label");
            label.AddCssClass(tipoColor[TipoProcedimiento]);
            label.InnerHtml = tipoNom[TipoProcedimiento];

            return MvcHtmlString.Create(label.ToString());
        }

        public static MvcHtmlString TipoProcedimiento(
            this HtmlHelper htmlHelper,
            TipoProcedimiento TipoProcedimiento)
        {
            return htmlHelper.TipoProcedimiento((int)TipoProcedimiento);
        }

        /************************************************************************/

        public static MvcHtmlString TipoOperacion(
            this HtmlHelper htmlHelper,
            int Operacion)
        {
            string[] tpoOpColor = { "", "label-success", "label-primary", "label-danger", "bg-black" };
            string[] tpoOperacion = { "", "NUEVO", "MODIFICACION", "ELIMINACION", "NINGUNA" };

            var label = new TagBuilder("label");
            label.AddCssClass("label");
            label.AddCssClass(tpoOpColor[Operacion]);
            label.InnerHtml = tpoOperacion[Operacion];

            return MvcHtmlString.Create(label.ToString());
        }

        public static MvcHtmlString TipoOperacion(
            this HtmlHelper htmlHelper,
            OperacionExpediente Operacion)
        {
            return htmlHelper.TipoOperacion((int)Operacion);
        }

        /************************************************************************/

        public static MvcHtmlString EstadoExpediente(
            this HtmlHelper htmlHelper,
            int EstadoExpediente)
        {
            string[] tipoColor = { "", "label-primary", "label-warning", "label-success", "label-danger", "bg-gray-active", "label-info", "label-public" };
            string[] tipoNom = { "", "EN PROCESO", "PRESENTADO", "APROBADO", "OBSERVADO", "ANULADO", "SUBSANADO", "PUBLICADO" };

            var label = new TagBuilder("label");
            label.AddCssClass("label");
            label.AddCssClass(tipoColor[EstadoExpediente]);
            label.InnerHtml = tipoNom[EstadoExpediente];

            return MvcHtmlString.Create(label.ToString());
        }

        public static MvcHtmlString EstadoExpediente(
            this HtmlHelper htmlHelper,
            EstadoExpediente EstadoExpediente)
        {
            return htmlHelper.EstadoExpediente((int)EstadoExpediente);
        }

        /************************************************************************/

        public static MvcHtmlString TipoExpediente(
            this HtmlHelper htmlHelper,
            int TipoExpediente)
        {
            string[] tipoColor = { "", "label-warning", "label-primary" };
            string[] tipoNom = { "", "CARGA INICIAL", "EXPEDIENTE REGULAR" };

            var label = new TagBuilder("label");
            label.AddCssClass("label");
            label.AddCssClass(tipoColor[TipoExpediente]);
            label.InnerHtml = tipoNom[TipoExpediente];

            return MvcHtmlString.Create(label.ToString());
        }

        public static MvcHtmlString TipoExpediente(
            this HtmlHelper htmlHelper,
            TipoExpediente TipoExpediente)
        {
            return htmlHelper.TipoExpediente((int)TipoExpediente);
        }

        /************************************************************************/

        public static MvcHtmlString TipoTupa(
            this HtmlHelper htmlHelper,
            int TipoTupa)
        {
            string[] tipoColor = { "", "bg-purple", "label-warning" };
            string[] tipoNom = { "", "TUPA REGULAR", "TUPA ESTÁNDAR" };

            var label = new TagBuilder("label");
            label.AddCssClass("label");
            label.AddCssClass(tipoColor[TipoTupa]);
            label.InnerHtml = tipoNom[TipoTupa];

            return MvcHtmlString.Create(label.ToString());
        }

        public static MvcHtmlString TipoTupa(
            this HtmlHelper htmlHelper,
            TipoTupa TipoTupa)
        {
            return htmlHelper.TipoTupa((int)TipoTupa);
        }
    }
}