using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class SYS_SHController : Controller
    {
        private static readonly SYS_SHService ISer = new SYS_SHService();
        public ActionResult Index(SSYS_SHModel Smodels)
        {
            return View(Smodels);
        }
        public ActionResult PageList(SSYS_SHModel SRmodels, int? PageIndex)
        {
            var PageList = ISer.GetPageList(SRmodels, PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int? Id)
        {
            SYS_SHModel Models = new SYS_SHModel();
            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(SYS_SHModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "SYS_SH");
            }
            else { return View(Models); }
        }
        //删除单个
        public ActionResult DeleteOne(int Id)
        {
            if (ISer.DeleteOne(Id) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        //删除多个
        public ActionResult DeleteMore(string ListId)
        {
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (ISer.DeleteMore(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        
    }
}
