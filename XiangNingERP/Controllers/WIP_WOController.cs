using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class WIP_WOController : Controller
    {
        private static readonly WIP_WOService WWSer = new WIP_WOService();
        private static readonly UserService USer = new UserService();
        private static readonly ToExcel ESer = new ToExcel();
        public ActionResult Index(SWIP_WOXQModel Smodels)
        {
            if (string.IsNullOrEmpty(Smodels.NavName))
            {
                Smodels.NavName = "开料";
            }
            //if (Smodels.status == null)
            //{
            //    Smodels.status = 0;
            //}
            return View(Smodels);
        }
        public ActionResult PageList(SWIP_WOXQModel SRmodels)
        {
            var PageList = WWSer.GetFlowPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize??10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int? Id,string Job,int? PageIndex,int? PageSize)
        {
            WIP_WOXQModel models = new WIP_WOXQModel();
            models.Job = Job;
            models.PageSize = PageSize;
            models.PageIndex = PageIndex;
            models.UserByJob = USer.GeUserListByJob(Job);
            return View(models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(WIP_WOXQModel Models)
        {
            int NavNum = 1;
            if (WWSer.AddOrUpdate(Models, out NavNum) == true)
            {
                return RedirectToAction("Index", "WIP_WO", new { NavName = Models.Job });
            }
            else { return View(Models); }
        }
        public ActionResult UserDroListByJob(string Job)
        {
            var List = USer.GetUserDrolistByJob(Job);
            return Content(List.ToString());
        }
        public ActionResult Checked(int Id, int status, int? PageIndex, int? PageSize)
        {
            if (WWSer.Checked(Id, status) == true)
            {
                return Content("True"); 
                //return RedirectToAction("Index", "WIP_WO", new { PageIndex = PageIndex, PageSize = PageSize });
            }
            else { return Content("False"); }
        }
        public void ToWorkExcelOut()
        {
            var models = WWSer.ToWorkExcel();
            ESer.CreateExcel(models, "生产汇总" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}
