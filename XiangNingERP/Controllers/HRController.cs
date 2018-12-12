using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class HRController : Controller
    {
        private static readonly HRService ISer = new HRService();
        private static readonly UserService USer = new UserService();
        private static readonly WIP_WOService WWSer = new WIP_WOService();
        private static readonly ToExcel ESer = new ToExcel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PageList(SHRModel SRmodels, int? PageIndex)
        {
            var PageList = ISer.GetPageList(SRmodels, PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int? Id)
        {
            HRModel Models = new HRModel();
            if (Id!=null && Id.Value!=0)
            {
                Models = ISer.GetDetailById(Id.Value);
                Models.ArrUser = Models.user_id + "#" + Models.user_name + "#" + Models.department_id + "#" + Models.department;
            }
            Models.UserList = USer.GeUserListByJob("");
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(HRModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "HR");
            }
            else { return View(Models); }
        }
        //删除单个
        public ActionResult DeleteOne(Guid Id)
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
        public ActionResult Workflow(SWIP_WOXQModel SModel)
        {
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModel.StartTime))
            {
                SModel.StartTime = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModel.EndTime))
            {
                SModel.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-5).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModel.NavName))
            {
                SModel.NavName = "刮磨";
            }
           
            return View(SModel);
        }
        public ActionResult WFPagelist(SWIP_WOXQModel SRmodels)
        {
            var PageList = WWSer.GetHRFlowPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize ?? 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public void ToExcelOut(SWIP_WOXQModel SModel)
        {
            var models = WWSer.ToExcel(SModel);
            
            ESer.CreateExcel(models, "包工工资" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}
