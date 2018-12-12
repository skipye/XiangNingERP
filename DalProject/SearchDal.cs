using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using ModelProject;

namespace DalProject
{
    public class SearchDal
    {
        public SearchModel GetProductsCount(SearchModel SModel)
        {
            SearchModel Models = new SearchModel();
            Models = SModel;
            DateTime StartTime=Convert.ToDateTime(SModel.StartTime);
            DateTime EndTime=Convert.ToDateTime(SModel.EndTime);
            using (var db = new XNERPEntities())
            {
                
                var SaleTable = (from p in db.CRM_contract_detail.Where(k => k.delete_flag == false)
                              where p.created_time > StartTime
                              where p.created_time < EndTime
                              where SModel.product_SN_id != null && SModel.product_SN_id > 0 ? SModel.product_SN_id == p.SYS_product.product_SN_id : true
                              where SModel.product_area_id != null && SModel.product_area_id > 0 ? SModel.product_area_id == p.SYS_product.product_area_id : true
                              where SModel.wood_id != null && SModel.wood_id > 0 ? SModel.wood_id == p.wood_type_id : true
                              where !string.IsNullOrEmpty(SModel.Pname) ? p.SYS_product.name.Contains(SModel.Pname) : true
                              select p).ToList();
                Models.SaleCount = SaleTable.Sum(k => k.qty);

                var SemiTable = (from p in db.INV_semi.Where(k => k.delete_flag == false)
                              where p.created_time > StartTime
                              where p.created_time < EndTime
                              where SModel.product_SN_id != null && SModel.product_SN_id > 0 ? SModel.product_SN_id == p.SYS_product.product_SN_id : true
                              where SModel.product_area_id != null && SModel.product_area_id > 0 ? SModel.product_area_id == p.SYS_product.product_area_id : true
                              where SModel.wood_id != null && SModel.wood_id > 0 ? SModel.wood_id == p.wood_id : true
                              where !string.IsNullOrEmpty(SModel.Pname) ? p.SYS_product.name.Contains(SModel.Pname) : true
                              select p).ToList();
                Models.SemiCount = SemiTable.Count;

                var LabelsTable = (from p in db.INV_labels.Where(k => k.delete_flag == false)
                                 where p.created_time > StartTime
                                 where p.created_time < EndTime
                                 where SModel.product_SN_id != null && SModel.product_SN_id > 0 ? SModel.product_SN_id == p.SYS_product.product_SN_id : true
                                 where SModel.product_area_id != null && SModel.product_area_id > 0 ? SModel.product_area_id == p.SYS_product.product_area_id : true
                                 where SModel.wood_id != null && SModel.wood_id > 0 ? SModel.wood_id == p.wood_id : true
                                 where !string.IsNullOrEmpty(SModel.Pname) ? p.SYS_product.name.Contains(SModel.Pname) : true
                                 select p).ToList();
                Models.LabelsCount = LabelsTable.Count;

                return Models;
            }
        }

        public AdminModel GetWorkerCount()
        {
            DateTime datetime = DateTime.Now;
            DateTime StartTime = Convert.ToDateTime(new DateTime(datetime.Year, 1, 1).ToString("yyyy-MM-dd"));
            DateTime EndTime = Convert.ToDateTime(datetime.AddDays(1).ToString("yyyy-MM-dd"));
            DateTime MonthTime = Convert.ToDateTime(datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd"));
            
            AdminModel Models = new AdminModel();
            using (var db = new XNHREntities())
            {
                var list = db.ehr_employee.Where(k => k.status == 1).Count();
                Models.TotalWorker = list;
                DateTime TimeNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                var Today = db.ehr_post.Where(k => k.acttime >= TimeNow).GroupBy(k => k.name).Count();
                Models.TodayWorker = Today;
            }
            using (var db = new XNERPEntities())
            {
                var WorkOrder = db.WIP_workorder.Where(k => k.closed_flag == false && k.delete_flag == false).ToList();
                Models.TotalWorkProduct = WorkOrder.Sum(k => k.qty);
                Models.YTWorkProduct = WorkOrder.Where(k => k.WIP_contract_id > 0).Sum(k => k.qty);
                Models.HTWorkProduct = WorkOrder.Where(k => k.CRM_contract_detail_id > 0).Sum(k => k.qty);

                var SemiTable = db.INV_semi.Where(k => k.delete_flag == false).ToList();
                Models.TotalSemi = SemiTable.Count;
                Models.confirmSemi = SemiTable.Where(k => k.status == 1).Count();
                Models.NoSemi = SemiTable.Where(k => k.status == 0).Count();

                var LabelsTable = db.INV_labels.Where(k => k.delete_flag == false).ToList();
                Models.TotalLabel = LabelsTable.Count;
                Models.confirmLabel = LabelsTable.Where(k => k.status == 1).Count();
                Models.NoLabel = LabelsTable.Where(k => k.status == 0).Count();

                var SaleTable = db.CRM_contract_header.Where(k => k.delete_flag == false).ToList();
                Models.TotalSale = SaleTable.Sum(k => k.amount);
                Models.confirmSale = SaleTable.Where(k => k.status == 2).Sum(k => k.amount);
                Models.YearSale = SaleTable.Where(k => k.created_time > StartTime && k.created_time < EndTime).Sum(k => k.amount);
                Models.MonthSale = SaleTable.Where(k => k.created_time > MonthTime && k.created_time < EndTime).Sum(k => k.amount);

                var CGTable = db.INV_accessory_purchase_order.Where(k => k.delete_flag == false).ToList();
                Models.TotalCG = CGTable.Sum(k => k.price * k.qty);
                Models.YearCG = CGTable.Where(k => k.created_time > StartTime && k.created_time < EndTime).Sum(k => k.price * k.qty);
                Models.MonthCG = CGTable.Where(k => k.created_time > MonthTime && k.created_time < EndTime).Sum(k => k.price * k.qty);

            }
            return Models;
        }
    }
}
