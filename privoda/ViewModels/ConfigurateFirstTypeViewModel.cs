using privoda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.ViewModels
{
    public class ConfigurateFirstTypeViewModel
    {
        public double Power { get; set; }
        public double Current { get; set; }
        public int CurrentType { get; set; }
        public double Voltage { get; set; }
    }
}
