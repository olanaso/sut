using System.Web;

namespace Sut.Web.Areas.General.Models
{
    public class ArMotivoAdjuntoModel
    {
        public long ArMotivoAdjuntoId { get; set; }

        public HttpPostedFileBase PostedFile { get; set; }
    }
}