using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Contracts.Repositories
{
    public interface ICoilRepository
    {
        Task<IEnumerable<int>> GetAllVoltage();
    }
}
