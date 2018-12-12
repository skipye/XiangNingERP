using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class CW_BJController : Controller
    {
        private static readonly WIP_WOService WWSer = new WIP_WOService();
        public ActionResult Index(SWIP_WOModel SModel)
        {
            //DateTime datetime = DateTime.Now;
            //if (string.IsNullOrEmpty(SModel.StartTime))
            //{
            //    SModel.StartTime = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            //}
            //if (string.IsNullOrEmpty(SModel.EndTime))
            //{
            //    SModel.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            //}
            if (SModel.PageSize==null || SModel.PageSize<0)
            {
                SModel.PageSize = 80;
            }
            return View(SModel);
        }
        public ActionResult PageList(SWIP_WOModel SRmodels)
        {
            var PageList = WWSer.GetWIP_CWPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize ?? 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Update(int Id,int PageIndex,int PageSize)
        {
            var Models = WWSer.GetCWWIPById(Id);
            Models.PageIndex = PageIndex;
            Models.PageSize = PageSize;
            return View(Models);
        }
        public ActionResult Update(WIP_CWModel models)
        {
            if (WWSer.UpdateWorkOrder(models) == true)
            {
                return RedirectToAction("Index", "CW_BJ", new { PageIndex = models.PageIndex, PageSize = models.PageSize });
            }
            else { return View(models); }
        }
    }
}
