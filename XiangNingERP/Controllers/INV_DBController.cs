using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelProject;
using ServiceProject;

namespace XiangNingERP.Controllers
{
    [Authorize]
    public class INV_DBController : Controller
    {
        private static readonly INVDBService ISer = new INVDBService();
        private static readonly INVService INVSer = new INVService();
        private static readonly INV_MCService MCSer = new INV_MCService();
        private static readonly ToExcel ESer = new ToExcel();
        public ActionResult Index(INVDBSModel Smodels)
        {
            Smodels.CKDroList = INVSer.GetCKDrolist(Smodels.inventory_id,null);
            Smodels.MCDroList = MCSer.GetWoodDrolist(Smodels.Wood_typeId);
            return View(Smodels);
        }
        //仓库设置列表
        public ActionResult PageList(INVDBSModel SRmodels)
        {
            var PageList = ISer.GetPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize ?? 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        //增加仓库设置
        public ActionResult Add(int? Id)
        {
            OnlyboardModel Models = new OnlyboardModel();
            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            Models.CKDroList = INVSer.GetCKDrolist(Models.inventory_id,1);
            Models.MCDroList = MCSer.GetWoodDrolist(Models.Wood_typeId);
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(OnlyboardModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "INV_DB");
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
        public void ToExcelOut(INVDBSModel SModel)
        {
            var models = ISer.ToExcel(SModel);
            ESer.CreateExcel(models, "独板库盘点" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}
