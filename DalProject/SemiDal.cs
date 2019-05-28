using Infrastructure;
using ModelProject;
using MvcPager.WebControls.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DalProject
{
    public class SemiDal
    {
        private static readonly UserDal UDal = new UserDal();
        public PagedList<SemiModel> GetPageList(SSemiModel SModel, int PageIndex, int PageSize)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_semi.Where(k => k.delete_flag == false)
                            where SModel.product_SN_id != null && SModel.product_SN_id>0 ? SModel.product_SN_id == p.SYS_product.product_SN_id : true
                            where SModel.product_area_id != null && SModel.product_area_id > 0 ? SModel.product_area_id == p.SYS_product.product_area_id : true
                            where SModel.wood_id != null && SModel.wood_id > 0 ? SModel.wood_id == p.wood_id : true
                            where SModel.inv_id != null && SModel.inv_id > 0 ? SModel.inv_id == p.inv_id : true
                            where SModel.status != null && SModel.status >= 0 ? p.status == SModel.status : true
                            where !string.IsNullOrEmpty(SModel.productName)?p.SYS_product.name.Contains(SModel.productName):true
                            where SModel.ProState != null && SModel.ProState == 1 ? p.CRM_id > 0 : SModel.ProState != null && SModel.ProState == 2 ? p.WIP_id > 0 : SModel.ProState != null && SModel.ProState == 3?p.CRM_id<=0 && p.WIP_id<=0:true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            orderby p.created_time descending
                            select new SemiModel
                            {
                                id = p.id,
                                product_id = p.product_id,
                                productName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                wood_id = p.wood_id,
                                woodname = p.INV_wood_type.name,
                                inv_id=p.inv_id,
                                invname = p.INV_inventories.name,
                                status = p.status,
                                input_date = p.input_date,
                                input_user_name = p.input_user_name,
                                CRM_id = p.CRM_id,
                                WIP_id = p.WIP_id,
                                length=p.length,
                                width=p.width,
                                height=p.height
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }

        public void AddOrUpdate(SemiModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id > 0)
                {
                    var table = db.INV_semi.Where(k => k.id == Models.id).SingleOrDefault();
                    table.inv_id = Models.inv_id;
                    table.status = Models.status;
                    table.input_date = DateTime.Now;
                    table.input_user_id = UDal.GetCurrentUserName().UserId;
                    table.input_user_name = UDal.GetCurrentUserName().UserName;
                }
                else
                {
                    for (int i = 0; i < Models.qty; i++)
                    {
                        INV_semi table = new INV_semi();
                        table.product_id = Models.product_id;
                        table.wood_id = Models.wood_id;
                        table.inv_id = Models.inv_id;
                        table.status = 1;
                        table.created_time = DateTime.Now;
                        table.delete_flag = false;
                        table.CRM_id = Models.CRM_id ?? 0;
                        table.WIP_id = Models.WIP_id ?? 0;
                        table.input_date = DateTime.Now;
                        table.input_user_id = UDal.GetCurrentUserName().UserId;
                        table.input_user_name = UDal.GetCurrentUserName().UserName;
                        table.length = Models.length;
                        table.width = Models.width;
                        table.height = Models.height;
                        db.INV_semi.Add(table);
                    }
                }
                db.SaveChanges();
            }
        }
        public void EidtStyle(SemiModel Models)
        {
            using (var db = new XNERPEntities())
            {
                var table = db.INV_semi.Where(k => k.id == Models.id).SingleOrDefault();

                table.length = Models.length;
                table.width = Models.width;
                table.height = Models.height;
                db.SaveChanges();
            }
        }
        public SemiModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.INV_semi.Where(k => k.id == Id)
                              select new SemiModel
                              {
                                  id = p.id,
                                  product_id = p.product_id,
                                  productName = p.SYS_product.name,
                                  ProductXL = p.SYS_product.SYS_product_SN.name,
                                  wood_id = p.wood_id,
                                  woodname = p.INV_wood_type.name,
                                  inv_id = p.inv_id,
                                  invname = p.INV_inventories.name,
                                  status = p.status,
                                  input_date = p.input_date,
                                  input_user_name = p.input_user_name,
                                  length = p.length,
                                  width = p.width,
                                  height = p.height
                              }).SingleOrDefault();
                return tables;
            }
        }
        //移库操作
        public void TransferMore(string ListId, int inv_id)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.INV_semi.Where(k => k.id == Id).SingleOrDefault();
                        tables.inv_id = inv_id;
                        tables.input_date = DateTime.Now;
                    }
                }
                db.SaveChanges();
            }
        }
        public void Checked(SemiModel SModel)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.INV_semi.Where(k => k.id == SModel.id).SingleOrDefault();
                tables.inv_id = SModel.inv_id;
                tables.input_date = DateTime.Now;
                tables.input_user_id = UDal.GetCurrentUserName().UserId;
                tables.input_user_name = UDal.GetCurrentUserName().UserName;
                tables.status = 1;

                int? WId=tables.Work_id;
                var WTable = db.WIP_workorder.Where(k => k.id == WId).SingleOrDefault();
                WTable.closed_flag = true;

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
                        var tables = db.INV_semi.Where(k => k.id == Id).SingleOrDefault();
                        tables.inv_id = 29;
                        tables.input_date = DateTime.Now;
                        tables.input_user_id = UDal.GetCurrentUserName().UserId;
                        tables.input_user_name = UDal.GetCurrentUserName().UserName;
                        tables.status = 1;

                        int? WId = tables.Work_id;
                        var WTable = db.WIP_workorder.Where(k => k.id == WId).SingleOrDefault();
                        WTable.closed_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.INV_semi.Where(k => k.id == Id).SingleOrDefault();

                tables.delete_flag = true;
                 
                db.SaveChanges();
            }
        }
        public string GetProNameDrolistBySNAndArea(int? ProSN,int? ProProArea)
        {
            using (var db = new XNERPEntities())
            {
                var list = (from p in db.SYS_product.Where(k => k.delete_flag==false)
                            where ProSN > 0 ? p.product_SN_id == ProSN : true
                            where ProProArea > 0 ? p.product_area_id == ProProArea : true
                            orderby p.created_time descending
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name,
                                length=p.length,
                                width=p.width,
                                height=p.height
                            }).ToList();
                string NewItme = "";
                foreach (var item in list)
                {
                    var strText = item.Name+"_"+item.length+"_"+item.width+"_"+item.height;
                    var IstrValue = item.Id;
                    NewItme += "<option value=" + IstrValue + ">" + strText + "</option>";
                }
                return NewItme;
            }
        }
        //安排生产
        public void AddWork(string ListId)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var STables = db.INV_semi.Where(k => k.id == Id).SingleOrDefault();
                        int CRM_id = STables.CRM_id ?? 0;
                        int WIP_id = STables.WIP_id ?? 0;
                        int W_id = STables.Work_id ?? 0;
                        DateTime? BOM_ready_date = DateTime.Now;
                        DateTime? BOM_over_date = DateTime.Now;
                        string paper_path = "";
                        string BOM_path = "";
                        string workorder = "WO" + DateTime.Now.ToString("yyyyMMddfff");
                        string CStattus = "";
                        STables.delete_flag = true;
                        
                        if (WIP_id <= 0 && CRM_id <= 0)//如果是库存产品，继续生产算是预投产品
                        {
                            WIP_contract WCTable = new WIP_contract();
                            WCTable.product_id = STables.product_id;
                            WCTable.wood_id = STables.wood_id;
                            WCTable.color = STables.color??"";
                            WCTable.custom_flag = false;
                            WCTable.length = STables.length;
                            WCTable.width = STables.width;
                            WCTable.height = STables.height;
                            WCTable.created_time = DateTime.Now;
                            WCTable.delete_flag = false;
                            db.WIP_contract.Add(WCTable);
                            db.SaveChanges();
                            WIP_id = WCTable.id;//把刚插入到预投产品的ID赋值

                            CStattus = "预投产品（半成品）";
                        }
                        if (WIP_id > 0)//判断是否是预投产品
                        { workorder = "WP" + DateTime.Now.ToString("yyyyMMddfff"); }
                        if (W_id > 0)
                        {
                            var OldWWTable = db.WIP_workorder.Where(k => k.id == W_id).SingleOrDefault();
                            BOM_ready_date = OldWWTable.BOM_ready_date;
                            BOM_over_date = OldWWTable.BOM_over_date;
                            paper_path = OldWWTable.paper_path;
                            BOM_path = OldWWTable.BOM_path;
                        }

                        WIP_workorder WWTable = new WIP_workorder();
                        WWTable.workorder = workorder;
                        WWTable.CRM_contract_detail_id = CRM_id;
                        WWTable.WIP_contract_id = WIP_id;
                        WWTable.qty = 1;
                        WWTable.status = 5;
                        WWTable.BOM_ready_date = BOM_ready_date;
                        WWTable.BOM_over_date = BOM_over_date;
                        WWTable.paper_path = BOM_path;
                        WWTable.BOM_path = BOM_path;
                        WWTable.closed_flag = false;
                        WWTable.created_time = DateTime.Now;
                        WWTable.delete_flag = false;
                        if (string.IsNullOrEmpty(CStattus))
                        {
                            WWTable.reserved3 = CStattus;
                        }
                        db.WIP_workorder.Add(WWTable);
                    }
                }
                db.SaveChanges();
            }
        }
        //返修
        public void AddReWork(SemiModel models)
        {
            using (var db = new XNERPEntities())
            {
                int Id = models.id;
                var STables = db.INV_semi.Where(k => k.id == Id).SingleOrDefault();
                int CRM_id = STables.CRM_id ?? 0;
                int WIP_id = STables.WIP_id ?? 0;
                int W_id = STables.Work_id ?? 0;
                DateTime? BOM_ready_date = DateTime.Now;
                DateTime? BOM_over_date = DateTime.Now;
                string paper_path = "";
                string BOM_path = "";
                string workorder = "WO" + DateTime.Now.ToString("yyyyMMddfff");
                STables.delete_flag = true;
                var OldWWTable = db.WIP_workorder.Where(k => k.id == W_id).SingleOrDefault();
                if (OldWWTable != null)
                {
                    BOM_ready_date = OldWWTable.BOM_ready_date;
                    BOM_over_date = OldWWTable.BOM_over_date;
                    paper_path = OldWWTable.paper_path;
                    BOM_path = OldWWTable.BOM_path;
                }

                if (WIP_id > 0)//判断是否是预投产品
                { workorder = "WP" + DateTime.Now.ToString("yyyyMMddfff"); }

                WIP_workorder WWTable = new WIP_workorder();
                WWTable.workorder = workorder;
                WWTable.CRM_contract_detail_id = CRM_id;
                WWTable.WIP_contract_id = WIP_id;
                WWTable.qty = 1;
                WWTable.status = 5;
                WWTable.BOM_ready_date = BOM_ready_date;
                WWTable.BOM_over_date = BOM_over_date;
                WWTable.paper_path = BOM_path;
                WWTable.BOM_path = BOM_path;
                WWTable.closed_flag = false;
                WWTable.created_time = DateTime.Now;
                WWTable.delete_flag = false;
                WWTable.remark = models.Remark;
                db.WIP_workorder.Add(WWTable);
                
                db.SaveChanges();
            }
        }
        public DataTable ToExcel(SSemiModel SModel)
        {
            DataTable Exceltable = new DataTable();
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_semi.Where(k => k.delete_flag == false)
                            where SModel.product_SN_id != null && SModel.product_SN_id>0 ? SModel.product_SN_id == p.SYS_product.product_SN_id : true
                            where SModel.product_area_id != null && SModel.product_area_id > 0 ? SModel.product_area_id == p.SYS_product.product_area_id : true
                            where SModel.wood_id != null && SModel.wood_id > 0 ? SModel.wood_id == p.wood_id : true
                            where SModel.inv_id != null && SModel.inv_id > 0 ? SModel.inv_id == p.inv_id : true
                            where SModel.status != null && SModel.status >= 0 ? p.status == SModel.status : true
                            where !string.IsNullOrEmpty(SModel.productName)?p.SYS_product.name.Contains(SModel.productName):true
                            where SModel.ProState != null && SModel.ProState == 1 ? p.CRM_id > 0 : SModel.ProState != null && SModel.ProState == 2 ? p.WIP_id > 0 : SModel.ProState != null && SModel.ProState == 3?p.CRM_id<=0 && p.WIP_id<=0:true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            orderby p.created_time descending
                            select new SemiModel
                            {
                                id = p.id,
                                product_id = p.product_id,
                                productName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                areaName = p.SYS_product.SYS_product_area.name,
                                wood_id = p.wood_id,
                                woodname = p.INV_wood_type.name,
                                inv_id=p.inv_id,
                                invname = p.INV_inventories.name,
                                status = p.status,
                                input_date = p.input_date,
                                input_user_name = p.input_user_name,
                                CRM_id = p.CRM_id,
                                WIP_id = p.WIP_id,
                                length=p.length,
                                width=p.width,
                                height=p.height,
                                volume = p.SYS_product.volume,
                                W_BZ = p.INV_wood_type.g_bz,
                                W_price = p.INV_wood_type.prcie
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("产品名称", typeof(string));
                    Exceltable.Columns.Add("产品系列", typeof(string));
                    Exceltable.Columns.Add("产品区域", typeof(string));
                    Exceltable.Columns.Add("材质", typeof(string));
                    Exceltable.Columns.Add("长", typeof(string));
                    Exceltable.Columns.Add("宽", typeof(string));
                    Exceltable.Columns.Add("高", typeof(string));
                    Exceltable.Columns.Add("所入仓库", typeof(string));
                    Exceltable.Columns.Add("进库日期", typeof(string));
                    Exceltable.Columns.Add("状态", typeof(string));
                    Exceltable.Columns.Add("所属方式", typeof(string));
                    Exceltable.Columns.Add("材积", typeof(string));
                    Exceltable.Columns.Add("比重", typeof(string));
                    Exceltable.Columns.Add("单价", typeof(string));

                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["产品名称"] = item.productName;
                        row["产品系列"] = item.ProductXL;
                        row["产品区域"] = item.areaName;
                        row["材质"] = item.woodname;
                        row["长"] = item.length;
                        row["宽"] = item.width;
                        row["高"] = item.height;
                        row["所入仓库"] = item.invname;
                        row["进库日期"] = Convert.ToDateTime(item.input_date).ToString("yyyy-MM-dd"); ;
                        row["状态"] = item.status != null && item.status == 2 ? "已出库" : item.status == 1 ? "已入库" : "未确认";
                        row["所属方式"] = item.CRM_id != null && item.CRM_id > 0 ? "销售产品" : item.WIP_id != null && item.WIP_id > 0 ? "预投产品" : "盘点产品";
                        row["材积"] = item.volume;
                        row["比重"] = item.W_BZ;
                        row["单价"] = item.W_price;
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;

        }
    }
}
