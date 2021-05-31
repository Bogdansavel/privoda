using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Models
{
    public class TeSys
    {
        public IEnumerable<CircuitBreaker> CircuitBreakers { get; set; }
        public Contactor Contactor { get; set; }
        public Coil Coil { get; set; }

        public string CircuitBreakersInLine
        {
            get
            {
                return string.Join("/", CircuitBreakers.Select(cb => cb.Reference).ToArray());
            }
        }

        public string FullReference
        {
            get
            {
                if (CircuitBreakers.Count() == 1)
                {
                    return CircuitBreakers.First().Reference + Contactor.Reference + Coil?.Reference;
                }

                return "|" + CircuitBreakersInLine + "|" + Contactor.Reference + Coil?.Reference;
            }
        }
    }
}
