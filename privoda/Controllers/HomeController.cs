using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using privoda.Contracts.Services;
using privoda.Models;
using privoda.Services;
using privoda.Utils;
using privoda.Utils.ReCaptchaV2;
using privoda.ViewModels;

namespace privoda.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmailService _emailService;
        private readonly IService<ModelLine> _modelLineService;
        private readonly IService<LineType> _lineTypeService;
        private readonly IService<Description> _descriptionService;

        public HomeController(EmailService emailService, IService<ModelLine> modelLineService, 
            IService<LineType> lineTypeService, IService<Description> descriptionService)
        {
            _modelLineService = modelLineService;
            _lineTypeService = lineTypeService;
            _descriptionService = descriptionService;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel ivm = new IndexViewModel 
            { 
                ModelLines = await _modelLineService.GetManyAsync(), 
                LineTypes = await _lineTypeService.GetManyAsync(), 
                Descriptions = await _descriptionService.GetManyAsync() 
            };
            return View(ivm);
        }

        public async Task<IActionResult> Lines()
        {
            IndexViewModel ivm = new IndexViewModel
            {
                ModelLines = await _modelLineService.GetManyAsync(),
                LineTypes = await _lineTypeService.GetManyAsync(),
                Descriptions = await _descriptionService.GetManyAsync()
            };
            return View(ivm);
        }

        public IActionResult Library()
        {
            const string LIBRARY_DIRECTORY_NAME = "Библиотека";
            return View(FileUtil.GetLibraryFileNames(LIBRARY_DIRECTORY_NAME));
        }

        public async Task<IActionResult> Order(string name, string org, string post, string email, string phone, int lineId)
        {
            string captchaResponse = HttpContext.Request.Form["g-Recaptcha-Response"];
            ReCaptchaValidationResult result = ReCaptchaValidator.IsValid(captchaResponse);
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            ModelLine line = await _modelLineService.GetAsync(p => p.Id == lineId);
            _emailService.SendOrder(name, org, post, email, phone, line);
            return RedirectToAction("SuccessOrder", new { name = line.Name });
        }


        public IActionResult SuccessOrder(string name)
        {
            ViewBag.LineName = name;
            return View();
        }

        public IActionResult Select(string normal, string hard, string workSequences, string inputOrganization, string power, string current, string speed, string cos, string problem, string email)
        {
            string captchaResponse = HttpContext.Request.Form["g-Recaptcha-Response"];
            ReCaptchaValidationResult result = ReCaptchaValidator.IsValid(captchaResponse);
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            _emailService.SendSelect(normal, hard, workSequences, inputOrganization, power, current, speed, cos, problem, email);
            return RedirectToAction("SuccessSelect");
        }

        public IActionResult SuccessSelect()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Line(int ID)
        {
            ModelLine line = await _modelLineService.GetAsync(p => p.Id == ID);
            Description description = await _descriptionService.GetAsync(p => p.LineId == ID);
            ViewBag.Documents = FileUtil.GetLibraryFileNames(line.Name);
            LineViewModel lvm = new LineViewModel
            {
                Line = line,
                Description = description 
            };

            return View(lvm);
        }

        public IActionResult Services()
        {
            return View();
        }

    }
}