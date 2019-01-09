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
    public class CRM_HTDal
    {
        public PagedList<CRM_HTZModel> GetPageList(SCRM_HTZModel SModel, int PageIndex, int PageSize,out decimal TotalHT,out decimal? TotalYF)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.CRM_contract_header.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN.Contains(SModel.SN) : true
                            where SModel.CheckState != null && SModel.CheckState == 1 ? p.status == SModel.CheckState : true
                            where !string.IsNullOrEmpty(SModel.UserName) ? p.CRM_customers.name.Contains(SModel.UserName) : true
                            where SModel.FR_flag >= 0 ? p.FR_flag == SModel.FR_flag : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            orderby p.created_time descending
                            select new CRM_HTZModel
                            {
                                id = p.id,
                                SN = p.SN,
                                customer = p.CRM_customers.name,
                                customer_id = p.customer_id,
                                TelPhone = p.CRM_customers.tel,
                                delivery_date = p.delivery_date,
                                amount = p.amount,
                                status = p.status,
                                delivery_channel = p.delivery_channel,
                                freight_carrier = p.freight_carrier,
                                prepay = p.prepay,
                                measure_flag = p.measure_flag,
                                delivery_address = p.delivery_address,
                                signed_user_id = p.signed_user_id,
                                signed_department_id = p.signed_department_id,
                                userName = p.SaleName,
                                created_time = p.created_time,
                                Remark = p.reserved3,
                                FR_flag = p.FR_flag
                            }).ToList();
                TotalHT = List.Sum(k => k.amount);
                TotalYF = List.Sum(k => k.prepay);
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public PagedList<CRM_HTProModel> GetWOPageList(SCRM_HTZModel SModel, int PageIndex, int PageSize)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            DateTime SStartTime = Convert.ToDateTime("1999-12-31");
            DateTime SEndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            if (!string.IsNullOrEmpty(SModel.SStartTime))
            {
                SStartTime = Convert.ToDateTime(SModel.SStartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.SEndTime))
            {
                SEndTime = Convert.ToDateTime(SModel.SEndTime).AddDays(1);
            }
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_contract_detail.Where(k => k.delete_flag == false && k.CRM_contract_header.FR_flag > 0 && k.CRM_contract_header.status == 1 && k.CRM_contract_header.delete_flag == false)
                            where SModel.status!=null ?p.status==SModel.status:true
                            where !string.IsNullOrEmpty(SModel.SN) ? p.CRM_contract_header.SN.Contains(SModel.SN) : true
                            where !string.IsNullOrEmpty(SModel.UserName) ? p.CRM_contract_header.CRM_customers.name.Contains(SModel.UserName) : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            where p.CRM_contract_header.checked_date > SStartTime
                            where p.CRM_contract_header.checked_date < SEndTime
                            orderby p.created_time descending
                            select new CRM_HTProModel
                            {
                                id = p.id,
                                header_id=p.CRM_contract_header.id,
                                SN = p.CRM_contract_header.SN,
                                customer = p.CRM_contract_header.CRM_customers.name,
                                delivery_date = p.CRM_contract_header.delivery_date,
                                checked_date = p.CRM_contract_header.checked_date,
                                productName = p.SYS_product.name,
                                productXL = p.SYS_product.SYS_product_SN.name,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                wood_type_id=p.wood_type_id,
                                product_id=p.product_id,
                                woodName = p.INV_wood_type.name,
                                colorName = p.SYS_colors.name,
                                status=p.status,
                                qty = p.qty,
                                LabelsCount = db.INV_labels.Where(k => k.product_id == p.product_id && k.wood_id == p.wood_type_id && k.status == 1 && k.CRM_contract_detail_id <= 0 && k.flag != 0 && k.delete_flag == false).Count(),
                                SemiCount = db.INV_semi.Where(k => k.product_id == p.product_id && k.wood_id == p.wood_type_id && k.status == 1 && k.delete_flag==false).Count(),
                                hardware_part = p.hardware_part,
                                amount = p.CRM_contract_header.amount,
                                prepay = p.CRM_contract_header.prepay,
                                decoration_part = p.decoration_part,
                                req_others = p.req_others,
                                created_time = p.created_time,
                                WorkCount=p.LabelsCount,
                                Remark = p.CRM_contract_header.reserved3,
                            }).ToList();
                //List<CRM_HTProModel> ListCHModel = new List<CRM_HTProModel>();
                //foreach (var item in List)
                //{
                //    CRM_HTProModel Models = new CRM_HTProModel();
                //    var FR_contract = GetFR_contract(item.header_id);
                //    int WorkCount = 0;
                //    int Qty = 0;
                //    WorkCount = item.WorkCount ?? 0;
                //    Qty = item.qty;
                //    if (item.amount > 0 && (Qty - WorkCount) > 0)
                //    {
                //        decimal d = FR_contract / item.amount;
                //        decimal cd = Convert.ToDecimal(0.5);
                //        if (d > cd)
                //        {
                //            Models = item;
                //            Models.FR_contract = FR_contract;
                //            ListCHModel.Add(Models);
                //        }
                //    }

                //}
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public PagedList<CRM_HTProModel> GetWOTPPageList(SCRM_HTZModel SModel, int PageIndex, int PageSize)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            DateTime SStartTime = Convert.ToDateTime("1999-12-31");
            DateTime SEndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            if (!string.IsNullOrEmpty(SModel.SStartTime))
            {
                SStartTime = Convert.ToDateTime(SModel.SStartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.SEndTime))
            {
                SEndTime = Convert.ToDateTime(SModel.SEndTime).AddDays(1);
            }
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_contract_detail.Where(k => k.delete_flag == false && k.status == 0 && k.CRM_contract_header.FR_flag >= 0 && k.CRM_contract_header.status == 1 && k.CRM_contract_header.delete_flag==false)
                            where p.WIP_workorder.Count <= 0
                            where !string.IsNullOrEmpty(SModel.SN) ? p.CRM_contract_header.SN.Contains(SModel.SN) : true
                            where !string.IsNullOrEmpty(SModel.UserName) ? p.CRM_contract_header.CRM_customers.name.Contains(SModel.UserName) : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            where p.CRM_contract_header.checked_date > SStartTime
                            where p.CRM_contract_header.checked_date < SEndTime
                            orderby p.created_time descending
                            select new CRM_HTProModel
                            {
                                id = p.id,
                                header_id = p.CRM_contract_header.id,
                                SN = p.CRM_contract_header.SN,
                                customer = p.CRM_contract_header.CRM_customers.name,
                                delivery_date = p.CRM_contract_header.delivery_date,
                                checked_date = p.CRM_contract_header.checked_date,
                                productName = p.SYS_product.name,
                                productXL = p.SYS_product.SYS_product_SN.name,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                woodName = p.INV_wood_type.name,
                                colorName = p.SYS_colors.name,
                                qty = p.qty,
                                amount = p.CRM_contract_header.amount,
                                prepay = p.CRM_contract_header.prepay,
                                hardware_part = p.hardware_part,
                                decoration_part = p.decoration_part,
                                req_others = p.req_others,
                                created_time = p.created_time,
                                Remark = p.CRM_contract_header.reserved3,
                            }).ToList();
                List<CRM_HTProModel> ListCHModel = new List<CRM_HTProModel>();
                foreach (var item in List)
                {
                    CRM_HTProModel Models = new CRM_HTProModel();
                    var FR_contract = GetFR_contract(item.header_id);

                    if (item.amount > 0)
                    {
                        decimal d = FR_contract / item.amount;
                        decimal cd = Convert.ToDecimal(0.5);
                        if (d < cd)
                        {
                            Models = item;
                            Models.FR_contract = FR_contract;
                            ListCHModel.Add(Models);
                        }
                    }
                    
                }
                return ListCHModel.ToPagedList(PageIndex, PageSize);
            }
        }
        public void AddOrUpdate(CRM_HTZModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.CRM_contract_header.Where(k => k.id == Models.id).SingleOrDefault();
                    table.SN = Models.SN;
                    table.HTDate = Convert.ToDateTime(Models.HTDate);
                    table.customer_id = Models.customer_id;
                    table.delivery_date = Models.delivery_date;
                    table.amount = Models.amount;
                    table.delivery_channel = Models.delivery_channel;
                    table.freight_carrier = Models.freight_carrier;
                    table.prepay = Models.prepay;
                    table.measure_flag = Models.measure_flag;
                    table.delivery_address = Models.delivery_address;
                    table.Linkman = Models.Linkman;
                    table.Linktel = Models.Linktel;
                    table.signed_user_id = Models.signed_user_id;
                    table.signed_department_id = Models.signed_department_id;
                }
                else
                {
                    CRM_contract_header table = new CRM_contract_header();
                    table.SN = Models.SN;
                    table.HTDate = Convert.ToDateTime(Models.HTDate);
                    table.customer_id = Models.customer_id;
                    table.delivery_date = Models.delivery_date;
                    table.amount = 0;
                    table.delivery_channel = Models.delivery_channel;
                    table.freight_carrier = Models.freight_carrier;
                    table.prepay = Models.prepay;
                    table.measure_flag = Models.measure_flag;
                    table.delivery_address = Models.delivery_address;
                    table.Linkman = Models.Linkman;
                    table.Linktel = Models.Linktel;
                    table.signed_user_id = Models.signed_user_id;
                    table.signed_department_id = Models.signed_department_id;
                    table.department = Models.department;
                    table.SaleName = Models.userName;
                    table.status = 0;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.CRM_contract_header.Add(table);
                }
                db.SaveChanges();

            }
        }
        public void UpdateDelivery(CRM_HTZModel Models)
        {
            using (var db = new XNERPEntities())
            {
                
                var table = db.CRM_contract_header.Where(k => k.id == Models.id).SingleOrDefault();
                
                table.delivery_address = Models.delivery_address;
                table.Linkman = Models.Linkman;
                table.Linktel = Models.Linktel;
              
                db.SaveChanges();

            }
        }
        public CRM_HTZModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.CRM_contract_header.Where(k => k.delete_flag==false && k.id==Id)
                              
                              orderby p.created_time descending
                              select new CRM_HTZModel
                              {
                                  id = p.id,
                                  SN = p.SN,
                                  customer_id = p.customer_id,
                                  customer = p.CRM_customers.name,
                                  delivery_date = p.delivery_date,
                                  amount = p.amount,
                                  status = p.status,
                                  delivery_channel = p.delivery_channel,
                                  freight_carrier = p.freight_carrier,
                                  prepay = p.prepay,
                                  userName = p.SaleName,
                                  measure_flag = p.measure_flag,
                                  delivery_address = p.delivery_address,
                                  signed_user_id = p.signed_user_id,
                                  signed_department_id = p.signed_department_id,
                                  created_time = p.created_time,
                                  Remark = p.reserved3,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.CRM_contract_header.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.CRM_contract_header.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public void Checked(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.CRM_contract_header.Where(k => k.id == Id).SingleOrDefault();
                tables.status = 1;
                tables.check_user_id = new UserDal().GetCurrentUserName().UserId;
                tables.check_user_name = new UserDal().GetCurrentUserName().UserName;
                tables.checked_date = DateTime.Now;

                db.SaveChanges();
            }
        }
        public void AddHTRemark(int Id,string Remark)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.CRM_contract_header.Where(k => k.id == Id).SingleOrDefault();
                tables.reserved3 = Remark;
                db.SaveChanges();
            }
        }
        public DataTable ToExcel(SCRM_HTZModel SModel)
        {
            DataTable Exceltable = new DataTable();
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            DateTime SStartTime = Convert.ToDateTime("1999-12-31");
            DateTime SEndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            if (!string.IsNullOrEmpty(SModel.SStartTime))
            {
                SStartTime = Convert.ToDateTime(SModel.SStartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.SEndTime))
            {
                SEndTime = Convert.ToDateTime(SModel.SEndTime).AddDays(1);
            }
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_contract_header.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN.Contains(SModel.SN) : true
                            where SModel.CheckState != null && SModel.CheckState == 1 ? p.status == SModel.CheckState : true
                            where !string.IsNullOrEmpty(SModel.UserName) ? p.CRM_customers.name.Contains(SModel.UserName) : true
                            where SModel.FR_flag >= 0 ? p.FR_flag == SModel.FR_flag : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            where p.delivery_date > SStartTime
                            where p.delivery_date < SEndTime
                            orderby p.created_time descending
                            select new CRM_HTZModel
                            {
                                id = p.id,
                                SN = p.SN,
                                customer = p.CRM_customers.name,
                                customer_id = p.customer_id,
                                TelPhone = p.CRM_customers.tel,
                                delivery_date = p.delivery_date,
                                amount = p.amount,
                                status = p.status,
                                delivery_channel = p.delivery_channel,
                                freight_carrier = p.freight_carrier,
                                prepay = p.prepay,
                                measure_flag = p.measure_flag,
                                delivery_address = p.delivery_address,
                                signed_user_id = p.signed_user_id,
                                signed_department_id = p.signed_department_id,
                                userName = p.SaleName,
                                created_time = p.created_time,
                                Remark = p.reserved3,
                                FR_flag = p.FR_flag
                            }).ToList();
                decimal TotalHT = List.Sum(k => k.amount);
                decimal? TotalYF = List.Sum(k => k.prepay);
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("订货人", typeof(string));
                    Exceltable.Columns.Add("合同编号", typeof(string));
                    Exceltable.Columns.Add("送货日期", typeof(string));
                    Exceltable.Columns.Add("合同总金额", typeof(string));
                    Exceltable.Columns.Add("预付款", typeof(string));
                    Exceltable.Columns.Add("送货通道", typeof(string));
                    Exceltable.Columns.Add("运费承担方", typeof(string));
                    Exceltable.Columns.Add("付款状态", typeof(string));
                    Exceltable.Columns.Add("合同时间", typeof(string));

                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["订货人"] = item.customer;
                        row["合同编号"] = item.SN;
                        row["送货日期"] = Convert.ToDateTime(item.delivery_date).ToString("yyyy-MM-dd");
                        row["合同总金额"] = item.amount;
                        row["预付款"] = item.prepay;
                        row["送货通道"] = item.delivery_channel;
                        row["运费承担方"] = item.freight_carrier ;
                        row["付款状态"] = item.FR_flag != null && item.FR_flag == 2 ? "已付全款" : item.status == 1 ? "已付预付款" : "未付款";
                        row["合同时间"] = Convert.ToDateTime(item.created_time).ToString("yyyy-MM-dd");

                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;

        }
        public DataTable ToSaleExcel(SCRM_HTZModel SModel)
        {
            DataTable Exceltable = new DataTable();
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            DateTime SStartTime = Convert.ToDateTime("1999-12-31");
            DateTime SEndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            if (!string.IsNullOrEmpty(SModel.SStartTime))
            {
                SStartTime = Convert.ToDateTime(SModel.SStartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.SEndTime))
            {
                SEndTime = Convert.ToDateTime(SModel.SEndTime).AddDays(1);
            }
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_contract_detail.Where(k => k.delete_flag == false && k.CRM_contract_header.FR_flag > 0 && k.CRM_contract_header.status == 1 && k.CRM_contract_header.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.CRM_contract_header.SN.Contains(SModel.SN) : true
                            where SModel.CheckState != null && SModel.CheckState == 1 ? p.status == SModel.CheckState : true
                            where !string.IsNullOrEmpty(SModel.UserName) ? p.CRM_contract_header.CRM_customers.name.Contains(SModel.UserName) : true
                            where SModel.FR_flag >= 0 ? p.CRM_contract_header.FR_flag == SModel.FR_flag : true
                            where p.CRM_contract_header.created_time > StartTime
                            where p.CRM_contract_header.created_time < EndTime
                            where p.CRM_contract_header.delivery_date > SStartTime
                            where p.CRM_contract_header.delivery_date < SEndTime
                            orderby p.created_time descending
                            select new CRM_HTProModel
                            {
                                id = p.id,
                                header_id = p.CRM_contract_header.id,
                                SN = p.CRM_contract_header.SN,
                                customer = p.CRM_contract_header.CRM_customers.name,
                                delivery_date = p.CRM_contract_header.delivery_date,
                                checked_date = p.CRM_contract_header.checked_date,
                                productName = p.SYS_product.name,
                                productXL = p.SYS_product.SYS_product_SN.name,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                wood_type_id = p.wood_type_id,
                                product_id = p.product_id,
                                woodName = p.INV_wood_type.name,
                                colorName = p.SYS_colors.name,
                                status = p.status,
                                qty = p.qty,
                                price=p.price,
                                hardware_part = p.hardware_part,
                                amount = p.CRM_contract_header.amount,
                                prepay = p.CRM_contract_header.prepay,
                                decoration_part = p.decoration_part,
                                req_others = p.req_others,
                                created_time = p.created_time,
                                WorkCount = p.LabelsCount,
                                Remark = p.CRM_contract_header.reserved3,
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("签订日期", typeof(string));
                    Exceltable.Columns.Add("合同编号", typeof(string));
                    Exceltable.Columns.Add("客户名称", typeof(string));
                    Exceltable.Columns.Add("产品名称", typeof(string));
                    Exceltable.Columns.Add("规格", typeof(string));
                    Exceltable.Columns.Add("材质", typeof(string));
                    Exceltable.Columns.Add("单位", typeof(string));
                    Exceltable.Columns.Add("数量", typeof(string));
                    Exceltable.Columns.Add("单价", typeof(string));
                    Exceltable.Columns.Add("合同总金额", typeof(string));
                    

                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["签订日期"] = Convert.ToDateTime(item.created_time).ToString("yyyy-MM-dd");
                        row["合同编号"] = item.SN;
                        row["客户名称"] = item.customer;
                        row["产品名称"] = item.productXL + "_" + item.productName;
                        row["规格"] = Convert.ToDecimal(item.length).ToString("0") + "X" + Convert.ToDecimal(item.width).ToString("0") + "X" + Convert.ToDecimal(item.height).ToString("0");
                        row["材质"] = item.woodName;
                        row["单位"] = "件";
                        row["数量"] = item.qty;
                        row["单价"] = item.price;
                        row["合同总金额"] = item.amount;
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;

        }
        public decimal GetFR_contract(int contract_id)
        {
            using (var db = new XNFinanceEntities())
            {
                var TotailFR = db.FR_contract.Where(k => k.contract_id == contract_id && k.delete_flag == false).Sum(k => k.amount);
                return TotailFR??0;
            }
        }
        //根据日期获取合同总个数
        public int GetCRMHTCount(DateTime CreateTime)
        { 
             using (var db = new XNERPEntities())
            {
                var List = db.CRM_contract_header.Where(k => k.created_time > CreateTime).Count();
                return List;
             }
        }
    }
}
