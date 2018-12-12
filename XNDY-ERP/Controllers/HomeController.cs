using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceProject;
using ModelProject;

namespace XNDY_ERP.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ToExcel ESer = new ToExcel();
        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {
            var List = ESer.GetMouthHRList("03");
            return View(List);
        }
        //导出当月的考勤记录
        public void ToExcelOut()
        {
            var models = ESer.ToExcelOut();
            ESer.CreateExcel(models, "考勤报表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}
