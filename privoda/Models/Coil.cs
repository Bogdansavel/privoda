using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Models
{
    public class Coil
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public int CurrentTypeId { get; set; }
        public CurrentType СurrentType { get; set; }
        public int Voltage { get; set; }
    }
}
