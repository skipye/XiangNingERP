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
    public class SYS_XLService
    {
        private static readonly SYS_XLDal IDal = new SYS_XLDal();
        /// <summary>
        /// 获取仓库设置列表
        /// </summary>
        /// <param name="SModel"></param>
        /// <returns></returns>
        public PagedList<SYS_XLModel> GetPageList(SSYS_XLModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(SYS_XLModel Models)
        {
            try { IDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SYS_XLModel GetDetailById(int Id)
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
        public List<SelectListItem> GetXLDrolist(int? pId)
        {
            try { return IDal.GetXLDrolist(pId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetAreaDrolist(int? pId)
        {
            try { return IDal.GetAreaDrolist(pId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
