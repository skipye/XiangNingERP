using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelProject;
using DalProject;
using MvcPager.WebControls.Mvc;
using System.Web.Mvc;

namespace ServiceProject
{
    public class CRM_KHService
    {
        private static readonly CRM_KHDal IDal = new CRM_KHDal();
        private static readonly UserDal UDal = new UserDal();
        public PagedList<CRM_KHModel> GetPageList(SCRM_KHModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddHKByUser(int KHId, string ListUserId)
        {
            try { IDal.AddHKByUser(KHId, ListUserId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(CRM_KHModel Models)
        {
            try {
                Models.UserId = UDal.GetCurrentUserName().UserId;
                IDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CRM_KHModel GetDetailById(int Id)
        {
            try { return IDal.GetDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteOne(int Id)
        {
            try { IDal.DeleteOne(Id); return true; }
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
        public string CrrKHByUser(int KHId)
        {
            try { return IDal.CrrKHByUser(KHId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetKHDroList()
         {
            try
            {
                int UId = UDal.GetCurrentUserName().UserId;
                return IDal.GetKHDroList(UId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
