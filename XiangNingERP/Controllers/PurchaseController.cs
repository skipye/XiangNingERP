using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class PurchaseController : Controller
    {
        private static readonly PO_ACService ISer = new PO_ACService();
        private static readonly UserService USer = new UserService();
        private static readonly INV_FLZLService FLSer = new INV_FLZLService();
        public ActionResult Index(SPO_ACModel Smodels)
        {
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(Smodels.StartTime))
            {
                Smodels.StartTime = datetime.AddMonths(-1).AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(Smodels.EndTime))
            {
                Smodels.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            return View(Smodels);
        }
        public ActionResult PageList(SPO_ACModel SRmodels)
        {
            var PageList = ISer.GetCPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize ?? 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Checked(int Id)
        {
            string ListId = Id + "$";
            if (ISer.CheckedMore(ListId) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        public ActionResult CheckedMore(string ListId)
        {
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (ISer.CheckedMore(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
    }
}
