using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Models
{
    public class ModelLineContext : DbContext
    {
        public DbSet<ModelLine> ModelLines { get; set; }

        public ModelLineContext(DbContextOptions<ModelLineContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
