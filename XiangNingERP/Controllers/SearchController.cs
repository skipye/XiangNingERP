using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelProject;
using ServiceProject;

namespace XiangNingERP.Controllers
{
    public class SearchController : Controller
    {
        private static readonly SYS_XLService XLSer = new SYS_XLService();
        private static readonly INV_MCService MCSer = new INV_MCService();
        private static readonly SearchService SSer = new SearchService();
        public ActionResult Home()
        {
            AdminModel Models = new AdminModel();
            Models = SSer.GetWorkerCount();
            return View(Models);
        }


        public ActionResult Index(SearchModel Models)
        {
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(Models.StartTime))
            {
                Models.StartTime = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(Models.EndTime))
            {
                Models.EndTime = datetime.AddDays(1).ToString("yyyy-MM-dd");
            }
            Models.XLDroList = XLSer.GetXLDrolist(Models.product_SN_id);
            Models.AreaDroList = XLSer.GetAreaDrolist(Models.product_area_id);
            Models.MCDroList = MCSer.GetWoodDrolist(Models.wood_id);
            return View(Models);
        }
        public ActionResult Search(SearchModel Models)
        {
            var Date = SSer.GetProductsCount(Models);
            return View(Date);
        }
        public ActionResult Sale(SearchModel Models)
        {
            return View(Models);
        }
        public ActionResult SaleList(SearchModel Models)
        {
            return View(Models);
        }
    }
}
