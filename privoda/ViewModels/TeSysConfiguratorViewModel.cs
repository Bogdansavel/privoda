using privoda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.ViewModels
{
    public class TeSysConfiguratorViewModel
    {
        public IEnumerable<PowerAndCurrent> PowerAndCurrentList { get; set; }
        public IEnumerable<CurrentType> CurrentTypes { get; set; }
        public IEnumerable<int> VoltageList { get; set; }
        public TeSys TeSysType1 { get; set; }
        public TeSys TeSysType2 { get; set; }
    }
}
