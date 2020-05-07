using Microsoft.AspNetCore.Mvc;
using privoda.Contracts.Repositories;
using privoda.Contracts.Services;
using privoda.Models;
using privoda.Services;
using privoda.Utils.ReCaptchaV2;
using privoda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Controllers
{
    public class TeSysConfiguratorController : Controller
    {
        private readonly IService<PowerAndCurrent> _powerAndCurrentService;
        private readonly IService<CurrentType> _currentTypeService;
        private readonly IService<CircuitBreaker> _circuitBreakerService;
        private readonly IService<Contactor> _contactorService;
        private readonly IService<Coil> _coilService;
        private readonly ICoilRepository _coilRepository;
        private readonly TeSysService _teSysService;
        private readonly EmailService _emailService;

        public TeSysConfiguratorController(IService<PowerAndCurrent> powerAndCurrentService, IService<CurrentType> currentTypeService, 
            ICoilRepository coilRepository, IService<CircuitBreaker> circuitBreakerService, IService<Contactor> contactorService,
            IService<Coil> coilService, TeSysService teSysService, EmailService emailService)
        {
            _powerAndCurrentService = powerAndCurrentService;
            _currentTypeService = currentTypeService;
            _coilRepository = coilRepository;
            _circuitBreakerService = circuitBreakerService;
            _contactorService = contactorService;
            _coilService = coilService;
            _teSysService = teSysService;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            TeSysConfiguratorViewModel teSysConfiguratorViewModel = new TeSysConfiguratorViewModel()
            {
                PowerAndCurrentList = await _powerAndCurrentService.GetManyAsync(),
                CurrentTypes = await _currentTypeService.GetManyAsync(),
                VoltageList = await _coilRepository.GetAllVoltage()
            };
            return View(teSysConfiguratorViewModel);
        }

        public async Task<IActionResult> Configurate(ConfigurateFirstTypeViewModel configurateFirstTypeViewModel)
        {
            TeSysConfiguratorViewModel teSysConfiguratorViewModel = new TeSysConfiguratorViewModel()
            {
                PowerAndCurrentList = await _powerAndCurrentService.GetManyAsync(),
                CurrentTypes = await _currentTypeService.GetManyAsync(),
                VoltageList = await _coilRepository.GetAllVoltage(),
                TeSysType1 = await _teSysService.GetFirstTypeAsync(configurateFirstTypeViewModel.Current,
                configurateFirstTypeViewModel.Voltage, configurateFirstTypeViewModel.CurrentType),
                TeSysType2 = await _teSysService.GetSecondTypeAsync(configurateFirstTypeViewModel.Current,
                configurateFirstTypeViewModel.Voltage, configurateFirstTypeViewModel.CurrentType)
            };
            return View("Index", teSysConfiguratorViewModel);
        }

        public async Task<IActionResult> SendConfigurationRefs(string email, string phone, 
            string teSys1FullRef, string teSys1CircuitBreakersRef, string teSys1CoilRef, string teSys1ContactorRef, 
            string teSys2FullRef, string teSys2CircuitBreakersRef, string teSys2CoilRef, string teSys2ContactorRef)
        {
            string captchaResponse = HttpContext.Request.Form["g-Recaptcha-Response"];
            ReCaptchaValidationResult result = ReCaptchaValidator.IsValid(captchaResponse);
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            _emailService.SendConfigurationRefs(email,phone,
            teSys1FullRef, teSys1CircuitBreakersRef, teSys1CoilRef, teSys1ContactorRef,
            teSys2FullRef, teSys2CircuitBreakersRef, teSys2CoilRef, teSys2ContactorRef);
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
