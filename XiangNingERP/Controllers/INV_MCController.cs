using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelProject;
using ServiceProject;

namespace XiangNingERP.Controllers
{
    [Authorize]
    public class INV_MCController : Controller
    {
        private static readonly INV_MCService ISer = new INV_MCService();
        public ActionResult Index(SINV_MCModel Smodels)
        {
            return View(Smodels);
        }
        //仓库设置列表
        public ActionResult PageList(SINV_MCModel SRmodels, int? PageIndex)
        {
            var PageList = ISer.GetPageList(SRmodels, PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        //增加仓库设置
        public ActionResult Add(int? Id)
        {
            INV_MCModel Models = new INV_MCModel();
            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(INV_MCModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "INV_MC");
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
