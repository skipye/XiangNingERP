using Common;
using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class SYS_CPMCController : Controller
    {
        private static readonly SYS_CPMCService ISer = new SYS_CPMCService();
        private static readonly SYS_XLService XLSer = new SYS_XLService();
        public ActionResult Index(SSYS_CPMCModel Smodels)
        {
            SSYS_CPMCModel Models = new SSYS_CPMCModel();
            Models = Smodels;
            Models.XLDroList = XLSer.GetXLDrolist(Models.product_SN_id);
            Models.AreaDroList = XLSer.GetAreaDrolist(Models.product_area_id);
            return View(Models);
        }
        public ActionResult PageList(SSYS_CPMCModel SRmodels)
        {
            var PageList = ISer.GetPageList(SRmodels, SRmodels.PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int? Id, string name, int? product_SN_id, int? product_area_id, int? PageIndex)
        {
            SYS_CPMCModel Models = new SYS_CPMCModel();
            SSYS_CPMCModel SModels = new SSYS_CPMCModel();
            if (!string.IsNullOrEmpty(name))
            {
                SModels.name = name;
            }
            SModels.product_SN_id = product_SN_id;
            SModels.product_area_id = product_area_id;
            SModels.PageIndex = PageIndex;
            
            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            Models.XLDroList = XLSer.GetXLDrolist(Models.product_SN_id);
            Models.AreaDroList = XLSer.GetAreaDrolist(Models.product_area_id);
            Models.SModel = SModels;
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(SYS_CPMCModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "SYS_CPMC", new { name = Models.SModel.name, product_SN_id = Models.SModel.product_SN_id, product_area_id = Models.SModel.product_area_id, PageIndex = Models.SModel.PageIndex });
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
        public ActionResult GetProDrolist(int? PId)
        {
            var models = ISer.GetProDrolist(PId);
            return Content(models.ToString());
        }
    }
}
