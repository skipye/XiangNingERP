using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceProject;

namespace XiangNingERP.Controllers
{
    public class WIPController : Controller
    {
        private static readonly WIP_WOService WWSer = new WIP_WOService();
        private static readonly UserService USer = new UserService();
        private static readonly CRM_HTService ISer = new CRM_HTService();
        private static readonly SYS_XLService XLSer = new SYS_XLService();
        private static readonly INV_MCService MCSer = new INV_MCService();
        private static readonly SYS_SHService SHSer = new SYS_SHService();//色号操作
        public ActionResult Index()
        {
            return View();
        }
        //销售生产汇总
        public ActionResult WIP_WorkOrder(SWIP_WOModel Smodels)
        {
            Smodels.IsYT = 0;
            return View(Smodels);
        }
        //预投生产汇总
        public ActionResult Precast(SWIP_WOModel Smodels)
        {
            Smodels.IsYT = 1;
            Smodels.XLDroList = XLSer.GetXLDrolist(null);
            return View(Smodels);
        }
        public ActionResult WorkOrderMore(string ListId)
        {
            WIP_WOXQModel models = new WIP_WOXQModel();
            models.ListId = ListId;
            return View(models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult WorkOrderMore(WIP_WOXQModel Models,string ListId)
        {
            int NavNum = 1;
            if (WWSer.WorkOrderMore(Models, ListId,out NavNum) == true)
            {
                if (NavNum == 1)
                {
                    return RedirectToAction("WIP_WorkOrder", "WIP");
                }
                else { return RedirectToAction("Precast", "WIP"); }
            }
            else { return View(); }
        }
        public ActionResult Add(int Id, string HTSN, int? status,string SaleName,int? PageSize,int? PageIndex)
        {
            WIP_WOXQModel models = new WIP_WOXQModel();
            models.id = Id;
            models.HTSN = HTSN;
            models.status = status;
            models.SaleName = SaleName;
            models.PageSize = PageSize;
            models.PageIndex = PageIndex;
            return View(models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(WIP_WOXQModel Models)
        {
            Models.wo_id = Models.id;
            Models.id = 0;
            int NavNum = 1;
            if (WWSer.AddOrUpdate(Models, out NavNum) == true)
            {
                if (NavNum == 1)
                { return RedirectToAction("WIP_WorkOrder", "WIP", new { PageIndex = Models.PageIndex,PageSize = Models.PageSize, SaleName = Models.SaleName, status = Models.status, HTSN = Models.HTSN }); }
                else { return RedirectToAction("Precast", "WIP", new { PageIndex = Models.PageIndex, PageSize = Models.PageSize, status = Models.status, product_SN_id = Models.product_SN_id }); }
            }
            else { return View(Models); }
        }
        public ActionResult PageList(SWIP_WOModel SRmodels)
        {
            var PageList = WWSer.GetPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize??10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult YTPageList(SWIP_WOModel SRmodels)
        {
            var PageList = WWSer.GetYTPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize??10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        //操作记录详情
        public ActionResult WIP_EvenList(int Id)
        {
            var list = WWSer.WIP_EvenList(Id);
            return View(list);
        }
        //开工单
        public ActionResult WO(SCRM_HTZModel Smodels)
        {
            DateTime datetime = DateTime.Now;
            
            return View(Smodels);
        }
        public ActionResult WOPageList(SCRM_HTZModel SRmodels)
        {
            var PageList = ISer.GetWOPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize ?? 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult SaleWorkOrder(int Id)
        {
            string ListId = Id + "$";
            if (WWSer.AddSaleWorkOrder(ListId) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        public ActionResult SaleWorkOrderMore(string ListId)
        {
            if (WWSer.AddSaleWorkOrder(ListId) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        public ActionResult AddYT()
        {
            YTWorkOrder Models = new YTWorkOrder();

            Models.XLDroList = XLSer.GetXLDrolist(null);
            Models.AreaDroList = XLSer.GetAreaDrolist(null);
            Models.MCDroList = MCSer.GetWoodDrolist(null);
            Models.SHDroList = SHSer.GetSHDrolist(null);
            return View(Models);
        }
        public ActionResult AddYTWorkOrder(YTWorkOrder Models)
        {
            if (WWSer.AddYTWorkOrder(Models) == true)
            {
               return RedirectToAction("WO", "WIP"); 
            }
            else { return View(Models); }
        }
    }
}
