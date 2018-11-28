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
    public class CRM_HTProService
    {
        private static readonly CRM_HTProDal IDal = new CRM_HTProDal();
        private static readonly UserService UDal = new UserService();
        public PagedList<CRM_HTProModel> GetPageList(int HTId, int PageIndex, int PageSize)
        {
            try { return IDal.GetPageList(HTId, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(CRM_HTProModel Models)
        {
            try {IDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CRM_HTProModel GetDetailById(int Id)
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
        public IList<CRMItem> GetProAutolist(string KeyWord, int? ProAreaId)
        {
            try { return IDal.GetProAutolist(KeyWord, ProAreaId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
