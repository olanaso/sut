using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class BaseLegal
    {
        [Key]
        public long BaseLegalId { get; set; }

        public List<BaseLegalNorma> BaseLegalNorma { get; set; }
    }
}
