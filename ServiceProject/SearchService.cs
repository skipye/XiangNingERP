using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelProject;
using DalProject;

namespace ServiceProject
{
     
    public class SearchService
    {
        private static readonly SearchDal SDal = new SearchDal();
        public SearchModel GetProductsCount(SearchModel SModel)
        {
            try { return SDal.GetProductsCount(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public AdminModel GetWorkerCount()
        {
            try { return SDal.GetWorkerCount(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
