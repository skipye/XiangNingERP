using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    [Authorize]
    public class INV_FLController : Controller
    {
        private static readonly INV_FLService ISer = new INV_FLService();
        private static readonly INV_FLZLService IZLSer = new INV_FLZLService();
        private static readonly INVService CKSer = new INVService();
        public ActionResult Index(SINV_FLModel Smodels)
        {
            Smodels.FLZLDroList = IZLSer.GetFLZLDrolist(Smodels.accessory_type_id);
            Smodels.CKDroList = CKSer.GetCKDrolist(Smodels.inventory_id,2);
            return View(Smodels);
        }
        //仓库设置列表
        public ActionResult PageList(SINV_FLModel SRmodels, int? PageIndex)
        {
            var PageList = ISer.GetPageList(SRmodels, PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        //增加仓库设置
        public ActionResult Add(int? Id)
        {
            INV_FLModel Models = new INV_FLModel();
            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            //Models.unitList = IZLSer.GetUnitsDrolist(Models.units_id);
            Models.FLZLDroList = IZLSer.GetFLZLDrolist(Models.accessory_type_id);
            Models.CKDroList = CKSer.GetCKDrolist(Models.inventory_id,null);
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(INV_FLModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "INV_FL");
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
    }
}
