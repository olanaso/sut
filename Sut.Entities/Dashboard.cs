using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Dashboard
    {
        public Dashboard()
        {
            iOpSp = 1;
            result = "";
            parameter1 = "";
            parameter2 = "";
            parameter3 = "";
            parameter4 = "";
            parameter5 = "";
            parameter6 = "";
            parameter7 = "";
        }

        [NotMapped]
        public string result { get; set; }

        [NotMapped]
        public string parameter1 { get; set; }
        [NotMapped]
        public string parameter2 { get; set; }
        [NotMapped]
        public string parameter3 { get; set; }
        [NotMapped]
        public string parameter4 { get; set; }
        [NotMapped]
        public string parameter5 { get; set; }

        [NotMapped]
        public string parameter6 { get; set; }
        [NotMapped]
        public string parameter7 { get; set; }

        [NotMapped]
        public int iOpSp { get; set; }


    }
}
