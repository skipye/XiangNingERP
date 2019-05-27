using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelProject;
using DalProject;
using MvcPager.WebControls.Mvc;
using System.Data;

namespace ServiceProject
{
    public class WIP_WOService
    {
        private static readonly WIP_WODal WWDal = new WIP_WODal();
        public PagedList<WIP_WOModel> GetPageList(SWIP_WOModel SModel, int PageIndex, int PageSize)
        {
            try { return WWDal.GetPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PagedList<WIP_WOModel> GetYTPageList(SWIP_WOModel SModel, int PageIndex, int PageSize)
        {
            try { return WWDal.GetYTPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PagedList<WIP_WOXQModel> GetFlowPageList(SWIP_WOXQModel SModel, int PageIndex, int PageSize)
        {
            try { return WWDal.GetFlowPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PagedList<WIP_WOXQModel> GetHRFlowPageList(SWIP_WOXQModel SModel, int PageIndex, int PageSize)
        {
            try { return WWDal.GetHRFlowPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(WIP_WOXQModel Models,out int NavNum)
        {
            try { WWDal.AddOrUpdate(Models,out NavNum); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool WorkOrderMore(WIP_WOXQModel Models, string ListId,out int NavNum)
        {
            try { WWDal.WorkOrderMore(Models, ListId,out NavNum); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public WIP_WOXQModel GetDetailById(int Id)
        {
            try { return WWDal.GetDetailById(Id);}
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<WIP_Even> WIP_EvenList(int WoId)
        {
            try { return WWDal.WIP_EvenList(WoId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Checked(int Id, int status)
        {
            try { WWDal.Checked(Id, status); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //销售产品开工单
        public bool AddSaleWorkOrder(string ListId)
        {
            try { WWDal.AddSaleWorkOrder(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //预投产品添加
        public bool AddYTWorkOrder(YTWorkOrder Models)
        {
            try { WWDal.AddYTWorkOrder(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToExcel(SWIP_WOXQModel SModel)
        {
            try { return WWDal.ToExcel(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToWorkExcel()
        {
            try { return WWDal.ToWorkExcel(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PagedList<WIP_CWModel> GetWIP_CWPageList(SWIP_WOModel SModel, int PageIndex, int PageSize)
        {
            try { return WWDal.GetWIP_CWPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public WIP_CWModel GetCWWIPById(int Id)
        {
            try { return WWDal.GetCWWIPById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateWorkOrder(WIP_CWModel models)
        {
            try { WWDal.UpdateWorkOrder(models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
