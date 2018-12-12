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
    public class XT_RMService
    {
        private static readonly XT_RMDal IDal = new XT_RMDal();
        public PagedList<XT_RMModel> GetPageList(SXT_RMModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(XT_RMModel Models)
        {
            try {
                IDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public XT_RMModel GetDetailById(Guid Id)
        {
            try { return IDal.GetDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteOne(Guid Id)
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
        public XT_RMURLModel GetRoleByUserId(int UserId)
        {
            try { return IDal.GetRoleByUserId(UserId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
