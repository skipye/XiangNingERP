using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class CRM_SaleServiceController : Controller
    {
        private static readonly SaleSerService SSer = new SaleSerService();
        private static readonly UserService USer = new UserService();
        public ActionResult Index(SSaleSerModel SModel)
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
            return View(SModel);
        }
        public ActionResult PageList(SSaleSerModel SModels)
        {
            SModels.PageSize=SModels.PageSize >0? SModels.PageSize : 10;
            var PageList = SSer.GetPageList(SModels);
            ViewBag.SModel = SModels;
            return View(PageList);
        }
        public ActionResult Add(int? Id)
        {
            string strtime = DateTime.Now.Year.ToString() + "-01-01";
            DateTime TimeNow = Convert.ToDateTime(strtime);
            SaleSerModel Models = new SaleSerModel();
            if (Id > 0)
            {
                Models = SSer.GetDetailById(Id.Value);
            }
            else
            {
                Models.SN = "SH00" + DateTime.Now.ToString("yy") + SSer.GetSaleSerCount(TimeNow).ToString("0000");
                Models.userName = USer.GetCurrentUserName().UserName;

            }
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(SaleSerModel Models)
        {
            if (SSer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "CRM_SaleService");
            }
            else { return View(Models); }
        }
        public ActionResult AddPro(int? Id)
        {
            SaleSerProModel Models = new SaleSerProModel();
            Models.SaleServiceId = Id.Value;
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPro(SaleSerProModel Models)
        {
            if (SSer.AddSaleSerPro(Models) == true)
            {
                return RedirectToAction("Index", "CRM_SaleService");
            }
            else { return View(Models); }
        }
        public ActionResult Checked(int Id)
        {
            if (SSer.Checked(Id) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        public ActionResult Delete(int Id)
        {
            string ListId = Id + "$";
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            if (SSer.DeleteMore(ListId) == true)
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
            if (SSer.DeleteMore(ListId) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        public ActionResult DeletePro(int Id)
        {
            string ListId = Id + "$";
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            if (SSer.DeleteProMore(ListId) == true)
            {
                return Content("True");
            }
            else return Content("False");
        }
        public ActionResult ProPageList(int Id)
        {
            var PageList = SSer.GetProPageList(Id);
            return View(PageList);
        }
        public ActionResult Print(int Id)
        {
            SaleSerModel Models = new SaleSerModel();
            Models = SSer.GetDetailById(Id);
            Models.SaleSerPro = SSer.GetProPageList(Id);
            return View(Models);
        }
    }
}
