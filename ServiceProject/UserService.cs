using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalProject;
using ModelProject;
using System.Web.Mvc;

namespace ServiceProject
{
    public class UserService
    {
        private static readonly UserDal UDal = new UserDal();
        //用户登录
        public LoginModel IsLogin(LoginModel models)
        {
            try { return UDal.IsLogin(models); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserCurrentModel GetCurrentUserName()
        {
            try { return UDal.GetCurrentUserName(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //获取办公室用户
        public List<SelectListItem> GeXTUserListByJob()
        {
            try { return UDal.GeXTUserListByJob(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //获取销售部的用户
        public List<CRMItem> GetSaleUserList()
        {
            try { return UDal.GetSaleUserList(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //获取东阳厂考勤信息
        public List<CRMItem> GetDYUserList()
        {
            try { return UDal.GetDYUserList(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CRMItem> GetUserList()
        {
            try { return UDal.GetUserList(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GeUserListByJob(string Job)
        {
            try { return UDal.GeUserListByJob(Job); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetUserDrolistByJob(string Job)
        {
            try { return UDal.GetUserDrolistByJob(Job); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //根据Uid获取权限列表
        public string GetLeve(int UId)
        {
            try { return UDal.GetLeve(UId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
