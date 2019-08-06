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
    public class PO_ACService
    {
        private static readonly PO_ACDal IDal = new PO_ACDal();
        private static readonly UserDal UDal = new UserDal();
        
        public PagedList<PO_ACModel> GetPageList(SPO_ACModel SModel, int PageIndex, int PageSize, out decimal? TotalHT)
        {
            try { return IDal.GetPageList(SModel, PageIndex, PageSize, out TotalHT); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PagedList<PO_ACModel> GetCPageList(SPO_ACModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetCPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(PO_ACModel Models)
        {
            try {
                Models.input_user_id = UDal.GetCurrentUserName().UserId;
                Models.input_user_name = UDal.GetCurrentUserName().UserName;
                IDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckedMore(string ListId)
        {
            try
            {
                int input_user_id = UDal.GetCurrentUserName().UserId;
                string input_user_name = UDal.GetCurrentUserName().UserName;
                IDal.CheckedMore(ListId, input_user_id, input_user_name); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckedCKMore(string ListId)
        {
            try
            {
                int input_user_id = UDal.GetCurrentUserName().UserId;
                string input_user_name = UDal.GetCurrentUserName().UserName;
                IDal.CheckedCKMore(ListId, input_user_id, input_user_name); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PO_ACModel GetDetailById(int Id)
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
        public bool FRMore(string ListId)
        {
            try { IDal.FRMore(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetGYSDrolist(int? Id)
        {
            try { return IDal.GetGYSDrolist(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToExcel(SPO_ACModel SModel)
        {
            try
            {
                return IDal.ToExcel(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
