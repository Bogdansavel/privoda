using Microsoft.EntityFrameworkCore;
using privoda.Contracts.Repositories;
using privoda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Data.Repositories
{
    public class CoilRepository : ICoilRepository
    {
        private readonly PrivodaDbContext _dbContext;

        public CoilRepository(PrivodaDbContext dbContext, string[] includes = null)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<int>> GetAllVoltage()
        {
            return await _dbContext.Coils.Select(coils => coils.Voltage).Distinct().ToListAsync();
        }
    }
}
