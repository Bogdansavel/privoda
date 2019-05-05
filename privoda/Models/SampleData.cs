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
                context.ModelLines.AddRange(
                    new ModelLine
                    {
                        Name = "ATV 320",
                        Protection = "IP20",
                        OutputFrequency = "1-599",
                        TransientMoment = "170-200%",
                        Functions = 150,
                        SecurityFeatures = 5,
                        PresetSpeeds = 16
                    },
                    new ModelLine
                    {
                        Name = "ATV 630",
                        Protection = "IP21",
                        OutputFrequency = "0,1-500",
                        TransientMoment = null,
                        Functions = 0,
                        SecurityFeatures = 1,
                        PresetSpeeds = 16
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
