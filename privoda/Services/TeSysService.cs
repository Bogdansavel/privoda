using privoda.Contracts.Services;
using privoda.Models;
using privoda.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Services
{
    public class TeSysService
    {
        private readonly IService<CircuitBreaker> _circuitBreakerService;
        private readonly IService<Contactor> _contactorService;
        private readonly IService<Coil> _coilService;

        public TeSysService(IService<CircuitBreaker> circuitBreakerService, IService<Contactor> contactorService,
            IService<Coil> coilService)
        {
            _circuitBreakerService = circuitBreakerService;
            _contactorService = contactorService;
            _coilService = coilService;
        }

        public async Task<TeSys> GetFirstTypeAsync(double current, double voltage, int currentTypeId)
        {
            var teSys = new TeSys()
            {
                CircuitBreakers = await _circuitBreakerService.GetManyAsync(cb =>
                current >= cb.AFrom &&
                current < cb.ATo &&
                cb.FirstTypeCoordination == true),
                Contactor = await _contactorService.GetAsync(c =>
                current >= c.AFrom &&
                current < c.ATo &&
                c.FirstTypeCoordination == true),
                Coil = await _coilService.GetAsync(c =>
                voltage == c.Voltage &&
                currentTypeId == c.CurrentTypeId, new List<string>() { "СurrentType" })
            };
            return teSys;
        }

        public async Task<TeSys> GetSecondTypeAsync(double current, double voltage, int currentTypeId)
        {
            var teSys = new TeSys()
            {
                CircuitBreakers = await _circuitBreakerService.GetManyAsync(cb =>
                current >= cb.AFrom &&
                current < cb.ATo &&
                cb.SecondTypeCoordination == true),
                Contactor = await _contactorService.GetAsync(c =>
                current >= c.AFrom &&
                current < c.ATo &&
                c.SecondTypeCoordination == true),
                Coil = await _coilService.GetAsync(c =>
                voltage == c.Voltage &&
                currentTypeId == c.CurrentTypeId, new List<string>() { "СurrentType" })
            };
            return teSys;
        }
    }
}
