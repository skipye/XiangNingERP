using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class SJ_HTController : Controller
    {
        private static readonly CRM_HTService ISer = new CRM_HTService();
        private static readonly CRM_KHService KSer = new CRM_KHService();
        private static readonly UserService USer = new UserService();
        public ActionResult Index(SCRM_HTZModel Smodels)
        {
            Smodels.CheckState = 1;
            return View(Smodels);
        }
        public ActionResult PageList(SCRM_HTZModel SRmodels, int? PageIndex)
        {
            decimal TotalHT = 0;
            decimal? TotalYF = 0;
            var PageList = ISer.GetPageList(SRmodels, PageIndex ?? 1, 10, out TotalHT, out TotalYF);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [HttpPost]
        public ActionResult Add(int Id, string Remark)
        {
            if (ISer.AddHTRemark(Id, Remark) == true)
            {
                return RedirectToAction("Index", "SJ_HT");
            }
            else { return View(); }
        }
    }
}
