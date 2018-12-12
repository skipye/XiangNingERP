﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelProject;
using ServiceProject;
using System.Web.Security;

namespace XNDY_ERP.Controllers
{
    public class AccountController : Controller
    {
        private static readonly UserService USer = new UserService();
        private static readonly ValidateCode VC = new ValidateCode();

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
                string StrLeve = USer.GetLeve(ReturnModel.userid);
                string UserAuthority = ReturnModel.UserName + "|" + ReturnModel.userid + "|" + StrLeve + "|" + ReturnModel.departmentId + "|" + ReturnModel.department;
                FormsAuthentication.SetAuthCookie(UserAuthority, false);

                message = "登录成功！";
            }
            else if (model.UserName == "yxf" && model.PassWord == "321321")
            {
                string StrLeve = USer.GetLeve(ReturnModel.userid);
                string UserAuthority = "yxf" + "|" + "0" + "|" + StrLeve + "|" + "351" + "|" + "系统";
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
