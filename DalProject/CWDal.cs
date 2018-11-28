using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using System.Data;
using ModelProject;
using MvcPager.WebControls.Mvc;

namespace DalProject
{
    public class CWDal
    {
        public PagedList<CRM_HTProModel> GetSalePagelist(SCRM_HTZModel SModel)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            DateTime SStartTime = Convert.ToDateTime("1999-12-31");
            DateTime SEndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            if (!string.IsNullOrEmpty(SModel.SStartTime))
            {
                SStartTime = Convert.ToDateTime(SModel.SStartTime);
            }
            if (!string.IsNullOrEmpty(SModel.SEndTime))
            {
                SEndTime = Convert.ToDateTime(SModel.SEndTime).AddDays(1);
            }
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_contract_detail.Where(k => k.delete_flag == false && k.CRM_contract_header.status == 1 && k.CRM_contract_header.delete_flag == false)
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
                                price = p.price,
                                hardware_part = p.hardware_part,
                                amount = p.CRM_contract_header.amount,
                                prepay = p.CRM_contract_header.prepay,
                                decoration_part = p.decoration_part,
                                req_others = p.req_others,
                                created_time = p.created_time,
                                WorkCount = p.LabelsCount,
                                Remark = p.CRM_contract_header.reserved3,
                                OrderNum = p.CRM_delivery_tmp_header.FirstOrDefault().OrderNum
                            }).ToList();
                List<CRM_HTProModel> ListCHModel = new List<CRM_HTProModel>();
                foreach (var item in List)
                {
                    CRM_HTProModel Models = new CRM_HTProModel();
                    var FR_contract = GetFR_contract(item.header_id);
                    Models = item;
                    Models.FR_contract = FR_contract;
                    ListCHModel.Add(Models);

                }
                return ListCHModel.ToPagedList(SModel.PageIndex.Value, SModel.PageSize.Value);
            }

        }
        public decimal GetFR_contract(int contract_id)
        {
            using (var db = new XNFinanceEntities())
            {
                var TotailFR = db.FR_contract.Where(k => k.contract_id == contract_id && k.delete_flag == false).Sum(k => k.amount);
                return TotailFR ?? 0;
            }
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
                                price = p.price,
                                hardware_part = p.hardware_part,
                                amount = p.CRM_contract_header.amount,
                                prepay = p.CRM_contract_header.prepay,
                                decoration_part = p.decoration_part,
                                req_others = p.req_others,
                                created_time = p.created_time,
                                WorkCount = p.LabelsCount,
                                Remark = p.CRM_contract_header.reserved3,
                                OrderNum = p.CRM_delivery_tmp_header.FirstOrDefault().OrderNum
                            }).ToList();
                List<CRM_HTProModel> ListCHModel = new List<CRM_HTProModel>();
                foreach (var item in List)
                {
                    CRM_HTProModel Models = new CRM_HTProModel();
                    var FR_contract = GetFR_contract(item.header_id);
                    Models = item;
                    Models.FR_contract = FR_contract;
                    ListCHModel.Add(Models);

                }
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
                    Exceltable.Columns.Add("收款", typeof(string));
                    Exceltable.Columns.Add("剩余金额", typeof(string));
                    Exceltable.Columns.Add("送货日期", typeof(string));
                    Exceltable.Columns.Add("运送单号", typeof(string));
                    Exceltable.Columns.Add("是否结清", typeof(string));
                    foreach (var item in List)
                    {
                        decimal Surplus = item.amount - item.FR_contract;
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
                        row["收款"] = item.FR_contract;
                        row["剩余金额"] = Surplus;
                        row["送货日期"] = Convert.ToDateTime(item.delivery_date).ToString("yyyy-MM-dd");
                        row["运送单号"] = item.OrderNum;
                        row["是否结清"] = Surplus > 0 || string.IsNullOrEmpty(item.OrderNum) ? "否" : "是";
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;

        }
    }
}
