using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceProject;

namespace XiangNingERP.Controllers
{
    public class SJ_KHController : Controller
    {
        private static readonly SJService SSer = new SJService();
        public ActionResult Index(SWIP_WOModel Smodels)
        {
            if (Smodels.status ==null)
            {
                Smodels.status = 0;
            }
            return View(Smodels);
        }
        public ActionResult YTIndex(SWIP_WOModel Smodels)
        {
            if (Smodels.status == null)
            {
                Smodels.status = 0;
            }
            return View(Smodels);
        }
        public ActionResult PageList(SWIP_WOModel SRmodels, int? PageIndex)
        {
            var PageList = SSer.GetPageList(SRmodels, PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult YTPageList(SWIP_WOModel SRmodels, int? PageIndex)
        {
            var PageList = SSer.GetYTPageList(SRmodels, PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int Id)
        {
            SJ_WorkModel Models = new SJ_WorkModel();
            Models.Id = Id;
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(SJ_WorkModel Models)
        {
            if (SSer.Update(Models) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
    }
}
