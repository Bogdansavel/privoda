using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using privoda.Models;
using privoda.Services;
using privoda.Utils;
using privoda.Utils.ReCaptchaV2;
using privoda.ViewModels;

namespace privoda.Controllers
{
    public class HomeController : Controller
    {
        ModelLineContext db;
        private readonly EmailService _emailService;

        public HomeController(ModelLineContext context, EmailService emailService)
        {
            db = context;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel { ModelLines = db.ModelLines.ToList(), LineTypes = db.LineTypes.ToList(), Descriptions = db.Descriptions.ToList() };
            return View(ivm);
        }

        public IActionResult Lines()
        {
            IndexViewModel ivm = new IndexViewModel { ModelLines = db.ModelLines.ToList(), LineTypes = db.LineTypes.ToList(), Descriptions = db.Descriptions.ToList() };
            return View(ivm);
        }

        public IActionResult Library()
        {
            const string LIBRARY_DIRECTORY_NAME = "Библиотека";
            return View(FileUtil.GetLibraryFileNames(LIBRARY_DIRECTORY_NAME));
        }

        public IActionResult Order(string name, string org, string post, string email, string phone, int lineId)
        {
            string captchaResponse = HttpContext.Request.Form["g-Recaptcha-Response"];
            ReCaptchaValidationResult result = ReCaptchaValidator.IsValid(captchaResponse);
            if (!result.Success)
            {
                return RedirectToAction("Index");
            }
            ModelLine line = db.ModelLines.FirstOrDefault(p => p.Id == lineId);
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
        public IActionResult Line(int ID)
        {
            ModelLine line = db.ModelLines.FirstOrDefault(p => p.Id == ID);
            Description description = db.Descriptions.FirstOrDefault(p => p.LineId == ID);
            ViewBag.Documents = FileUtil.GetLibraryFileNames(line.Name);
            LineViewModel lvm = new LineViewModel { Line = line, Description = description };

            return View(lvm);
        }

        public IActionResult Services()
        {
            return View();
        }

    }
}