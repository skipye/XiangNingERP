using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelProject;
using DalProject;
using MvcPager.WebControls.Mvc;

namespace ServiceProject
{
    public class SJService
    {
        private static readonly SJDal SDal=new SJDal();
        public PagedList<WIP_WOModel> GetPageList(SWIP_WOModel SModel, int PageIndex, int PageSize)
        {
            try { return SDal.GetPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
        public PagedList<WIP_WOModel> GetYTPageList(SWIP_WOModel SModel, int PageIndex, int PageSize)
        {
            try { return SDal.GetYTPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
        public bool Update(SJ_WorkModel Models)
        {
            try { SDal.Update(Models); return true; }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
    }
}
