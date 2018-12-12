using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceProject;

namespace XiangNingERP.Controllers
{
    public class SemiController : Controller
    {
        private static readonly SemiService SSer = new SemiService();
        private static readonly SYS_XLService XLSer = new SYS_XLService();
        private static readonly INVService INVSer = new INVService();//仓库操作
        private static readonly INV_MCService MCSer = new INV_MCService();
        private static readonly ToExcel ESer = new ToExcel();
        public ActionResult Index(SSemiModel Smodels)
        {
            SSemiModel Models = new SSemiModel();
            Models = Smodels;
            Models.XLDroList = XLSer.GetXLDrolist(Models.product_SN_id);
            Models.AreaDroList = XLSer.GetAreaDrolist(Models.product_area_id);
            Models.CKDroList = INVSer.GetCKDrolist(Models.inv_id,3);
            return View(Models);
        }
        public ActionResult PageList(SSemiModel SRmodels)
        {
            var PageList = SSer.GetPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize ?? 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult EditStyle(int Id, SSemiModel Models)
        {
            SemiModel NewModels = new SemiModel();
            NewModels.id = Id;
            NewModels.PageIndex = Models.PageIndex;
            NewModels.PageSize = Models.PageSize;
            NewModels.productName = Models.productName; 
            NewModels.product_SN_id = Models.product_SN_id; 
            NewModels.product_area_id = Models.product_area_id; 
            NewModels.inv_id = Models.inv_id;
            NewModels.StartTime = Models.StartTime;
            NewModels.EndTime = Models.EndTime;
            return View(NewModels);
        }
        public ActionResult Add(int? Id)
        {
            SemiModel Models = new SemiModel();
            SSemiModel SModels = new SSemiModel();
            Models.qty = 1;
            if (Id > 0)
            {
                Models = SSer.GetDetailById(Id.Value);
            }
            Models.XLDroList = XLSer.GetXLDrolist(Models.product_SN_id);
            Models.AreaDroList = XLSer.GetAreaDrolist(Models.product_area_id);
            Models.CKDroList = INVSer.GetCKDrolist(Models.inv_id,3);
            Models.MCDroList = MCSer.GetWoodDrolist(Models.wood_id);
            Models.SModel = SModels;
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(SemiModel Models)
        {
            if (SSer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "Semi");
            }
            else { return View(Models); }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditStyle(SemiModel Models)
        {
            if (SSer.EidtStyle(Models) == true)
            {
                return RedirectToAction("Index", "Semi", new { PageIndex = Models.PageIndex, PageSize = Models.PageSize, productName = Models.productName, product_SN_id = Models.product_SN_id, product_area_id = Models.product_area_id, inv_id = Models.inv_id, StartTime = Models.StartTime, EndTime = Models.EndTime });
            }
            else { return View(Models); }
        }
        //移库操作
        public ActionResult Transfer(int Id)
        {
            SemiModel SM = new SemiModel();
            SM.ListId = Id + "$";
            SM.CKDroList = INVSer.GetCKDrolist(null,3);
            return View(SM);
        }
        public ActionResult TransferMore(string ListId)
        {
            SemiModel SM = new SemiModel();
            SM.ListId = ListId;
            SM.CKDroList = INVSer.GetCKDrolist(null,3);
            return View(SM);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TransferMore(string ListId, int inv_id)
        {
            if (SSer.TransferMore(ListId, inv_id) == true)
            {
                return RedirectToAction("Index", "Semi");
            }
            else { return View(); }
        }

        //确认验收产品
        public ActionResult Checked(int Id)
        {
            SemiModel SM = new SemiModel();
            SM.id = Id;
            SM.CKDroList = INVSer.GetCKDrolist(null,3);
            return View(SM);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Checked(SemiModel Models)
        {
            if (SSer.Checked(Models) == true)
            {
                return RedirectToAction("Index", "Semi", new { PageIndex = Models.SModel.PageIndex });
            }
            else { return View(Models); }
        }
        public ActionResult DeleteOne(int Id)
        {

            if (SSer.DeleteOne(Id) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        public ActionResult DeleteMore(string ListId)
        {
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (SSer.DeleteMore(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        public ActionResult GetProNameDrolistBySNAndArea(int? ProSN, int? ProProArea)
        {
            var List = SSer.GetProNameDrolistBySNAndArea(ProSN, ProProArea);
            return Content(List.ToString());
        }
        //安排生产
        public ActionResult WorkOne(int Id)
        {
            string ListId = Id + "$";
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (SSer.AddWork(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Work(string ListId)
        {
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (SSer.AddWork(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        //返修
        public ActionResult Rework(int Id)
        {
            SemiModel SModel = new SemiModel();
            SModel.id = Id;
            return View(SModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Rework(SemiModel models)
        {
            if (string.IsNullOrEmpty(models.Remark) == true)
            {
                return Content("False");
            }
            else
            {
                if (SSer.AddReWork(models) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        public void ToExcelOut(SSemiModel SModel)
        {
            var models = SSer.ToExcel(SModel);
            ESer.CreateExcel(models, "半成品库盘点" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}
