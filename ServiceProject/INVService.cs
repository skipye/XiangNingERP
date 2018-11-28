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
    public class INVService
    {
        private static readonly INVDal IDal = new INVDal();
        /// <summary>
        /// 获取仓库设置列表
        /// </summary>
        /// <param name="SModel"></param>
        /// <returns></returns>
        public PagedList<INVCKModel> GetINVCKPageList(INVCKSerModel SModel,int PageIndex,int PageSize)
        {
            try { return IDal.GetINVCKPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(INVCKModel Models)
        {
            try { IDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public INVCKModel GetDetailById(int Id)
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
        //获取仓库列表
        public List<SelectListItem> GetCKDrolist(int? pId,int? CKTypeI)
        {
            try { return IDal.GetCKDrolist(pId, CKTypeI); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IList<CRMItem> GetCKAutolist(string KeyWord)
        {
            try { return IDal.GetCKAutolist(KeyWord); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
