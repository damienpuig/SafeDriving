using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafeDriving.Controllers
{
    public class FormationController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Download()
        {
            return File(Url.Content("~/Content/Files/TarifdesFormations.pdf"), "application/pdf");
        }

    }
}
