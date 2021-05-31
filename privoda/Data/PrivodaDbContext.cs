using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Models
{
    public class PrivodaDbContext : DbContext
    {
        public DbSet<ModelLine> ModelLines { get; set; }
        public DbSet<LineType> LineTypes { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<CircuitBreaker> CircuitBreakers { get; set; }
        public DbSet<Coil> Coils { get; set; }
        public DbSet<Contactor> Contactors { get; set; }
        public DbSet<PowerAndCurrent> PowerAndCurrent { get; set; }
        public DbSet<CurrentType> CurrentTypes { get; set; }

        public PrivodaDbContext(DbContextOptions<PrivodaDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
