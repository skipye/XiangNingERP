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
    public class INV_MCService
    {
        private static readonly INV_MCDal IDal = new INV_MCDal();
        /// <summary>
        /// 获取仓库设置列表
        /// </summary>
        /// <param name="SModel"></param>
        /// <returns></returns>
        public PagedList<INV_MCModel> GetPageList(SINV_MCModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PagedList<INV_MCModel> GetXTWoodPageList(SINV_MCModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetXTWoodPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(INV_MCModel Models)
        {
            try { IDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateWood(INV_MCModel Models)
        {
            try { IDal.UpdateWood(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public INV_MCModel GetDetailById(int Id)
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
        //木材列表
        public List<SelectListItem> GetWoodDrolist(int? pId)
        {
            try { return IDal.GetWoodDrolist(pId);  }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetXTWoodDrolist(int? pId)
        {
            try { return IDal.GetXTWoodDrolist(pId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public decimal? GetProvolume(int product_id)
        {
            try { return IDal.GetProvolume(product_id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
