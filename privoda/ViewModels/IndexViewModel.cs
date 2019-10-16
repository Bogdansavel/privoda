using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using privoda.Models;

namespace privoda.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ModelLine> ModelLines { get; set; }
        public IEnumerable<LineType> LineTypes { get; set; }
        public IEnumerable<Description> Descriptions { get; set; }
    }
}
