using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class UndOrgRecepcionDocumentos
    {
        [Key]
        public int UnrOrgRecepcionDocumentosId { get; set; }

        //[ForeignKey("Procedimiento")]
        public long ProcedimientoId { get; set; }
        public Procedimiento Procedimiento { get; set; }

        //[ForeignKey("Sede")]
        public long SedeId { get; set; }
        public Sede Sede { get; set; }


        [ForeignKey("UnidadOrganica")]
        public long UnidadOrganicaId { get; set; }
        public UnidadOrganica UnidadOrganica { get; set; }
    }
}
