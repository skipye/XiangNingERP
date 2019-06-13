using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelProject;
using DalProject;
using MvcPager.WebControls.Mvc;
using System.Web.Mvc;
using System.Data;

namespace ServiceProject
{
    public class SaleSerService
    {
        private static readonly SaleSerDal IDal = new SaleSerDal();
        private static readonly UserService UDal = new UserService();
        public PagedList<SaleSerModel> GetPageList(SSaleSerModel SModel)
        {
            try { return IDal.GetPageList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(SaleSerModel Models)
        {
            try
            {
                Models.userName = UDal.GetCurrentUserName().UserName;
                IDal.AddOrUpdate(Models); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SaleSerModel GetDetailById(int Id)
        {
            try { return IDal.GetDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteMore(string ListId)
        {
            try { IDal.DeleteMore(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteProMore(string ListId)
        {
            try { IDal.DeleteProMore(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Checked(int Id)
        {
            try { IDal.Checked(Id); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        public int GetSaleSerCount(DateTime CreateTime)
        {
            try
            {
                return IDal.GetSaleSerCount(CreateTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddSaleSerPro(SaleSerProModel Models)
        {
            try { IDal.AddSaleSerPro(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SaleSerProModel> GetProPageList(int SSId)
        {
            try { return IDal.GetProPageList(SSId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
