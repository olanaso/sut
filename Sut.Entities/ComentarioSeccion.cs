using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sut.Entities
{
    public class ComentarioSeccion
    {
        [Key]
        public long ComentarioSeccionId { get; set; }

        public List<Comentario> Comentario { get; set; }
    }
}
