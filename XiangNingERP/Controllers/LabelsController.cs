using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class LabelsController : Controller
    {
        private static readonly UserService USer = new UserService();
        private static readonly LabelsService SSer = new LabelsService();
        private static readonly SYS_XLService XLSer = new SYS_XLService();
        private static readonly INVService INVSer = new INVService();//仓库操作
        private static readonly INV_MCService MCSer = new INV_MCService();
        private static readonly SYS_SHService SHSer = new SYS_SHService();//色号操作
        private static readonly ToExcel ESer = new ToExcel();
        public ActionResult Index(SLabelsModel Smodels)
        {
            SLabelsModel Models = new SLabelsModel();
            Models = Smodels;
            Models.XLDroList = XLSer.GetXLDrolist(Models.product_SN_id);
            Models.AreaDroList = XLSer.GetAreaDrolist(Models.product_area_id);
            Models.CKDroList = INVSer.GetCKDrolist(Models.inv_id,4);
            Models.MCDroList = MCSer.GetWoodDrolist(Models.wood_id);
            return View(Models);
        }
        public ActionResult PageList(SLabelsModel SRmodels)
        {
            var PageList = SSer.GetPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PagePSize ?? 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int? Id)
        {
            LabelsModel Models = new LabelsModel();
            Models.qty = 1;
            if (Id > 0)
            {
                Models = SSer.GetDetailById(Id.Value);
            }
            Models.XLDroList = XLSer.GetXLDrolist(Models.product_SN_id);
            Models.AreaDroList = XLSer.GetAreaDrolist(Models.product_area_id);
            Models.CKDroList = INVSer.GetCKDrolist(Models.inv_id,4);
            Models.MCDroList = MCSer.GetWoodDrolist(Models.wood_id);
            Models.SHDroList = SHSer.GetSHDrolist(Models.color_id);
            return View(Models);
        }
        public ActionResult EditStyle(int? Id, int? PageIndex, int? PagePSize)
        {
            LabelsModel Models = new LabelsModel();
            if (Id > 0)
            {
                Models = SSer.GetDetailById(Id.Value);
            }
            Models.PageIndex = PageIndex;
            Models.PagePSize = PagePSize;
            Models.SHDroList = SHSer.GetSHDrolist(Models.color_id);
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(LabelsModel Models)
        {
            if (string.IsNullOrEmpty(Models.color))
            {
                return RedirectToAction("Index", "Labels");
            }
            if (SSer.Add(Models) == true)
            {
                return RedirectToAction("Index", "Labels");
            }
            else { return View(Models); }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditStyle(LabelsModel Models)
        {
            if (SSer.editStyle(Models) == true)
            {
                return RedirectToAction("Index", "Labels", new { PageIndex = Models.PageIndex, PagePSize = Models.PagePSize });
            }
            else { return View(Models); }
        }
        //移库操作
        public ActionResult Transfer(int Id)
        {
            LabelsModel SM = new LabelsModel();
            SM.ListId = Id + "$";
            SM.CKDroList = INVSer.GetCKDrolist(null,4);
            return View(SM);
        }
        public ActionResult TransferMore(string ListId)
        {
            LabelsModel SM = new LabelsModel();
            SM.ListId = ListId;
            SM.CKDroList = INVSer.GetCKDrolist(null,4);
            return View(SM);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TransferMore(string ListId, int inv_id)
        {
            if (SSer.TransferMore(ListId, inv_id) == true)
            {
                return RedirectToAction("Index", "Labels");
            }
            else { return RedirectToAction("Index", "Labels"); }
        }
        //确认验收产品
        public ActionResult Checked(int Id)
        {
            LabelsModel SM = new LabelsModel();
            SM.ListId = Id + "$";
            SM.CKDroList = INVSer.GetCKDrolist(null,4);
            return View(SM);
        }
        public ActionResult CheckMore(string ListId)
        {
            LabelsModel SM = new LabelsModel();
            SM.ListId = ListId;
            SM.CKDroList = INVSer.GetCKDrolist(null,4);
            return View(SM);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CheckMore(string ListId, int inv_id)
        {
            if (SSer.CheckMore(ListId, inv_id) == true)
            {
                return RedirectToAction("Index", "Labels");
            }
            else { return RedirectToAction("Index", "Labels"); }
        }
        public ActionResult GetProNameDrolistBySNAndArea(int? ProSN, int? ProProArea)
        {
            var List = SSer.GetProNameDrolistBySNAndArea(ProSN, ProProArea);
            return Content(List.ToString());
        }
        //安排生产
        public ActionResult Work(int Id)
        {
            WIP_WOXQModel models = new WIP_WOXQModel();
            models.id = Id;
            models.Job = "维修送货";
            models.UserByJob = USer.GeUserListByJob("送货");
            return View(models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Work(WIP_WOXQModel Models)
        {
            if (SSer.AddWork(Models) == true)
            {
                return RedirectToAction("Index", "Labels");
            }
            else { return View(Models); }
        }
        //返修
        public ActionResult Rework(int Id)
        {
            LabelsModel SModel = new LabelsModel();
            SModel.id = Id;
            return View(SModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Rework(LabelsModel models)
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
        public void ToExcelOut(SLabelsModel SModel)
        {
            var models = SSer.ToExcel(SModel);
            ESer.CreateExcel(models, "成品库盘点" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
        public void ToExcelCWOut(SLabelsModel SModel)
        {
            var models = SSer.ToCWExcel(SModel);
            ESer.CreateExcel(models, "财务成品库表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
        public void ToExcelRKOut(SLabelsModel SModel)
        {
            var models = SSer.ToRKExcel(SModel);
            ESer.CreateExcel(models, "当月入库表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
        public void ToDeliveryExcelOut(SLabelsModel SModel)
        {
            var models = SSer.ToDeliveryExcelOut(SModel);
            ESer.CreateExcel(models, "出库明细表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
        public ActionResult DeliveryMore(string ListId,string DeliveryTime)
        {
            if (SSer.DeliveryMore(ListId, DeliveryTime) == true)
            {
                return Content("True");
            }
            else { return Content("False"); }
        }
        public ActionResult Delivery(SLabelsModel SModel)
        {
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModel.StartTime))
            {
                SModel.StartTime = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModel.EndTime))
            {
                SModel.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            SModel.CKDroList = INVSer.GetCKDrolist(SModel.inv_id, 4);
            return View(SModel);
        }
        public ActionResult DeliveryPageList(SLabelsModel SRmodels)
        {
            var PageList = SSer.GetDeliveryList(SRmodels);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult CheckDeliveryView(string ListId)
        {
            ViewBag.ListId = ListId;
            return View();
        }
        public ActionResult CheckDelivery(string ListId, string OrderNum, string DeliverTime)
        {
            if (SSer.CheckDelivery(ListId, OrderNum, DeliverTime) == true)
            {
                return Content("<script>alert('操作成功！');history.back();window.location.reload();</script>");
            }
            else return Content("<script>alert('操作失败！');history.back();window.location.reload();</script>");
        }
        public ActionResult WorkLabels(SLabelsModel SRmodels)
        {
            ViewBag.CRM_Id = SRmodels.Id;
            ViewBag.qty = SRmodels.qty;
            var List = SSer.GetWorkLabelsList(SRmodels);
            return View(List);
        }
        public ActionResult CheckLabels(string ListId, int CRM_Id)
        {
            if (SSer.CheckLabels(ListId, CRM_Id) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        //打印出库单
        public ActionResult PrintDlivery(string ListId)
        {
            var models = SSer.PrintDelivery(ListId);
            return View(models);
        }
    }
}
