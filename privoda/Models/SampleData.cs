using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Models
{
    public class SampleData
    {
        public static void Initialize(ModelLineContext context)
        {
            if (!context.ModelLines.Any())
            {
                context.SaveChanges();
            }
        }
    }
}
