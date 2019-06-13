using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class CW_DeliveryController : Controller
    {
        private static readonly LabelsService SSer = new LabelsService();
        private static readonly CRM_HTService ISer = new CRM_HTService();
        public ActionResult Index(SCRM_HTZModel SModel)
        {
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModel.StartTime))
            {
                SModel.StartTime = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModel.EndTime))
            {
                SModel.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            return View(SModel);
        }
        public ActionResult PageList(SCRM_HTZModel SModel)
        {
            var PageList = ISer.GetCRM_HTDeliveryList(SModel);
            ViewBag.SModel = SModel;
            return View(PageList);
        }
        public ActionResult Checked(int Id)
        {
            string ListId = Id + "$";
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            if (SSer.CheckedDeliveryMore(ListId,true) == true)
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
            if (SSer.CheckedDeliveryMore(ListId, true) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
    }
}
