using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class CompararPA
    {
        public long entidadid { get; set; }
        public long procedimientoidb { get; set; }
        public string Camp1_Base { get; set; }
        public string Camp2_Base { get; set; }
        public long Camp3_Base { get; set; }
        public long Camp4_Base { get; set; }
        public string Camp5_Base { get; set; }
        public string Camp6_Base { get; set; }
        public string Camp7_Base { get; set; }
        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }
        public string c4 { get; set; }
        public string c5 { get; set; }
        public string c6 { get; set; }
        public string c7 { get; set; }
        public long procedimientoidc { get; set; }
        public string Campo1_Comp { get; set; }
        public string Campo2_Comp { get; set; }
        public long Campo3_Comp { get; set; }
        public long Campo4_Comp { get; set; }
        public string Campo5_Comp { get; set; }
        public string Campo6_Comp { get; set; }
        public string Campo7_Comp { get; set; }

    }
}
