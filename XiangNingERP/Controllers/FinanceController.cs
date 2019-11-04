using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class FinanceController : Controller
    {
        private static readonly SYS_XLService XLSer = new SYS_XLService();
        private static readonly INV_MCService MCSer = new INV_MCService();
        private static readonly CostService CSer = new CostService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cost(SCostModel Models)
        {
            Models.XLDroList = XLSer.GetXLDrolist(Models.ProductSNId);
            Models.MCDroList = MCSer.GetWoodDrolist(Models.WoodId);
            return View(Models);
        }
        public ActionResult PageList(SCostModel SModels)
        {
            var PageList = CSer.GetPageList(SModels);
            ViewBag.SModel = SModels;
            return View(PageList);
        }
        public ActionResult Add(int? Id, int? PageIndex)
        {
            CostModel Models = new CostModel();
            if (Id != null && Id > 0)
            {
                Models = CSer.GetDetailById(Id.Value);
                //Models.ArrUser = Models.user_id + "#" + Models.user_name + "#" + Models.department_id + "#" + Models.department;
            }
            Models.PageIndex = PageIndex;
            Models.XLDroList = XLSer.GetXLDrolist(Models.ProductSNId);
            Models.MCDroList = MCSer.GetWoodDrolist(Models.WoodId);
            Models.AreaDroList = XLSer.GetAreaDrolist(Models.product_areaId);
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(CostModel Models)
        {
            if (CSer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Cost", "Finance", new { PageIndex = Models.PageIndex });
            }
            else { return View(Models); }
        }
        //删除单个
        public ActionResult DeleteOne(Guid Id)
        {
            string ListId = Id+ "$";
            if (CSer.DeleteMore(ListId) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        
        
    }
}
