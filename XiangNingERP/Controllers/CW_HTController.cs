using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class CW_HTController : Controller
    {
        private static readonly CWService CWSer = new CWService();
        private static readonly ToExcel ESer = new ToExcel();
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
            if (SModel.FR_flag == null || SModel.FR_flag < 0)
            {
                SModel.FR_flag = -1;
            }
            return View(SModel);
        }
        public ActionResult PageList(SCRM_HTZModel SRmodels)
        {
            decimal TotalHT = 0;
            decimal? TotalYF = 0;
            SRmodels.PageIndex=SRmodels.PageIndex ?? 1;
            SRmodels.PageSize = SRmodels.PageSize ?? 10;
            var PageList = CWSer.GetSalePagelist(SRmodels, out TotalHT, out TotalYF);
            ViewBag.TotalHT = TotalHT;
            ViewBag.TotalYF = TotalYF;
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public void ToSaleExcelOut(SCRM_HTZModel SModel)
        {
            var models = CWSer.ToSaleExcel(SModel);
            ESer.CreateExcel(models, "销售明细" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}
