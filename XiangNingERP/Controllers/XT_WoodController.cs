using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class XT_WoodController : Controller
    {
        private static readonly INV_MCService ISer = new INV_MCService();
        private static readonly SYS_XLService XLSer = new SYS_XLService();
        private static readonly INV_MCService MCSer = new INV_MCService();
        public ActionResult Index(SINV_MCModel Smodels)
        {
            return View(Smodels);
        }
        public ActionResult PageList(SINV_MCModel SRmodels, int? PageIndex)
        {
            var PageList = ISer.GetXTWoodPageList(SRmodels, PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
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
            if (ISer.UpdateWood(Models) == true)
            {
                return RedirectToAction("Index", "XT_Wood");
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
        public ActionResult Reckon()
        {
            ReckonModel models = new ReckonModel();
            models.XLDroList = XLSer.GetXLDrolist(models.product_SN_id);
            models.AreaDroList = XLSer.GetAreaDrolist(models.product_area_id);
            models.MCDroList = MCSer.GetXTWoodDrolist(models.wood_id);
            return View(models);
        }
        public decimal? GetProvolume(int product_id)
        {
            var Volume = ISer.GetProvolume(product_id);
            return Volume;
        }
    }
}
