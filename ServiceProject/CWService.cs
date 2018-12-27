using DalProject;
using ModelProject;
using MvcPager.WebControls.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ServiceProject
{
    public class CWService
    {
        private static readonly CWDal CDal = new CWDal();
        public PagedList<CRM_HTProModel> GetSalePagelist(SCRM_HTZModel SModel, out decimal TotalHT, out decimal? TotalYF)
        {
            try { return CDal.GetSalePagelist(SModel, out TotalHT, out TotalYF); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToSaleExcel(SCRM_HTZModel SModel)
        {
            try
            {
                return CDal.ToSaleExcel(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
