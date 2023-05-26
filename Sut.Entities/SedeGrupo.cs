using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class SedeGrupo
    {
        [Key]
        public long SedeGrupoId { get; set; }

        [Required, ForeignKey("Sede")]
        public long SedeId { get; set; }
        public Sede Sede { get; set; }

        public long SedePadreId { get; set; }
        
    }
}
