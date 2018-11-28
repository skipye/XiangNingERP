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
    public class INV_FLService
    {
        private static readonly INV_FLDal IDal = new INV_FLDal();
        /// <summary>
        /// 获取仓库设置列表
        /// </summary>
        /// <param name="SModel"></param>
        /// <returns></returns>
        public PagedList<INV_FLModel> GetPageList(SINV_FLModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PagedList<INV_FLModel> GetFLPageList(SINV_FLModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetFLPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(INV_FLModel Models)
        {
            try { IDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public INV_FLModel GetDetailById(int Id)
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
        public bool CheckOut(Guid Id, decimal qty)
        {
            try { IDal.CheckOut(Id, qty); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToExcel(SINV_FLModel SModel)
        {
            try { return IDal.ToExcel(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
