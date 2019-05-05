using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using privoda.Models;

namespace privoda.Controllers
{
    public class HomeController : Controller
    {
        ModelLineContext db;

        public HomeController(ModelLineContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.ModelLines.ToList());
        }

        public IActionResult Lines()
        {
            return View(db.ModelLines.ToList());
        }

        [HttpGet]
        public IActionResult Line(int ID)
        {
            ModelLine line = new ModelLine();
            line = db.ModelLines.FirstOrDefault(p => p.Id == ID);
            return View(line);
        }

        public IActionResult Services()
        {
            return View();
        }

    }
}