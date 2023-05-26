using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Ubigeo
    {
        [Key]
        public long UbigeoId { get; set; }

        public int DepartamentoId { get; set; }
        public int ProvinciaId { get; set; }
        public int DistritoId { get; set; }

        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }

        [NotMapped]
        public string Ruta
        {
            get
            {
                return string.Format("{0} - {1} - {2}", Distrito, Provincia, Departamento);
            }
        }
    }
}
