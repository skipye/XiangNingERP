using ModelProject;
using ServiceProject;
using Common;
using System.Web.Mvc;
using System;

namespace XiangNingERP.Controllers
{
    public class CRM_HTProController : Controller
    {
        private static readonly CRM_HTProService ISer = new CRM_HTProService();
        private static readonly UserService USer = new UserService();
        private static readonly SYS_XLService SXL = new SYS_XLService();
        private static readonly INV_MCService CPMSer = new INV_MCService();
        private static readonly SYS_SHService SHSer = new SYS_SHService();
        public ActionResult Index(int HTId)
        {
            return View();
        }
        public ActionResult PageList(int Id, int? PageIndex)
        {
            var PageList = ISer.GetPageList(Id, PageIndex ?? 1, 100);
            return View(PageList);
        }
        public ActionResult Add(int? Id)
        {
            CRM_HTProModel Models = new CRM_HTProModel();
            Models.header_id = Id.Value;
            Models.ProXLDroList = SXL.GetXLDrolist(Models.productXL_id);
            Models.AreaDroList = SXL.GetAreaDrolist(Models.productArea_id);
            Models.WoodDroList = CPMSer.GetWoodDrolist(Models.wood_type_id);
            Models.colorDroList = SHSer.GetSHDrolist(Models.color_id);
            return View(Models);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(CRM_HTProModel Models)
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
        public JsonResult GetProAutoLiat(string term, string ProAreaId)
        {
             int TypeId = 0;
            if (!string.IsNullOrEmpty(ProAreaId))
            { TypeId = Convert.ToInt32(ProAreaId); }
            var list = ISer.GetProAutolist(term, TypeId);
            var StrJson = ListToJsonSer.Obj2Json(list);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
