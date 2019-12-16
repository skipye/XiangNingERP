using Infrastructure;
using ModelProject;
using MvcPager.WebControls.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace DalProject
{
    public class LabelsDal
    {
        private static readonly UserDal UDal = new UserDal();
        public PagedList<LabelsModel> GetPageList(SLabelsModel SModel, int PageIndex, int PageSize)
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
                var List = (from p in db.INV_labels.Where(k => k.delete_flag == false)
                            where SModel.product_SN_id != null && SModel.product_SN_id>0 ? SModel.product_SN_id == p.SYS_product.product_SN_id : true
                            where SModel.product_area_id != null && SModel.product_area_id > 0 ? SModel.product_area_id == p.SYS_product.product_area_id : true
                            where SModel.wood_id != null && SModel.wood_id > 0 ? SModel.wood_id == p.wood_id : true
                            where SModel.inv_id != null && SModel.inv_id > 0 ? SModel.inv_id == p.inv_id : true
                            where SModel.status != null && SModel.status >= 0 ? p.status == SModel.status : true
                            where !string.IsNullOrEmpty(SModel.productName)?p.SYS_product.name.Contains(SModel.productName):true
                            where SModel.ProState != null && SModel.ProState == 1 ? p.flag == 0 : SModel.ProState != null && SModel.ProState == 2 ? p.flag==1 : SModel.ProState != null && SModel.ProState == 3 ? p.flag == 2 : true
                            where !string.IsNullOrEmpty(SModel.ProSN)?p.CRM_contract_detail.CRM_contract_header.SN.Contains(SModel.ProSN):true
                            where !string.IsNullOrEmpty(SModel.SaleName) ? p.CRM_customers.name.Contains(SModel.SaleName) : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            orderby p.created_time descending
                            select new LabelsModel
                            {
                                id = p.id,
                                product_id = p.product_id,
                                ProductName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                wood_id = p.wood_id,
                                woodname = p.INV_wood_type.name,
                                inv_id=p.inv_id,
                                invname = p.INV_inventories.name,
                                status = p.status,
                                input_date = p.created_time,
                                SN = p.SN,
                                product_SN_Name = p.product_SN,
                                color = p.color,
                                flag = p.flag,
                                style = p.style,
                                customersName = p.CRM_customers.name,
                                CRM_SN=p.CRM_contract_detail.CRM_contract_header.SN,
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public List<LabelsModel> GetWorkLabelsList(SLabelsModel SModel)
        {
            
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_labels.Where(k => k.delete_flag == false && k.flag>0)
                            where SModel.wood_id != null && SModel.wood_id > 0 ? SModel.wood_id == p.wood_id : true
                            where SModel.product_id!=null && SModel.product_id>0?p.product_id==SModel.product_id:true
                            where !string.IsNullOrEmpty(SModel.color)?p.color.Contains(SModel.color):true
                            orderby p.created_time descending
                            select new LabelsModel
                            {
                                id = p.id,
                                product_id = p.product_id,
                                ProductName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                wood_id = p.wood_id,
                                woodname = p.INV_wood_type.name,
                                inv_id = p.inv_id,
                                invname = p.INV_inventories.name,
                                status = p.status,
                                input_date = p.created_time,
                                SN = p.SN,
                                product_SN_Name = p.product_SN,
                                color = p.color,
                                flag = p.flag,
                                style = p.style,
                                customersName = p.CRM_customers.name,
                            }).ToList();
                return List;
            }
        }
        public List<LabelsModel> GetCancelLabelsList(SLabelsModel SModel)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_labels.Where(k => k.delete_flag == false && k.CRM_contract_detail_id==SModel.Id)
                            orderby p.created_time descending
                            select new LabelsModel
                            {
                                id = p.id,
                                product_id = p.product_id,
                                ProductName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                wood_id = p.wood_id,
                                woodname = p.INV_wood_type.name,
                                inv_id = p.inv_id,
                                invname = p.INV_inventories.name,
                                status = p.status,
                                input_date = p.created_time,
                                SN = p.SN,
                                product_SN_Name = p.product_SN,
                                color = p.color,
                                flag = p.flag,
                                style = p.style,
                                customersName = p.CRM_customers.name,
                            }).ToList();
                return List;
            }
        }
        public PagedList<LabelsModel> GetDeliveryList(SLabelsModel SModel)
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
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_delivery_tmp_header.Where(k=>k.status==false)
                            where !string.IsNullOrEmpty(SModel.SaleName) ? p.INV_labels.CRM_customers.name.Contains(SModel.SaleName) : true
                            where !string.IsNullOrEmpty(SModel.ProSN) ? p.CRM_contract_header.SN.Contains(SModel.ProSN) : true
                            where SModel.inv_id != null && SModel.inv_id > 0 ? SModel.inv_id == p.INV_labels.inv_id: true
                            where p.DeliverTime>StartTime
                            where p.DeliverTime < EndTime
                            orderby p.created_time descending
                            select new LabelsModel
                            {
                                id = p.id,
                                CRM_SN=p.CRM_contract_header.SN,
                                CRM_HTId = p.contract_header_id,
                                ProductName = p.INV_labels.SYS_product.name,
                                ProductXL = p.INV_labels.SYS_product.SYS_product_SN.name,
                                wood_id = p.INV_labels.wood_id,
                                woodname = p.INV_labels.INV_wood_type.name,
                                inv_id = p.INV_labels.inv_id,
                                invname = p.INV_labels.INV_inventories.name,
                                input_date = p.created_time,
                                color = p.INV_labels.color,
                                style = p.INV_labels.style,
                                customersName = p.CRM_contract_header.CRM_customers.name,
                                delete_flag=p.delete_flag,
                                OrderNum=p.OrderNum,
                                CW_checked=p.CW_checked,
                                CZ_checked=p.CZ_checked,
                                DeliverTime=p.DeliverTime
                            }).ToList();
                return List.ToPagedList(SModel.PageIndex??1, SModel.PagePSize??10); 
            }
        }
        public void Add(LabelsModel Models)
        {
            Random r = new Random();
            using (var db = new XNERPEntities())
            {
                for (int i = 0; i < Models.qty; i++)
                {
                    var tables = db.SYS_product.Where(k => k.id == Models.product_id).SingleOrDefault();
                    string ProductSN = tables.SYS_product_SN.SN;
                    string ProductNum = "";
                    if (!string.IsNullOrEmpty(ProductSN))
                    {
                        ProductNum = "XN" + ProductSN.Substring(0, 2) + DateTime.Now.ToString("MMdd") + r.Next(10000, 100000);
                    }
                    INV_labels INTable = new INV_labels();
                    INTable.SN = "LA" + DateTime.Now.ToString("yyyyMMdd") + r.Next(100000,1000000);
                    INTable.product_SN = ProductNum;
                    INTable.product_id = Models.product_id;
                    INTable.style = Models.length + "×" + Models.width + "×" + Models.height;
                    INTable.wood_id = Models.wood_id;
                    INTable.color = Models.color;
                    INTable.customer_id = 0;
                    INTable.check_date = DateTime.Now;
                    INTable.company = "上海香凝工艺品有限公司";
                    INTable.address = "上海市青浦区朱家角朱枫公路1355号";
                    INTable.website = "www.xiangninghm.com";
                    INTable.status = 1;
                    INTable.CRM_contract_detail_id = 0;
                    INTable.flag = 2;
                    INTable.inv_id = Models.inv;
                    INTable.created_time = DateTime.Now;
                    INTable.delete_flag = false;
                    INTable.WorkId = 0;
                    INTable.WIP_contract_id = 0;
                    db.INV_labels.Add(INTable);
                }
                db.SaveChanges();
            }
        }
        public LabelsModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.INV_labels.Where(k => k.id == Id)
                              select new LabelsModel
                              {
                                  id = p.id,
                                  product_id = p.product_id,
                                  ProductName = p.SYS_product.name,
                                  ProductXL = p.SYS_product.SYS_product_SN.name,
                                  wood_id = p.wood_id,
                                  woodname = p.INV_wood_type.name,
                                  inv_id = p.inv_id,
                                  invname = p.INV_inventories.name,
                                  status = p.status,
                                  style=p.style,
                                  input_date = p.created_time,
                                  SN = p.SN,
                                  product_SN_Name = p.product_SN,
                                  color = p.color,
                                  check_date = p.check_date
                              }).SingleOrDefault();
                return tables;
            }
        }
        //编辑尺寸
        public void editStyle(LabelsModel models)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.INV_labels.Where(k => k.id == models.id).SingleOrDefault();
                tables.style = models.length + "×" + models.width + "×" + models.height;
                tables.color = models.color;
                db.SaveChanges();
            }
        }
        //绑定合同操作
        public void CheckLabels(string ListId, int CRM_Id)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                int UpdateCount = ArrId.Count() - 1;
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.INV_labels.Where(k => k.id == Id).SingleOrDefault();
                        tables.CRM_contract_detail_id = CRM_Id;
                        tables.flag = 0;
                        tables.WIP_contract_id = 0;
                        tables.check_date = DateTime.Now;
                    }
                }
                int LabelsCount = UpdateCount;
                int qty = 1;
                var CRMTables = db.CRM_contract_detail.Where(k => k.id == CRM_Id).SingleOrDefault();
                CRMTables.LabelsCount = UpdateCount;
                qty = CRMTables.qty;
                if ((qty - LabelsCount) <= 0)//判断是否已经没了
                {
                    CRMTables.status = 2;
                }
                db.SaveChanges();
            }
        }
        //取消绑定合同操作
        public void CancelOrder(string ListId)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                int UpdateCount = ArrId.Count() - 1;
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.INV_labels.Where(k => k.id == Id).SingleOrDefault();
                        tables.CRM_contract_detail_id = 0;
                        tables.flag = 1;
                        tables.WIP_contract_id = 0;
                        tables.check_date = DateTime.Now;

                        var CRMTables = db.CRM_contract_detail.Where(k => k.id == tables.CRM_contract_detail_id).SingleOrDefault();
                        if (CRMTables != null)
                        {
                            CRMTables.status = 0;
                            if(CRMTables.LabelsCount!=null && CRMTables.LabelsCount>=1)
                            { CRMTables.LabelsCount = CRMTables.LabelsCount - 1; }
                        }
                        
                    }
                }
                
                db.SaveChanges();
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
                        var tables = db.INV_labels.Where(k => k.id == Id).SingleOrDefault();
                        tables.inv_id = inv_id;
                        //tables.created_time = DateTime.Now;
                    }
                }
                db.SaveChanges();
            }
        }
        //送货维修操作
        public void DeliveryMore(string ListId,string DeliverTime)
        {
            DateTime DeliveTime = DateTime.Now;
            if (!string.IsNullOrEmpty(DeliverTime))
            { DeliveTime = Convert.ToDateTime(DeliverTime); }
            Random r = new Random();
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int CRMId = 0;
                        int Id = Convert.ToInt32(item);
                        var tables = db.INV_labels.Where(k => k.id == Id).SingleOrDefault();
                        CRMId = tables.CRM_contract_detail_id??0;
                        if (CRMId > 0)
                        {
                            CRM_delivery_tmp_header HTables = new CRM_delivery_tmp_header();
                            HTables.contract_header_id = tables.CRM_contract_detail.header_id;
                            HTables.contract_detail_id = tables.CRM_contract_detail_id ?? 0;
                            HTables.created_by = UDal.GetCurrentUserName().UserId;
                            HTables.created_time = DateTime.Now;
                            HTables.delete_flag = false;
                            HTables.label_id = Id;
                            HTables.CW_checked = false;
                            HTables.CZ_checked = false;
                            HTables.OrderNum = "XN" + DateTime.Now.ToString("yyMMdd") + r.Next(100, 1000);
                            HTables.status = false;
                            HTables.DeliverTime = DeliveTime;
                            db.CRM_delivery_tmp_header.Add(HTables);

                            tables.delete_flag = true;
                            tables.status = 9;
                        }
                    }
                }
                db.SaveChanges();
            }
        }
        public void CheckedDeliveryMore(string ListId,bool IsCW)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.CRM_delivery_detail.Where(k => k.id == Id).SingleOrDefault();
                        if (IsCW == true)
                        { tables.CW_checked = true;tables.CW_checked_Time = DateTime.Now; } else
                        { tables.CZ_checked = true;tables.CZ_checked_Time = DateTime.Now; }
                    }
                }
                db.SaveChanges();
            }
        }
        public void CheckDelivery(string ListId, string OrderNum, string DeliverTime)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.CRM_delivery_tmp_header.Where(k => k.id == Id).SingleOrDefault();
                        int HT_Head_id = tables.contract_header_id;
                        int HT_Detail_Id = tables.contract_detail_id;
                        int labelId = tables.label_id??0;
                        if (!string.IsNullOrEmpty(OrderNum))
                        { tables.OrderNum = OrderNum; }
                        if (!string.IsNullOrEmpty(DeliverTime))
                        { tables.DeliverTime = Convert.ToDateTime(DeliverTime); }
                        tables.delete_flag = true;
                        tables.created_time = DateTime.Now;

                        //var HTDetailTable = db.CRM_contract_detail.Where(k => Id == HT_Detail_Id).FirstOrDefault();
                        //HTDetailTable.SendCount = HTDetailTable.SendCount ?? 0 + 1;

                        //下面去把送货申请的送货状态更新掉
                        var DeliveryTable = db.CRM_delivery_detail.Where(k => k.delete_flag == false && k.status == 0 && k.header_id == HT_Head_id && k.contract_detail_id == HT_Detail_Id).FirstOrDefault();
                        if (DeliveryTable != null)
                        {
                            DeliveryTable.status = 1;
                            DeliveryTable.attach_label_id = labelId;
                        }
                        if (labelId > 0)
                        {
                            var LaTab = db.INV_labels.Where(k => k.id == labelId).FirstOrDefault();
                            LaTab.InputDateTime = tables.DeliverTime;
                        }
                    }
                    db.SaveChanges();
                }
                
            }
        }
        public void DeleteDelivery(string ListId)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.CRM_delivery_detail.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                        //下面还原库存产品
                        //var LabelsId=tables.label_id;
                        //var LabelsTable = db.INV_labels.Where(k => k.id == LabelsId).FirstOrDefault();
                        //LabelsTable.status = 8;
                        //LabelsTable.delete_flag = false;

                    }
                }
                db.SaveChanges();
            }
        }
        public void CheckMore(string ListId, int inv_id)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.INV_labels.Where(k => k.id == Id).SingleOrDefault();
                        var CKName = db.INV_inventories.Where(k => k.id == inv_id).SingleOrDefault().name;
                        tables.inv_id = inv_id;
                        tables.status = 1;
                        tables.check_date = DateTime.Now;
                        tables.InputDateTime= DateTime.Now;
                        int WId = tables.WorkId??0;
                        int CRM_Id = tables.CRM_contract_detail_id??0;
                        if (WId > 0)
                        {
                            var WTable = db.WIP_workorder.Where(k => k.id == WId).SingleOrDefault();
                            WTable.status = 10;//成品入库
                            WTable.closed_flag = true;//关闭工单
                        }
                        if (CRM_Id > 0)
                        {
                            var CRMTable = db.CRM_contract_detail.Where(k => k.id == CRM_Id).SingleOrDefault();
                            if (CRMTable != null)
                            {
                                CRMTable.status = 2;
                            }
                            string CustomName = tables.CRM_customers.name;
                            string PorName = tables.SYS_product.name;
                            string CRM_SN = tables.CRM_contract_detail.CRM_contract_header.SN;
                            string MailTitle = "客户："+CustomName + "；" + PorName+"产品已入库";
                            string CTel = tables.CRM_customers.tel;
                            string InvName = CKName;
                            string MailContent = "客户：" + CustomName + "；" + PorName + "产品已入库；所入仓库" + CKName + "；合同编号：" + CRM_SN + "。客户联系电话：" + CTel;

                            try
                            {
                                SendMailUse(MailTitle, MailContent);
                            }
                            catch (Exception ex) { throw new Exception(ex.Message); }
                        }
                        
                    }
                }
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
        //维修送货
        public void AddWork(WIP_WOXQModel Models)
        {
            using (var db = new XNERPEntities())
            {
                string TabName = "销售产品";
                
                var STables = db.INV_labels.Where(k => k.id == Models.id).SingleOrDefault();
                int? CRM_id =  0;
                int? WIP_id =  0;
                int? W_id =  0;
                string workorder="WO" + DateTime.Now.ToString("yyyyMMddfff");
                STables.delete_flag = true;
                CRM_id = STables.CRM_contract_detail_id;
                WIP_id = STables.WIP_contract_id;
                W_id = STables.WorkId;
                        
                if (WIP_id > 0)//判断是否是预投产品
                { workorder = "WP" + DateTime.Now.ToString("yyyyMMddfff"); TabName = "预投产品"; }

                WIP_workflow table = new WIP_workflow();
                table.wo_id = W_id ?? 0;
                table.Wood_Id = STables.wood_id;
                table.name = "维修送货";
                table.exp_begin_date = DateTime.Now;
                table.exp_end_date = DateTime.Now;
                table.act_begin_date = DateTime.Now;
                table.cost = 0;
                table.user_id = Models.user_id;
                table.user_name = Models.user_name;
                table.department_id = Models.department_id;
                table.department = Models.department;
                table.status = 0;
                table.created_time = DateTime.Now;
                table.delete_flag = false;
                table.checked_reason = "";
                table.Product_Id = STables.product_id;
                table.reserved2 = TabName;
                table.reserved3 = STables.color;
                db.WIP_workflow.Add(table);
                  
                db.SaveChanges();
            }
        }
        //返修
        public void AddReWork(LabelsModel models)
        {
            using (var db = new XNERPEntities())
            {
                int Id = models.id;
                var STables = db.INV_labels.Where(k => k.id == Id).SingleOrDefault();
                int? CRM_id = 0;
                int? WIP_id =  0;
                int? W_id = 0;
                string workorder = "WO" + DateTime.Now.ToString("yyyyMMddfff");
                STables.delete_flag = true;
                CRM_id = STables.CRM_contract_detail_id;
                WIP_id = STables.WIP_contract_id;
                W_id = STables.WorkId;
                if (WIP_id > 0)//判断是否是预投产品
                { workorder = "WP" + DateTime.Now.ToString("yyyyMMddfff"); }

                WIP_workorder WWTable = new WIP_workorder();
                WWTable.workorder = workorder;
                WWTable.CRM_contract_detail_id = CRM_id;
                WWTable.WIP_contract_id = WIP_id;
                WWTable.qty = 1;
                WWTable.status = 8;
                WWTable.closed_flag = false;
                WWTable.created_time = DateTime.Now;
                WWTable.delete_flag = false;
                WWTable.remark = models.Remark;
                db.WIP_workorder.Add(WWTable);
                
                db.SaveChanges();
            }
        }
        public DataTable ToExcel(SLabelsModel SModel)
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
                var List = (from p in db.INV_labels.Where(k => k.delete_flag == false)
                            where SModel.product_SN_id != null && SModel.product_SN_id > 0 ? SModel.product_SN_id == p.SYS_product.product_SN_id : true
                            where SModel.product_area_id != null && SModel.product_area_id > 0 ? SModel.product_area_id == p.SYS_product.product_area_id : true
                            where SModel.wood_id != null && SModel.wood_id > 0 ? SModel.wood_id == p.wood_id : true
                            where SModel.inv_id != null && SModel.inv_id > 0 ? SModel.inv_id == p.inv_id : true
                            where SModel.status != null && SModel.status >= 0 ? p.status == SModel.status : true
                            where !string.IsNullOrEmpty(SModel.productName) ? p.SYS_product.name.Contains(SModel.productName) : true
                            where SModel.ProState != null && SModel.ProState == 1 ? p.flag == 0 : SModel.ProState != null && SModel.ProState == 2 ? p.flag == 1 : SModel.ProState != null && SModel.ProState == 3 ? p.flag == 2 : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            orderby p.created_time descending
                            select new LabelsModel
                            {
                                id = p.id,
                                product_id = p.product_id,
                                ProductName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                ProductareaName = p.SYS_product.SYS_product_area.name,
                                wood_id = p.wood_id,
                                woodname = p.INV_wood_type.name,
                                inv_id = p.inv_id,
                                invname = p.INV_inventories.name,
                                status = p.status,
                                input_date = p.created_time,
                                SN = p.SN,
                                product_SN_Name = p.product_SN,
                                color = p.color,
                                flag = p.flag,
                                style = p.style,
                                volume=p.SYS_product.volume,
                                W_BZ=p.INV_wood_type.g_bz,
                                W_price=p.INV_wood_type.prcie,
                                customersName = p.CRM_customers.name,
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("标签编码", typeof(string));
                    Exceltable.Columns.Add("产品编号", typeof(string));
                    Exceltable.Columns.Add("产品名称", typeof(string));
                    Exceltable.Columns.Add("产品系列", typeof(string));
                    Exceltable.Columns.Add("产品区域", typeof(string));
                    Exceltable.Columns.Add("材质", typeof(string));
                    Exceltable.Columns.Add("色号", typeof(string));
                    Exceltable.Columns.Add("尺寸", typeof(string));
                    Exceltable.Columns.Add("所入仓库", typeof(string));
                    Exceltable.Columns.Add("进库日期", typeof(string));
                    Exceltable.Columns.Add("状态", typeof(string));
                    Exceltable.Columns.Add("所属方式", typeof(string));
                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["标签编码"] = item.SN;
                        row["产品编号"] = item.product_SN_Name;
                        row["产品名称"] = item.ProductName;
                        row["产品系列"] = item.ProductXL;
                        row["产品区域"] = item.ProductareaName;
                        row["材质"] = item.woodname;
                        row["色号"] = item.color;
                        row["尺寸"] = item.style;
                        row["所入仓库"] = item.invname;
                        row["进库日期"] = Convert.ToDateTime(item.input_date).ToString("yyyy-MM-dd"); ;
                        row["状态"] = item.status != null && item.status == 2 ? "已出库" : item.status==1 ? "已入库" : "未确认";
                        row["所属方式"] = item.flag != null && item.flag == 0 ? "销售产品" : item.flag != null && item.flag == 1 ? "预投产品" : "盘点产品";
                   
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }
        public DataTable ToCWExcel(SLabelsModel SModel)
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
                List<LabelsModel> LabelsTab = new List<LabelsModel>(); 
                var List = (from p in db.INV_labels.Where(k => k.delete_flag==false)
                            //where p.created_time<=EndTime
                            //where p.InputDateTime>=StartTime
                            //where p.InputDateTime<=EndTime
                            orderby p.created_time descending
                            select new LabelsModel
                            {
                                product_id = p.product_id,
                                ProductName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                ProductareaName = p.SYS_product.SYS_product_area.name,
                                product_area_id=p.SYS_product.SYS_product_area.id,
                                woodname = p.INV_wood_type.name,
                                invname = p.INV_inventories.name,
                                status = p.status,
                                input_date = p.created_time,
                                SN = p.SN,
                                product_SN_Name = p.product_SN,
                                color = p.color,
                                flag = p.flag,
                                style = p.style,
                                volume = p.SYS_product.volume ?? 0,
                                W_BZ = p.INV_wood_type.g_bz ?? 0,
                                W_price = p.INV_wood_type.prcie ?? 0,
                                customersName = p.CRM_customers.name,
                                PersonPrice = p.SYS_product.reserved1??0,
                                q_ccl = p.INV_wood_type.q_ccl ?? 0,
                                g_ccl = p.INV_wood_type.g_ccl??0,
                                cc_prcie = p.INV_wood_type.cc_prcie ?? 0,
                            }).ToList();
                LabelsTab = List;
                
                if (LabelsTab != null && LabelsTab.Any())
                {
                    Exceltable.Columns.Add("标签编码", typeof(string));
                    Exceltable.Columns.Add("产品编号", typeof(string));
                    Exceltable.Columns.Add("产品名称", typeof(string));
                    Exceltable.Columns.Add("产品系列", typeof(string));
                    Exceltable.Columns.Add("产品区域", typeof(string));
                    Exceltable.Columns.Add("材质", typeof(string));
                    Exceltable.Columns.Add("色号", typeof(string));
                    Exceltable.Columns.Add("尺寸", typeof(string));
                    Exceltable.Columns.Add("所入仓库", typeof(string));
                    Exceltable.Columns.Add("进库日期", typeof(string));
                    Exceltable.Columns.Add("状态", typeof(string));
                    Exceltable.Columns.Add("所属方式", typeof(string));
                    Exceltable.Columns.Add("材积", typeof(string));
                    Exceltable.Columns.Add("出材率", typeof(string));
                    Exceltable.Columns.Add("比重", typeof(string));
                    Exceltable.Columns.Add("吨", typeof(string));
                    Exceltable.Columns.Add("材料单价", typeof(string));
                    Exceltable.Columns.Add("人工成本", typeof(string));
                    Exceltable.Columns.Add("材料成本", typeof(string));
                    Exceltable.Columns.Add("辅料成本", typeof(string));
                    Exceltable.Columns.Add("总成本", typeof(string));
                    Exceltable.Columns.Add("出厂价", typeof(string));
                    Exceltable.Columns.Add("标签价", typeof(string));
                    Exceltable.Columns.Add("入库数量", typeof(string));
                    Exceltable.Columns.Add("出库数量", typeof(string));
                    Exceltable.Columns.Add("剩余数量", typeof(string));
                    foreach (var item in LabelsTab)
                    {
                        var vv = Math.Pow(10, 2);

                        var G = Convert.ToDouble(item.volume * item.W_BZ * item.W_price);
                        var GV = Convert.ToDouble(item.g_ccl);
                        var Q = Convert.ToDouble(item.volume * item.W_BZ * item.W_price);
                        var QV = Convert.ToDouble(item.q_ccl) ;
                        var G_WoodPrcie = Math.Round(G / GV * vv) / vv;
                        var Q_WoodPrcie = Math.Round(Q / QV * vv) / vv;
                        var GCost = G_WoodPrcie + Convert.ToDouble(item.PersonPrice);
                        var QCost = Q_WoodPrcie + Convert.ToDouble(item.PersonPrice);
                        var G_cost = GCost * Convert.ToDouble(item.cc_prcie);
                        var Q_cost = QCost * Convert.ToDouble(item.cc_prcie);

                        var G_CCPrice = Math.Round(G_cost / 100) * 100;
                        var G_BQPrice = G_CCPrice * 2.5;
                        var Q_CCPrice = Math.Round(Q_cost / 100) * 100;
                        var Q_BQPrice = Q_CCPrice * 2.5; ;

                        //var ccprice = G_CCPrice;
                        //var BQPrice = G_BQPrice;
                        double ccprice = 0;
                        double BQPrice = 0;
                        double CCL = 0.42;
                        if (item.product_area_id == 6)
                        {
                            CCL = 0.45;
                        }
                        
                        double Woodunit = 0;//吨，材积/出材率*比重*数量
                        double WoodCB = 0;//材料单价*吨数
                        double FLCB = 0;//辅料成本，辅料成本=材料成本*0.15
                        double CB = 0;//成本=材料成本+辅料成本+人工费
                        double ML = 0;//毛利=销售总额-成本
                        Woodunit = Convert.ToDouble(item.volume) / CCL * Convert.ToDouble(item.W_BZ);
                        WoodCB = Woodunit * Convert.ToDouble(item.W_price);
                        FLCB = WoodCB * 0.15;
                        CB = WoodCB + FLCB + Convert.ToDouble(item.PersonPrice ?? 0);
                        ML = Convert.ToDouble(item.price) - CB;
                        ccprice = CB * 1.8;
                        BQPrice = ccprice * 2.5;
                        DataRow row = Exceltable.NewRow();
                        int RKCount = 1;
                        int CKCount = 0;
                        if (item.status != null && item.status == 9)
                        {
                            CKCount = 1;
                        }
                        row["标签编码"] = item.SN;
                        row["产品编号"] = item.product_SN_Name;
                        row["产品名称"] = item.ProductName;
                        row["产品系列"] = item.ProductXL;
                        row["产品区域"] = item.ProductareaName;
                        row["材质"] = item.woodname;
                        row["色号"] = item.color;
                        row["尺寸"] = item.style;
                        row["所入仓库"] = item.invname;
                        row["进库日期"] = Convert.ToDateTime(item.input_date).ToString("yyyy-MM-dd"); ;
                        row["状态"] = item.status != null && item.status == 9 ? "已出库" : item.status == 1 ? "已入库" : "未确认";
                        row["所属方式"] = item.flag != null && item.flag == 0 ? "销售产品" : item.flag != null && item.flag == 1 ? "预投产品" : "盘点产品";
                        row["材积"] = item.volume;
                        row["出材率"] = CCL;
                        row["比重"] = item.W_BZ;
                        row["吨"] = Woodunit.ToString("0.0000");
                        row["材料单价"] = item.W_price;
                        row["人工成本"] = item.PersonPrice;
                        row["材料成本"] = WoodCB.ToString("0.00");
                        row["辅料成本"] = FLCB.ToString("0.00");
                        row["总成本"] = CB.ToString("0.00");
                        row["出厂价"] = ccprice;
                        row["标签价"] = BQPrice;
                        row["入库数量"] = RKCount;
                        row["出库数量"] = CKCount;
                        row["剩余数量"] = RKCount- CKCount;
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }
        public DataTable ToRKExcel(SLabelsModel SModel)
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
                List<LabelsModel> LabelsTab = new List<LabelsModel>();
                var List = (from p in db.INV_labels
                            where !string.IsNullOrEmpty(SModel.productName) ? p.SYS_product.name.Contains(SModel.productName) : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            orderby p.created_time descending
                            select new LabelsModel
                            {
                                product_id = p.product_id,
                                ProductName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                product_area_id = p.SYS_product.SYS_product_area.id,
                                woodname = p.INV_wood_type.name,
                                invname = p.INV_inventories.name,
                                status = p.status,
                                input_date = p.InputDateTime,
                                SN = p.SN,
                                product_SN_Name = p.product_SN,
                                color = p.color,
                                flag = p.flag,
                                style = p.style,
                                volume = p.SYS_product.volume ?? 0,
                                W_BZ = p.INV_wood_type.g_bz ?? 0,
                                W_price = p.INV_wood_type.prcie ?? 0,
                                PersonPrice = p.SYS_product.reserved1 ?? 0,
                                q_ccl = p.INV_wood_type.q_ccl ?? 0,
                                g_ccl = p.INV_wood_type.g_ccl ?? 0,
                                cc_prcie = p.INV_wood_type.cc_prcie ?? 0,
                            }).ToList();
                LabelsTab = List;
               
                if (LabelsTab != null && LabelsTab.Any())
                {
                    Exceltable.Columns.Add("标签编码", typeof(string));
                    Exceltable.Columns.Add("产品编号", typeof(string));
                    Exceltable.Columns.Add("产品名称", typeof(string));
                    Exceltable.Columns.Add("产品系列", typeof(string));
                    Exceltable.Columns.Add("产品区域", typeof(string));
                    Exceltable.Columns.Add("材质", typeof(string));
                    Exceltable.Columns.Add("色号", typeof(string));
                    Exceltable.Columns.Add("尺寸", typeof(string));
                    Exceltable.Columns.Add("所入仓库", typeof(string));
                    Exceltable.Columns.Add("进库日期", typeof(string));
                    Exceltable.Columns.Add("状态", typeof(string));
                    Exceltable.Columns.Add("所属方式", typeof(string));
                    Exceltable.Columns.Add("单位", typeof(string));
                    Exceltable.Columns.Add("数量", typeof(string));
                    Exceltable.Columns.Add("材积", typeof(string));
                    Exceltable.Columns.Add("出材率", typeof(string));
                    Exceltable.Columns.Add("比重", typeof(string));
                    Exceltable.Columns.Add("吨", typeof(string));
                    Exceltable.Columns.Add("材料单价", typeof(string));
                    Exceltable.Columns.Add("人工成本", typeof(string));
                    Exceltable.Columns.Add("材料成本", typeof(string));
                    Exceltable.Columns.Add("辅料成本", typeof(string));
                    Exceltable.Columns.Add("成本", typeof(string));
                    foreach (var item in LabelsTab)
                    {
                        double CCL = 0.42;
                        double Woodunit = 0;//吨，材积/出材率*比重*数量
                        double WoodCB = 0;//材料单价*吨数
                        double FLCB = 0;//辅料成本，辅料成本=材料成本*0.15
                        double CB = 0;//成本=材料成本+辅料成本+人工费
                        if (item.product_area_id == 6)
                        { CCL = 0.45; }
                        Woodunit = Convert.ToDouble(item.volume) / CCL * Convert.ToDouble(item.W_BZ);
                        WoodCB = Woodunit * Convert.ToDouble(item.W_price);
                        FLCB = WoodCB * 0.15;
                        CB = WoodCB + FLCB + Convert.ToDouble(item.PersonPrice ?? 0);
                        DataRow row = Exceltable.NewRow();
                        
                        row["标签编码"] = item.SN;
                        row["产品编号"] = item.product_SN_Name;
                        row["产品名称"] = item.ProductName;
                        row["产品系列"] = item.ProductXL;
                        row["产品区域"] = item.ProductareaName;
                        row["材质"] = item.woodname;
                        row["色号"] = item.color;
                        row["尺寸"] = item.style;
                        row["所入仓库"] = item.invname;
                        row["进库日期"] = Convert.ToDateTime(item.input_date).ToString("yyyy-MM-dd"); ;
                        row["状态"] = item.status != null && item.status == 9 ? "已出库" : item.status == 1 ? "已入库" : "未确认";
                        row["所属方式"] = item.flag != null && item.flag == 0 ? "销售产品" : item.flag != null && item.flag == 1 ? "预投产品" : "盘点产品";
                        row["单位"] = "件";
                        row["数量"] = "1";
                        row["材积"] = item.volume;
                        row["出材率"] = CCL;
                        row["比重"] = item.W_BZ;
                        row["吨"] = Woodunit.ToString("0.0000");
                        row["材料单价"] = item.W_price;
                        row["人工成本"] = item.PersonPrice;
                        row["材料成本"] = WoodCB.ToString("0.00");
                        row["辅料成本"] = FLCB.ToString("0.00");
                        row["成本"] = CB.ToString("0.00");
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }
        public DataTable ToDeliveryExcelOut(SLabelsModel SModel)
        {
            DataTable Exceltable = new DataTable();
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
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_delivery_tmp_header
                            where !string.IsNullOrEmpty(SModel.SaleName) ? p.INV_labels.CRM_customers.name.Contains(SModel.SaleName) : true
                            where !string.IsNullOrEmpty(SModel.ProSN) ? p.CRM_contract_header.SN.Contains(SModel.ProSN) : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            orderby p.created_time descending
                            select new LabelsModel
                            {
                                id = p.id,
                                CRM_SN = p.CRM_contract_header.SN,
                                CRM_HTId = p.contract_header_id,
                                ProductName = p.INV_labels.SYS_product.name,
                                ProductXL = p.INV_labels.SYS_product.SYS_product_SN.name,
                                product_area_id = p.CRM_contract_detail.SYS_product.product_area_id,
                                wood_id = p.INV_labels.wood_id,
                                woodname = p.INV_labels.INV_wood_type.name,
                                inv_id = p.INV_labels.inv_id,
                                invname = p.INV_labels.INV_inventories.name,
                                input_date = p.created_time,
                                color = p.INV_labels.color,
                                style = p.INV_labels.style,
                                customersName = p.CRM_contract_header.CRM_customers.name,
                                qty = p.CRM_contract_detail.qty,
                                price = p.CRM_contract_detail.price,
                                delete_flag = p.delete_flag,
                                OrderNum = p.OrderNum,
                                volume = p.CRM_contract_detail.SYS_product.volume,
                                W_BZ = p.CRM_contract_detail.INV_wood_type.g_bz,
                                W_price = p.CRM_contract_detail.INV_wood_type.prcie,
                                PersonPrice = p.CRM_contract_detail.SYS_product.reserved1,
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("客户名称", typeof(string));
                    Exceltable.Columns.Add("合同编号", typeof(string));
                    Exceltable.Columns.Add("送货日期", typeof(string));
                    Exceltable.Columns.Add("送货单号", typeof(string));
                    Exceltable.Columns.Add("产品名称", typeof(string));
                    Exceltable.Columns.Add("材质", typeof(string));
                    Exceltable.Columns.Add("色号", typeof(string));
                    Exceltable.Columns.Add("尺寸", typeof(string));
                    Exceltable.Columns.Add("单位", typeof(string));
                    Exceltable.Columns.Add("数量", typeof(string));
                    Exceltable.Columns.Add("单价", typeof(string));
                    Exceltable.Columns.Add("总价", typeof(string));
                    Exceltable.Columns.Add("材积", typeof(string));
                    Exceltable.Columns.Add("出材率", typeof(string));
                    Exceltable.Columns.Add("比重", typeof(string));
                    Exceltable.Columns.Add("吨", typeof(string));
                    Exceltable.Columns.Add("材料单价", typeof(string));
                    Exceltable.Columns.Add("人工成本", typeof(string));
                    Exceltable.Columns.Add("材料成本", typeof(string));
                    Exceltable.Columns.Add("辅料成本", typeof(string));
                    Exceltable.Columns.Add("总成本", typeof(string));
                    Exceltable.Columns.Add("毛利", typeof(string));
                    foreach (var item in List)
                    {
                        double CCL = 0.42;
                        double Woodunit = 0;//吨，材积/出材率*比重*数量
                        double WoodCB = 0;//材料单价*吨数
                        double FLCB = 0;//辅料成本，辅料成本=材料成本*0.15
                        double CB= 0;//成本=材料成本+辅料成本+人工费
                        double ML = 0;//毛利=销售总额-成本
                        if (item.product_area_id == 6)
                        { CCL = 0.45; }
                        Woodunit = Convert.ToDouble(item.volume) / CCL * Convert.ToDouble(item.W_BZ);
                        WoodCB = Woodunit * Convert.ToDouble(item.W_price);
                        FLCB = WoodCB * 0.15;
                        CB = WoodCB + FLCB + Convert.ToDouble(item.PersonPrice??0);
                        ML = Convert.ToDouble(item.price) - CB;
                        DataRow row = Exceltable.NewRow();
                        row["客户名称"] = item.customersName;
                        row["合同编号"] = item.CRM_SN;
                        row["送货日期"] = Convert.ToDateTime(item.input_date).ToString("yyyy-MM-dd");
                        row["送货单号"] = item.OrderNum;
                        row["产品名称"] = item.ProductXL + "_" + item.ProductName;
                        row["材质"] = item.woodname;
                        row["色号"] = item.color;
                        row["尺寸"] = item.style;
                        row["单位"] = "件";
                        row["数量"] = "1";
                        row["单价"] = item.price;
                        row["总价"] = item.price;
                        row["材积"] = item.volume;
                        row["出材率"] = CCL;
                        row["比重"] = item.W_BZ;
                        row["吨"] = Woodunit.ToString("0.0000");
                        row["材料单价"] = item.W_price;
                        row["人工成本"] = item.PersonPrice;
                        row["材料成本"] = WoodCB.ToString("0.00");
                        row["辅料成本"] = FLCB.ToString("0.00");
                        row["总成本"] = CB.ToString("0.00");
                        row["毛利"] = ML.ToString("0.00");
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }
        public static bool SendMailUse(string MailTitle, string MailContent)
        {
            string host = "smtp.qq.com";// 邮件服务器smtp.163.com表示网易邮箱服务器    
            string userName = "2410792329@qq.com";// 发送端账号   
            string password = "lxwyamcaotzrdifg";// 发送端密码(这个客户端重置后的密码)


            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式    
            client.Host = host;//邮件服务器
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(userName, password);//用户名、密码

            //////////////////////////////////////
            string strfrom = userName;
            string strto = "13764923@qq.com";
            //string strcc = "2410792329@qq.com";//抄送


            string subject = MailTitle;//邮件的主题             
            string body = MailContent;//发送的邮件正文  

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(strfrom, "香凝红木");
            msg.To.Add(strto);
            //msg.CC.Add(strcc);

            msg.Subject = subject;//邮件标题   
            msg.Body = body;//邮件内容   
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
            msg.IsBodyHtml = true;//是否是HTML邮件   
            msg.Priority = MailPriority.High;//邮件优先级   


            try
            {
                client.Send(msg);
                return true;
                //Console.WriteLine("发送成功");
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return false;
                //Console.WriteLine(ex.Message, "发送邮件出错");
            }
        }
        //打印送货单
        public CRM_HTZModel PrintDelivery(string ListId)
        {
            CRM_HTZModel Models = new CRM_HTZModel();
            using (var db = new XNERPEntities())
            {
                List<DeliveryModel> ListD = new List<DeliveryModel>();
                string[] ArrId = ListId.Split('$');
                List<string> list = new List<string>();
                for (int i = 0; i < ArrId.Length; i++)//遍历数组成员
                {
                    if (list.IndexOf(ArrId[i].ToLower()) == -1)//对每个成员做一次新数组查询如果没有相等的则加到新数组
                        list.Add(ArrId[i]);

                }
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.CRM_delivery_tmp_header.Where(k => k.id == Id).SingleOrDefault();

                        int HeadId = tables.contract_header_id;
                        int DetailId = tables.contract_detail_id;

                        Models.customer = tables.CRM_contract_header.Linkman;
                        Models.delivery_address = tables.CRM_contract_header.delivery_address;
                        Models.SN = tables.CRM_contract_header.SN;
                        Models.TelPhone = tables.CRM_contract_header.Linktel;
                        Models.OrderMun = tables.OrderNum;
                        Models.signed_user_id=tables.contract_detail_id;
                        Models.delivery_date = tables.DeliverTime;

                        var OldCount = ListD.Where(k => k.HeadId == HeadId && k.DetailId == DetailId).SingleOrDefault();

                        if (OldCount==null)

                        {
                            DeliveryModel DeModel = new DeliveryModel();
                            DeModel.productName = tables.CRM_contract_detail.SYS_product.name;
                            DeModel.productXL = tables.CRM_contract_detail.SYS_product.SYS_product_SN.name;
                            DeModel.woodName = tables.CRM_contract_detail.INV_wood_type.name;
                            DeModel.length = tables.CRM_contract_detail.length;
                            DeModel.width = tables.CRM_contract_detail.width;
                            DeModel.height = tables.CRM_contract_detail.height;
                            DeModel.qty = tables.CRM_contract_detail.qty;
                            DeModel.HeadId = HeadId;
                            DeModel.DetailId = DetailId;
                            ListD.Add(DeModel);
                        }
                    }
                }
                Models.DePro = ListD;
            }
            return Models;
        }
    }
}
