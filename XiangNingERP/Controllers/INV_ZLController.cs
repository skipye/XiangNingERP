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
    public class INV_ZLController : Controller
    {
        private static readonly INV_ZLService ISer = new INV_ZLService();
        private static readonly INV_MCService WSer = new INV_MCService();
        private static readonly INVService CKSer = new INVService();
        private static readonly ToExcel ESer = new ToExcel();
        public ActionResult Index(SINV_ZLModel Smodels)
        {
            Smodels.WoodDroList = WSer.GetWoodDrolist(Smodels.WoodId);
            Smodels.CKDroList = CKSer.GetCKDrolist(Smodels.inventory_id,1);
            return View(Smodels);
        }
        //仓库设置列表
        public ActionResult PageList(SINV_ZLModel SRmodels, int? PageIndex)
        {
            var PageList = ISer.GetPageList(SRmodels, PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        //增加仓库设置
        public ActionResult Add(int? Id)
        {
            INV_ZLModel Models = new INV_ZLModel();
            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            Models.WoodDroList = WSer.GetWoodDrolist(Models.WoodId);
            Models.CKDroList = CKSer.GetCKDrolist(Models.inventory_id,1);
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(INV_ZLModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "INV_ZL");
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
        public void ToExcelOut(SINV_ZLModel SModel)
        {
            var models = ISer.ToExcel(SModel);
            ESer.CreateExcel(models, "材料库" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}
