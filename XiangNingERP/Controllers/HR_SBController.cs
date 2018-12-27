using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XiangNingERP.Controllers
{
    public class HR_SBController : Controller
    {
        private static readonly HRService ISer = new HRService();
        private static readonly UserService USer = new UserService();
        public ActionResult Index(SHRModel Smodels)
        {
            if (Smodels.PageIndex == null)
            { Smodels.PageIndex = 1; }
            if (Smodels.status == null)
            {
                Smodels.status = 1;
            }
            return View(Smodels);
        }
        public ActionResult PageList(SHRModel SRmodels)
        {
            var PageList = ISer.GetPageList(SRmodels, SRmodels.PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int? Id, int? PageIndex)
        {
            HRModel Models = new HRModel();
            if (Id != null && Id >0)
            {
                Models = ISer.GetDetailById(Id.Value);
                //Models.ArrUser = Models.user_id + "#" + Models.user_name + "#" + Models.department_id + "#" + Models.department;
            }
            Models.PageIndex = PageIndex;
            //Models.UserList = USer.GeUserListByJob("");
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(HRModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "HR_SB", new { PageIndex = Models.PageIndex });
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
    }
}
