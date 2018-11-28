using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace XiangNingERP.Controllers
{
    [Authorize]
    public class PO_KCController : Controller
    {
        private static readonly INV_FLService ISer = new INV_FLService();
        private static readonly INV_FLZLService IZLSer = new INV_FLZLService();
        private static readonly INVService CKSer = new INVService();
        private static readonly ToExcel ESer = new ToExcel();
        public ActionResult Index(SINV_FLModel Smodels)
        {
            //Smodels.FLZLDroList = IZLSer.GetFLZLDrolist(Smodels.accessory_type_id);
            Smodels.CKDroList = CKSer.GetCKDrolist(Smodels.inventory_id,2);
            return View(Smodels);
        }
        //仓库设置列表
        public ActionResult PageList(SINV_FLModel SRmodels)
        {
            var PageList = ISer.GetFLPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize??10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public JsonResult GetCKAutoLiat(string term)  
        {
            var list = IZLSer.GetFLZLAutolist(term);
            var StrJson = ListToJsonSer.Obj2Json(list);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckOut(SINV_FLModel models)
        {
            return View(models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PostCheckOut(SINV_FLModel Models)
        {
            if (ISer.CheckOut(Models.Id, Models.qty.Value) == true)
            {
                return RedirectToAction("Index", "PO_KC", new { PageIndex = Models.PageIndex, PageSize = Models.PageSize, accessory_type_id = Models.accessory_type_id });
            }
            else { return View(Models); }
        }
        public void ToExcelOut(SINV_FLModel SModel)
        {
            var models = ISer.ToExcel(SModel);
            ESer.CreateExcel(models, "辅料仓库盘点" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}
