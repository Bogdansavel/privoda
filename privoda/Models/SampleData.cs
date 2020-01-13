using System.Linq;

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
