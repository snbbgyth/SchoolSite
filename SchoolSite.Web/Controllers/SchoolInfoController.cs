using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolSite.Web.Controllers
{
    public class SchoolInfoController : Controller
    {
        // GET: SchoolInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SchoolIntroduction()
        {

            return View();
        }
    }
}