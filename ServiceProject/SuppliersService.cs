using DalProject;
using ModelProject;
using MvcPager.WebControls.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceProject
{
    public class SuppliersService
    {
        private static readonly SuppliersDal IDal = new SuppliersDal();
        public PagedList<SuppliersModel> GetPageList(SSuppliersModel SModel, int PageIndex, int PageSize)
        {
            try { return IDal.GetPageList(SModel, PageIndex, PageSize); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(SuppliersModel Models)
        {
            try
            {
                IDal.AddOrUpdate(Models); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SuppliersModel GetDetailById(int Id)
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
    }
}
