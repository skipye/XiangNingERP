using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelProject;
using ServiceProject;

namespace XiangNingERP.Controllers
{
    [Authorize]
    public class INVController : Controller
    {
        private static readonly INVService ISer = new INVService();
        public ActionResult Index()
        {
            return View();
        }
        //仓库设置
        public ActionResult INV_inventories(INVCKSerModel SRmodels)
        {
            return View(SRmodels);
        }
        //仓库设置列表
        public ActionResult INVSZPageList(INVCKSerModel SRmodels,int? PageIndex)
        {
            var PageList = ISer.GetINVCKPageList(SRmodels, PageIndex??1,10);
            ViewBag.PageList = SRmodels;
            return View(PageList);
        }
        //增加仓库设置
        public ActionResult AddINVSZ(int? Id)
        {
            INVCKModel Models = new INVCKModel();
            if (Id > 0)
            {
                Models = ISer.GetDetailById(Id.Value);
            }
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddINVSZ(INVCKModel Models)
        {
            if (ISer.AddOrUpdate(Models) == true)
            {
                return RedirectToAction("INV_inventories", "INV");
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
    }
}
