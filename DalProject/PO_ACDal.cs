using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using MvcPager.WebControls.Mvc;
using ModelProject;
using System.Web.Mvc;
using System.Data;

namespace DalProject
{
    public class PO_ACDal
    {
        public PagedList<PO_ACModel> GetPageList(SPO_ACModel SModel, int PageIndex, int PageSize, out decimal? TotalHT)
        {
            
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            if (SModel.StartTime != null)
            { StartTime = Convert.ToDateTime(Convert.ToDateTime(SModel.StartTime).AddDays(-1)); }
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (SModel.EndTime != null )
            { EndTime = Convert.ToDateTime(Convert.ToDateTime(SModel.EndTime).AddDays(1)); }
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.INV_accessory_purchase_order.Where(k => k.delete_flag == false)
                            where p.require_date > StartTime
                            where p.require_date < EndTime
                            where SModel.CheckState != null && SModel.CheckState >= 0 ? p.status == SModel.CheckState : true
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN == SModel.SN : true
                            where SModel.supplier_id != null && SModel.supplier_id>0?p.supplier_id==SModel.supplier_id:true
                            where SModel.FR_flag != null && SModel.FR_flag > 0 ? p.FR_flag == SModel.FR_flag : true
                            orderby p.created_time descending
                            select new PO_ACModel
                            {
                                id = p.id,
                                SN=p.SN,
                                apply_name = p.apply_user_name,
                                accessory_name = p.INV_accessory_type.name,
                                require_date=p.require_date,
                                qty = p.qty,
                                remark = p.require_remark,
                                unit = p.unit,
                                price = p.price,
                                supplier_id = p.supplier_id,
                                supplier_name=p.CRM_suppliers.name,
                                created_time = p.created_time,
                                status = p.status,
                                check_user_name = p.check_user_name,
                                checked_date = p.checked_date,
                                input_user_name = p.input_user_name,
                                FR_flag = p.FR_flag
                            }).ToList();
                TotalHT = List.Sum(k => k.price * k.qty);
                return List.OrderByDescending(k=>k.created_time).ToPagedList(PageIndex, PageSize);
            }
        }
        public PagedList<PO_ACModel> GetCPageList(SPO_ACModel SModel, int PageIndex, int PageSize)
        {
            int CheckState = 0;
            if (SModel.CheckState > 0)
            { CheckState = SModel.CheckState; }
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            if (SModel.StartTime != null)
            { StartTime = Convert.ToDateTime(Convert.ToDateTime(SModel.StartTime).AddDays(-1)); }
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (SModel.EndTime != null)
            { EndTime = Convert.ToDateTime(Convert.ToDateTime(SModel.EndTime).AddDays(1)); }
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_accessory_purchase_order.Where(k => k.delete_flag == false && k.status==0)
                            where p.require_date > StartTime
                            where p.require_date < EndTime
                            where p.status == CheckState
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN == SModel.SN : true
                            orderby p.created_time descending
                            select new PO_ACModel
                            {
                                id = p.id,
                                SN = p.SN,
                                apply_name = p.apply_user_name,
                                accessory_name = p.INV_accessory_type.name,
                                require_date = p.require_date,
                                qty = p.qty,
                                remark = p.require_remark,
                                unit = p.unit,
                                price = p.price,
                                supplier_id = p.supplier_id,
                                supplier_name=p.CRM_suppliers.name,
                                created_time = p.created_time,
                                status = p.status,
                                check_user_name = p.check_user_name,
                                checked_date = p.checked_date,
                                input_user_name = p.input_user_name,
                            }).ToList();
                return List.OrderByDescending(k => k.created_time).ToPagedList(PageIndex, PageSize);
            }
        }
        public void AddOrUpdate(PO_ACModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.INV_accessory_purchase_order.Where(k => k.id == Models.id).SingleOrDefault();
                    table.apply_user_id = Models.apply_id;
                    table.apply_user_name = Models.apply_name;
                    table.apply_user_department = Models.apply_department;
                    table.require_date = DateTime.Now;
                    table.accessory_id = Models.accessory_id;
                    table.qty = Models.qty;
                    table.unit = Models.unit;
                    table.price = Models.price;
                    table.supplier_id = Models.supplier_id;
                    table.require_remark = Models.supplier_name;
                    table.input_user_id = Models.input_user_id;
                    table.input_user_name = Models.input_user_name;
                    table.FR_flag = 0;
                }
                else
                {
                    INV_accessory_purchase_order table = new INV_accessory_purchase_order();
                    table.SN = "POA-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    table.apply_user_id = Models.apply_id;
                    table.apply_user_name = Models.apply_name;
                    table.apply_user_department = Models.apply_department;
                    table.require_date = Models.require_date;

                    table.accessory_id = Models.accessory_id;
                    table.qty = Models.qty;
                    table.unit = Models.unit;
                    table.price = Models.price;
                    table.supplier_id = Models.supplier_id;
                    table.require_remark = Models.remark;
                    table.input_user_id = Models.input_user_id;
                    table.input_user_name = Models.input_user_name;
                    table.FR_flag = 0;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.INV_accessory_purchase_order.Add(table);
                }
                db.SaveChanges();

            }
        }
        //根据文章Id获取内容
        public PO_ACModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.INV_accessory_purchase_order.Where(k => k.id == Id)
                              select new PO_ACModel
                              {
                                  id = p.id,
                                  apply_name = p.apply_user_name,
                                  qty = p.qty,
                                  remark = p.remark,
                                  unit = p.unit,
                                  price = p.price,
                                  supplier_id = p.supplier_id,
                                  created_time = p.created_time,
                                  status = p.status,
                                  check_user_name = p.check_user_name,
                                  checked_date = p.checked_date,
                                  input_user_name = p.input_user_name,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.INV_accessory_purchase_order.Where(k => k.id == Id).SingleOrDefault();
                tables.delete_flag = true;
                db.SaveChanges();
            }
        }
        public void DeleteMore(string ListId)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.INV_accessory_purchase_order.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public void FRMore(string ListId)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.INV_accessory_purchase_order.Where(k => k.id == Id).SingleOrDefault();
                        tables.FR_flag = 1;
                    }
                }
                db.SaveChanges();
            }
        }
        public void CheckedMore(string ListId, int check_user_id, string check_user_name)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.INV_accessory_purchase_order.Where(k => k.id == Id).SingleOrDefault();
                        tables.status = 1;
                        tables.check_user_id = check_user_id;
                        tables.check_user_name=check_user_name;
                        tables.checked_date = DateTime.Now;

                        decimal? C_count = tables.qty;
                        var StTable = db.INV_accessory_stock.Where(k => k.accessory_type_id == tables.accessory_id).FirstOrDefault();
                        if (StTable != null)
                        {
                            StTable.C_count = tables.qty + StTable.C_count;
                        }
                        else {
                            StTable = new INV_accessory_stock();
                            StTable.Id = Guid.NewGuid();
                            StTable.accessory_type_id = tables.accessory_id;
                            StTable.inventory_id = 20;
                            StTable.C_count = C_count;
                            StTable.W_count = 0;
                            db.INV_accessory_stock.Add(StTable);
                        }
                    }
                }
                db.SaveChanges();
            }
        }
        //获取供应商
        public List<SelectListItem> GetGYSDrolist(int? Id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择供应商", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<CRM_suppliers> model = db.CRM_suppliers.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name + "_" + item.id, Value = item.id.ToString(), Selected = Id.HasValue && item.id.Equals(Id) });
                }
            }
            return items;
        }
        public DataTable ToExcel(SPO_ACModel SModel)
        {
            DataTable Exceltable = new DataTable();
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            if (SModel.StartTime != null)
            { StartTime = Convert.ToDateTime(Convert.ToDateTime(SModel.StartTime).AddDays(-1)); }
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (SModel.EndTime != null)
            { EndTime = Convert.ToDateTime(Convert.ToDateTime(SModel.EndTime).AddDays(1)); }
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_accessory_purchase_order.Where(k => k.delete_flag == false)
                            where p.require_date > StartTime
                            where p.require_date < EndTime
                            where SModel.CheckState != null && SModel.CheckState >= 0 ? p.status == SModel.CheckState : true
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN == SModel.SN : true
                            orderby p.created_time descending
                            select new PO_ACModel
                            {
                                id = p.id,
                                SN = p.SN,
                                apply_name = p.apply_user_name,
                                accessory_name = p.INV_accessory_type.name,
                                require_date = p.require_date,
                                qty = p.qty,
                                remark = p.require_remark,
                                unit = p.unit,
                                price = p.price,
                                supplier_id = p.supplier_id,
                                supplier_name = p.CRM_suppliers.name,
                                created_time = p.created_time,
                                status = p.status,
                                check_user_name = p.check_user_name,
                                checked_date = p.checked_date,
                                input_user_name = p.input_user_name,
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("供应商", typeof(string));
                    Exceltable.Columns.Add("采购名称", typeof(string));
                    Exceltable.Columns.Add("申请人", typeof(string));
                    Exceltable.Columns.Add("申请时间", typeof(string));
                    Exceltable.Columns.Add("采购数量", typeof(string));
                    Exceltable.Columns.Add("单价（元）", typeof(string));
                    Exceltable.Columns.Add("总价（元）", typeof(string));

                    foreach (var item in List)
                    {
                        decimal? TotalPrce = item.price * item.qty;
                        DataRow row = Exceltable.NewRow();
                        row["供应商"] = item.supplier_name;
                        row["采购名称"] = item.accessory_name;
                        row["申请人"] = item.apply_name;
                        row["申请时间"] = @Convert.ToDateTime(item.require_date).ToString("yyyy-MM-dd");
                        row["采购数量"] = item.qty+"("+item.unit+")";
                        row["单价（元）"] = item.price;
                        row["总价（元）"] = (TotalPrce.Value).ToString("0.00");
                        
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;

        }
    }
}
