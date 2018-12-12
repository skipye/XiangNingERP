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
    public class CRM_HTService
    {
        private static readonly CRM_HTDal IDal = new CRM_HTDal();
        private static readonly UserService UDal = new UserService();
        public PagedList<CRM_HTZModel> GetPageList(SCRM_HTZModel SModel, int PageIndex, int PageSize,out decimal TotalHT,out decimal? TotalYF)
        {
            try { return IDal.GetPageList(SModel, PageIndex, PageSize, out TotalHT, out TotalYF); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PagedList<CRM_HTProModel> GetWOPageList(SCRM_HTZModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetWOPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PagedList<CRM_HTProModel> GetWOTPPageList(SCRM_HTZModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetWOTPPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(CRM_HTZModel Models)
        {
            try {
                Models.signed_department_id = UDal.GetCurrentUserName().DepartmentId;
                Models.department = UDal.GetCurrentUserName().Department;
                IDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CRM_HTZModel GetDetailById(int Id)
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
        public bool Checked(int Id)
        {
            try { IDal.Checked(Id); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddHTRemark(int Id, string Remark)
        {
            try { IDal.AddHTRemark(Id, Remark); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToExcel(SCRM_HTZModel SModel)
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
        public DataTable ToSaleExcel(SCRM_HTZModel SModel)
        {
            try
            {
                return IDal.ToSaleExcel(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public decimal GetFR_contract(int contract_id)
        {
            try
            {
                return IDal.GetFR_contract(contract_id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int GetCRMHTCount(DateTime CreateTime)
        {
            try
            {
                return IDal.GetCRMHTCount(CreateTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
