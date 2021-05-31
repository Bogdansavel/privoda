using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Models
{
    public class Contactor
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public double AFrom { get; set; }
        public double ATo { get; set; }
        public bool FirstTypeCoordination { get; set; }
        public bool SecondTypeCoordination { get; set; }
    }
}
