using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class XT_RMController : Controller
    {
        private static readonly XT_RMService ISer = new XT_RMService();
        private static readonly UserService USer = new UserService();
        private static readonly CRM_HTService CHSer = new CRM_HTService();
        public ActionResult Index(SXT_RMModel Smodels)
        {
            return View(Smodels);
        }
        public ActionResult PageList(SXT_RMModel SRmodels, int? PageIndex)
        {
            var PageList = ISer.GetPageList(SRmodels, PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(Guid? Id)
        {
            XT_RMModel Models = new XT_RMModel();
            if (Id != null && Id != Guid.Empty)
            {
                Models = ISer.GetDetailById(Id.Value);
                Models.ArrUser = Models.UId + "#" + Models.UName + "#" + Models.DepartmentId + "#" + Models.Department;
            }
            Models.UserList = USer.GeXTUserListByJob();
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(XT_RMModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "XT_RM");
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
        //特批生产
        public ActionResult WO(SCRM_HTZModel Smodels)
        {
            DateTime datetime = DateTime.Now;

            return View(Smodels);
        }
        public ActionResult WOTPPageList(SCRM_HTZModel SRmodels)
        {
            var PageList = CHSer.GetWOTPPageList(SRmodels, SRmodels.PageIndex ?? 1, SRmodels.PageSize ?? 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
    }
}
