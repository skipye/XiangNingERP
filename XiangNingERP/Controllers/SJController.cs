using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceProject;

namespace XiangNingERP.Controllers
{
    public class SJController : Controller
    {
        private static readonly SJService SSer = new SJService();
        public ActionResult Index()
        {
            return View();
        }
        
    }
}
