using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class PO_ACController : Controller
    {
        private static readonly PO_ACService ISer = new PO_ACService();
        private static readonly UserService USer = new UserService();
        private static readonly INV_FLZLService FLSer = new INV_FLZLService();
        private static readonly ToExcel ESer = new ToExcel();
        public ActionResult Index(SPO_ACModel Smodels)
        {
            DateTime datetime = DateTime.Now;
            if (Smodels.CheckState == null || Smodels.CheckState < 0)
            {
                Smodels.CheckState = -1;
            }
            if (string.IsNullOrEmpty(Smodels.StartTime))
            {
                Smodels.StartTime = datetime.AddMonths(-1).AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(Smodels.EndTime))
            {
                Smodels.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            Smodels.GYDroList = ISer.GetGYSDrolist(Smodels.supplier_id);
            return View(Smodels);
        }
        public ActionResult PageList(SPO_ACModel SRmodels)
        {
            decimal? TotalHT = 0;
            var PageList = ISer.GetPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize?? 10, out TotalHT);
            ViewBag.TotalHT = TotalHT;
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int? Id, string SN, DateTime? StartTime, DateTime? EndTime, int? PageIndex)
        {
            PO_ACModel Models = new PO_ACModel();
            SPO_ACModel SModels = new SPO_ACModel();
            if (!string.IsNullOrEmpty(SN))
            {
                SModels.SN = SN;
            }
            SModels.PageIndex = PageIndex;

            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            Models.UserDroList = USer.GeUserListByJob("");
            Models.FLDroList = FLSer.GetFLZLDrolist(SModels.accessory_id);
            Models.GYSDroList = ISer.GetGYSDrolist(null);
            Models.SModel = SModels;
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(PO_ACModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "PO_AC", new { SN = Models.SModel.SN, StartTime = Models.SModel.StartTime, EndTime = Models.SModel.EndTime, PageIndex = Models.SModel.PageIndex, PageSize = Models.SModel.PageSize });
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
        public ActionResult FRMore(string ListId)
        {
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (ISer.FRMore(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        public void ToExcelOut(SPO_ACModel SModel)
        {
            var models = ISer.ToExcel(SModel);
            ESer.CreateExcel(models, "采购汇总" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }

    }
}
