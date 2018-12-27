using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class CRM_HTController : Controller
    {
        private static readonly CRM_HTProService IPSer = new CRM_HTProService();
        private static readonly CRM_HTService ISer = new CRM_HTService();
        private static readonly CRM_KHService KSer = new CRM_KHService();
        private static readonly UserService USer = new UserService();
        private static readonly ToExcel ESer = new ToExcel();
        private static readonly LabelsService LSer = new LabelsService();
        public ActionResult Index(SCRM_HTZModel Smodels)
        {
            DateTime datetime= DateTime.Now;
            if (string.IsNullOrEmpty(Smodels.StartTime))
            {
                Smodels.StartTime = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(Smodels.EndTime))
            {
                Smodels.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            if (Smodels.FR_flag == null || Smodels.FR_flag<0)
            {
                Smodels.FR_flag = -1;
            }
            return View(Smodels);
        }
        public ActionResult PageList(SCRM_HTZModel SRmodels)
        {
            decimal TotalHT = 0;
            decimal? TotalYF = 0;
            var PageList = ISer.GetPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize ?? 10, out TotalHT, out TotalYF);
            ViewBag.SModel = SRmodels;
            ViewBag.TotalHT = TotalHT;
            ViewBag.TotalYF = TotalYF;
            return View(PageList);
        }
        public ActionResult Add(int? Id)
        {
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            CRM_HTZModel Models = new CRM_HTZModel();
            Models.KHByDroList = KSer.GetKHDroList();
            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            else {
                Models.CRMHTCount = ISer.GetCRMHTCount(dt)+1;

                Models.SN = "ZY00" + DateTime.Now.ToString("yy") + Models.CRMHTCount.ToString("0000");
                Models.HTDate = DateTime.Now.ToString("yyyy-MM-dd");
                Models.signed_user_id = USer.GetCurrentUserName().UserId;
                Models.userName = USer.GetCurrentUserName().UserName;
                
            }
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(CRM_HTZModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "CRM_HT");
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
        //审核
        public ActionResult Checked(int Id)
        {
            if (ISer.Checked(Id) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        //详情
        public ActionResult Detail(int Id)
        {
            var models = ISer.GetDetailById(Id);
            models.HTPro = IPSer.GetPageList(models.id, 1, 100);
            return View(models);
        }
        public void ToExcelOut(SCRM_HTZModel SModel)
        {
            var models = ISer.ToExcel(SModel);
            ESer.CreateExcel(models, "合同" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
        public void ToSaleExcelOut(SCRM_HTZModel SModel)
        {
            var models = ISer.ToSaleExcel(SModel);
            ESer.CreateExcel(models, "销售明细" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
        public ActionResult Print(int Id)
        {
            CRM_HTZModel Models = new CRM_HTZModel();
            Models = ISer.GetDetailById(Id);
            Models.HTPro = IPSer.GetPageList(Id, 1, 20);
            return View(Models);
        }
        public ActionResult Delivery(SLabelsModel SModel)
        {
            return View(SModel);
        }
        public ActionResult DPageList(SLabelsModel SModel)
        {
            var List = LSer.GetPageList(SModel,1,100);
            ViewBag.SModel = SModel;
            return View(List);
        }
    }
}
