using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XNDY_ERP.Areas.Sale.Controllers
{
    public class CRM_KHController : Controller
    {
        private static readonly CRM_KHService ISer = new CRM_KHService();
        private static readonly UserService USer = new UserService();
        public ActionResult Index(SCRM_KHModel Smodels)
        {
            return View(Smodels);
        }
        public ActionResult PageList(SCRM_KHModel SRmodels, int? PageIndex)
        {
            var PageList = ISer.GetPageList(SRmodels, PageIndex ?? 1, 10);
            ViewBag.SModel = SRmodels;
            return View(PageList);
        }
        public ActionResult Add(int? Id)
        {
            CRM_KHModel Models = new CRM_KHModel();
            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(CRM_KHModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("Index", "CRM_KH");
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
        public ActionResult KHByUser(int Id)
        {
            CRMByUser KHByUser = new CRMByUser();
            KHByUser.KHId = Id;
            KHByUser.ListUserId = ISer.CrrKHByUser(Id);
            KHByUser.SaleUserList = USer.GetSaleUserList();
            return View(KHByUser);
        }
        //添加所属业务员
        public ActionResult AddKHByUser(int KHId, string ListUserId)
        {
            if (string.IsNullOrEmpty(ListUserId) == true)
            {
                return Content("False");
            }
            if (ISer.AddHKByUser(KHId, ListUserId) == true)
            {
                return Content("true");
            }
            else
            {
                return Content("False");
            }
        }
    }
}
