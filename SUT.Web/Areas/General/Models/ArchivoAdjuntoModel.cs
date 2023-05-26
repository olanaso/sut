using System.Web;

namespace Sut.Web.Areas.General.Models
{
    public class ArchivoAdjuntoModel
    {
        public long ArchivoAdjuntoId { get; set; }

        public HttpPostedFileBase PostedFile { get; set; }
    }
}