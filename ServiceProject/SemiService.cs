using ModelProject;
using MvcPager.WebControls.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalProject;
using System.Data;

namespace ServiceProject
{
    public class SemiService
    {
        private static readonly SemiDal SDal = new SemiDal();
        public PagedList<SemiModel> GetPageList(SSemiModel SModel, int PageIndex, int PageSize)
        {
            try { return SDal.GetPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(SemiModel Models)
        {
            try
            {
                SDal.AddOrUpdate(Models); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EidtStyle(SemiModel Models)
        {
            try
            {
                SDal.EidtStyle(Models); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SemiModel GetDetailById(int Id)
        {
            try { return SDal.GetDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool TransferMore(string ListId, int inv_id)
        {
            try
            {
                SDal.TransferMore(ListId, inv_id); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Checked(SemiModel SModel)
        {
            try
            {
                SDal.Checked(SModel); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteMore(string ListId)
        {
            try
            {
                SDal.DeleteMore(ListId); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteOne(int Id)
        {
            try
            {
                SDal.DeleteOne(Id); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetProNameDrolistBySNAndArea(int? ProSN, int? ProProArea)
        {
            try
            {
                return SDal.GetProNameDrolistBySNAndArea(ProSN, ProProArea); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddWork(string ListId)
        {
            try
            {
                SDal.AddWork(ListId); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //返修
        public bool AddReWork(SemiModel models)
        {
            try
            {
                SDal.AddReWork(models); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToExcel(SSemiModel SModel)
        {
            try
            {
                return SDal.ToExcel(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
