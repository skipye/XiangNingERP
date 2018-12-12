using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    [Authorize]
    public class INV_FLZLController : Controller
    {
        private static readonly INV_FLZLService ISer = new INV_FLZLService();
        public ActionResult Index(SINV_FLZLModel Smodels)
        {
            Smodels.catalogDroList = ISer.GetFLDrolist(Smodels.catalogId);
            return View(Smodels);
        }
        public ActionResult PageList(SINV_FLZLModel SRmodels)
        {
            var PageList = ISer.GetPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize?? 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int? Id)
        {
            INV_FLZLModel Models = new INV_FLZLModel();
            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            Models.catalogDroList = ISer.GetFLDrolist(Models.catalogId);
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(INV_FLZLModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "INV_FLZL");
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
