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
    public class WIP_WODal
    {
        private static readonly UserDal UDal = new UserDal();
        public PagedList<WIP_WOModel> GetPageList(SWIP_WOModel SModel, int PageIndex, int PageSize)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.WIP_workorder.Where(k => k.delete_flag == false && k.closed_flag == false && k.status >= 0 && k.status < 9 && k.CRM_contract_detail_id > 0 && k.CRM_contract_detail.delete_flag==false)
                            where !string.IsNullOrEmpty(SModel.HTSN) ? p.CRM_contract_detail.CRM_contract_header.SN==SModel.HTSN : true
                            where SModel.status != null && SModel.status>0? p.status == SModel.status : true
                            where !string.IsNullOrEmpty(SModel.SaleName) ? p.CRM_contract_detail.CRM_contract_header.CRM_customers.name.Contains(SModel.SaleName) : true
                            orderby p.created_time descending
                            select new WIP_WOModel
                            {
                                id = p.id,
                                workorder = p.workorder,
                                SaleName=p.CRM_contract_detail.CRM_contract_header.CRM_customers.name,
                                HTSN = p.CRM_contract_detail.CRM_contract_header.SN,
                                ProductName = p.CRM_contract_detail.SYS_product.name,
                                ProductXL = p.CRM_contract_detail.SYS_product.SYS_product_SN.name,
                                HTId = p.CRM_contract_detail.header_id,
                                JHDateTime = p.CRM_contract_detail.CRM_contract_header.delivery_date,
                                length=p.CRM_contract_detail.length,
                                width = p.CRM_contract_detail.width,
                                height = p.CRM_contract_detail.height,
                                qty = p.qty,
                                status = p.status,
                                remark = p.remark,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public PagedList<WIP_WOModel> GetYTPageList(SWIP_WOModel SModel, int PageIndex, int PageSize)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.WIP_workorder.Where(k => k.delete_flag == false && k.closed_flag == false && k.status >=0 && k.status < 9 && k.WIP_contract_id>0)
                            where SModel.status != null && SModel.status > 0 ? p.status == SModel.status : true
                            where SModel.product_SN_id != null && SModel.product_SN_id > 0 ? p.WIP_contract.SYS_product.product_SN_id == SModel.product_SN_id : true
                            orderby p.created_time descending
                            select new WIP_WOModel
                            {
                                id = p.id,
                                workorder = p.workorder,
                                WoodName=p.WIP_contract.INV_wood_type.name,
                                //HTSN = p.CRM_contract_detail.CRM_contract_header.SN,
                                ProductName = p.WIP_contract.SYS_product.name,
                                ProductXL = p.WIP_contract.SYS_product.SYS_product_SN.name,
                                Color = p.WIP_contract.color,
                                //JHDateTime = p.CRM_contract_detail.CRM_contract_header.delivery_date,
                                length = p.WIP_contract.length,
                                width = p.WIP_contract.width,
                                height = p.WIP_contract.height,
                                qty = p.qty,
                                status = p.status,
                                remark = p.remark,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public PagedList<WIP_WOXQModel> GetFlowPageList(SWIP_WOXQModel SModel, int PageIndex, int PageSize)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.WIP_workflow.Where(k => k.delete_flag == false && k.WIP_workorder.closed_flag == false)
                            where !string.IsNullOrEmpty(SModel.NavName) ? p.name==SModel.NavName : true
                            where !string.IsNullOrEmpty(SModel.HTSN) ? p.WIP_workorder.CRM_contract_detail.CRM_contract_header.SN == SModel.HTSN : true
                            where SModel.status != null ? p.status == SModel.status : true
                            orderby p.created_time descending
                            select new WIP_WOXQModel
                            {
                                id = p.id,
                                Name = p.name,
                                workorder = p.WIP_workorder.workorder,
                                ProductName = p.SYS_product.name,
                                customer = p.WIP_workorder.CRM_contract_detail.CRM_contract_header.CRM_customers.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                user_name = p.user_name,
                                exp_begin_date = p.exp_begin_date,
                                exp_end_date = p.exp_end_date,
                                act_begin_date = p.act_begin_date,
                                act_end_date = p.act_end_date,
                                status=p.status,
                                checked_user_name = p.checked_user_name,
                                cost = p.cost,
                                remark=p.checked_reason,
                                source = p.reserved2
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public PagedList<WIP_WOXQModel> GetHRFlowPageList(SWIP_WOXQModel SModel, int PageIndex, int PageSize)
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
                var List = (from p in db.WIP_workflow.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.NavName) ? p.name == SModel.NavName : true
                            where !string.IsNullOrEmpty(SModel.HTSN) ? p.WIP_workorder.CRM_contract_detail.CRM_contract_header.SN == SModel.HTSN : true
                            where SModel.status != null ? p.status == SModel.status : true
                            where p.act_end_date > StartTime
                            where p.act_end_date < EndTime
                            orderby p.created_time descending
                            select new WIP_WOXQModel
                            {
                                id = p.id,
                                Name = p.name,
                                workorder = p.WIP_workorder.workorder,
                                ProductName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                user_name = p.user_name,
                                exp_begin_date = p.exp_begin_date,
                                exp_end_date = p.exp_end_date,
                                act_begin_date = p.act_begin_date,
                                act_end_date = p.act_end_date,
                                status = p.status,
                                checked_user_name = p.checked_user_name,
                                cost = p.cost,
                                remark = p.checked_reason,
                                source = p.reserved2
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public void AddOrUpdate(WIP_WOXQModel Models,out int NavNum)
        {
            using (var db = new XNERPEntities())
            {
                var WTable = db.WIP_workorder.Where(k => k.id == Models.wo_id).SingleOrDefault();
                string TabName = "销售产品";
                int ProId=0;
                int WoodId = 0;
                string Color = "";
                if (WTable.CRM_contract_detail != null && WTable.CRM_contract_detail.product_id > 0)
                {
                    ProId = WTable.CRM_contract_detail.product_id;
                    WoodId = WTable.CRM_contract_detail.wood_type_id;
                    Color = WTable.CRM_contract_detail.color;
                    NavNum = 1;
                }
                else
                {
                    ProId = WTable.WIP_contract.product_id;
                    WoodId = WTable.WIP_contract.wood_id;
                    Color = WTable.WIP_contract.color;
                    TabName = "预投产品";
                    NavNum = 0;
                }

                WIP_workflow table = new WIP_workflow();
                table.wo_id = Models.wo_id;
                table.Wood_Id = WoodId;
                table.name = Models.Job;
                table.exp_begin_date = Models.exp_begin_date;
                table.exp_end_date = Models.exp_end_date;
                table.act_begin_date = Models.exp_begin_date;
                table.cost = Models.cost;
                table.user_id = Models.user_id;
                table.user_name = Models.user_name;
                table.department_id = Models.department_id;
                table.department = Models.department;
                table.status = 0;
                table.created_time = DateTime.Now;
                table.delete_flag = false;
                table.checked_reason = Models.remark;
                table.Product_Id = ProId;
                table.reserved2 = TabName;
                table.reserved3 = Color;
                db.WIP_workflow.Add(table);

                WIP_Even Emodels = new WIP_Even();
                Emodels.name = Models.Job;
                Emodels.user_id = new UserDal().GetCurrentUserName().UserId;
                Emodels.user_name = new UserDal().GetCurrentUserName().UserName;
                Emodels.wo_id = Models.wo_id;
                Emodels.event_log = Emodels.user_name + ",安排" +Models.user_name+","+ Models.Job + "任务";
                Emodels.remark = Models.remark;

                AddWorkOrderEven(Emodels, db);

                db.SaveChanges();
            }
        }
        public void WorkOrderMore(WIP_WOXQModel Models,string ListId, out int NavNum)
        {
            int Navtab = 1;
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        
                            var WTable = db.WIP_workorder.Where(k => k.id == Id).SingleOrDefault();
                            string TabName = "销售产品";
                            int ProId = 0;
                            int WoodId = 0;
                            string Color = "";
                            if (WTable.CRM_contract_detail != null && WTable.CRM_contract_detail.product_id > 0)
                            {
                                ProId = WTable.CRM_contract_detail.product_id;
                                WoodId = WTable.CRM_contract_detail.wood_type_id;
                                Color = WTable.CRM_contract_detail.color;
                            Navtab = 1;
                        }
                            else
                            {
                                ProId = WTable.WIP_contract.product_id;
                                WoodId = WTable.WIP_contract.wood_id;
                                Color = WTable.WIP_contract.color;
                                TabName = "预投产品";
                            Navtab = 0;
                        }if (!string.IsNullOrEmpty(WTable.reserved3))
                        {
                            TabName = WTable.reserved3;
                        }

                            WIP_workflow table = new WIP_workflow();
                            table.wo_id = Id;
                            table.Wood_Id = WoodId;
                            table.name = Models.Job;
                            table.exp_begin_date = Models.exp_begin_date;
                            table.exp_end_date = Models.exp_end_date;
                            table.act_begin_date = Models.exp_begin_date;
                            table.cost = Models.cost;
                            table.user_id = Models.user_id;
                            table.user_name = Models.user_name;
                            table.department_id = Models.department_id;
                            table.department = Models.department;
                            table.status = 0;
                            table.created_time = DateTime.Now;
                            table.delete_flag = false;
                            table.checked_reason = Models.remark;
                            table.Product_Id = ProId;
                            table.reserved2 = TabName;
                            table.reserved3 = Color;
                            db.WIP_workflow.Add(table);

                            WIP_Even Emodels = new WIP_Even();
                            Emodels.name = Models.Job;
                            Emodels.user_id = new UserDal().GetCurrentUserName().UserId;
                            Emodels.user_name = new UserDal().GetCurrentUserName().UserName;
                            Emodels.wo_id = Id;
                            Emodels.event_log = Emodels.user_name + ",安排" + Models.user_name + "," + Models.Job + "任务";
                            Emodels.remark = Models.remark;

                            AddWorkOrderEven(Emodels, db);

                          
                    }
                }
                NavNum = Navtab;
                db.SaveChanges();
            }
        }
        public WIP_WOXQModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.WIP_workflow.Where(k => k.id == Id)
                              select new WIP_WOXQModel
                              {
                                  id = p.id,
                                  exp_begin_date = p.exp_begin_date,
                                  exp_end_date = p.exp_end_date,
                                  cost = p.cost,
                                  user_id = p.user_id,
                                  user_name = p.user_name,
                                  department_id = p.department_id,
                                  department = p.department,
                                  remark=p.checked_reason
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void Checked(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.WIP_workorder.Where(k => k.id == Id).SingleOrDefault();
                tables.status = 1;
                db.SaveChanges();
            }
        }
        public void AddWorkOrderEven(WIP_Even models,XNERPEntities db)
        {
            var tables = new WIP_wo_events();
            tables.id = Guid.NewGuid();
            tables.name = models.name;
            tables.wo_id = models.wo_id;
            tables.user_id = models.user_id;
            tables.user_name = models.user_name;
            tables.event_log = models.event_log;
            tables.remark = models.remark;
            tables.created_time = DateTime.Now;
            db.WIP_wo_events.Add(tables);
        }
        public List<WIP_Even> WIP_EvenList(int WoId)
        {
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.WIP_wo_events.Where(k => k.wo_id == WoId)
                            orderby p.created_time descending
                            select new WIP_Even
                            {
                                name = p.name,
                                user_id = p.user_id,
                                user_name = p.user_name,
                                event_log = p.event_log,
                                remark = p.remark,
                                created_time = p.created_time
                            }).ToList();
                return List;
            }
        }
        //提交任务审核
        public void Checked(string ListId, int status)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.WIP_workflow.Where(k => k.id == Id).SingleOrDefault();
                        tables.status = status;
                        int ProductId = tables.Product_Id ?? 0;
                        int WoodId = tables.Wood_Id ?? 0;
                        if (status == 1)//提交任务，更改实际完成时间
                        {
                            tables.act_end_date = DateTime.Now;
                        }
                        if (status == 2)//审核通过
                        {
                            tables.checked_date = DateTime.Now;
                            tables.checked_user_id = UDal.GetCurrentUserName().UserId;
                            tables.checked_user_name = UDal.GetCurrentUserName().UserName;

                            int wId = tables.wo_id;
                            var WTable = db.WIP_workorder.Where(m => m.id == wId).SingleOrDefault();
                            int? CRM_Id = WTable.CRM_contract_detail_id ?? 0;
                            int? WIP_Id = WTable.WIP_contract_id ?? 0;
                            int CustomersId = 0;
                            int length = 0;
                            int width = 0;
                            int height = 0;
                            int flag = 0;
                            string ProductSN = tables.SYS_product.SYS_product_SN.SN;
                            string ProductNum = "";

                            if (CRM_Id > 0)
                            {
                                length = Convert.ToInt32(WTable.CRM_contract_detail.length);
                                width = Convert.ToInt32(WTable.CRM_contract_detail.width);
                                height = Convert.ToInt32(WTable.CRM_contract_detail.height);
                                CustomersId = WTable.CRM_contract_detail.CRM_contract_header.CRM_customers.id;
                            }
                            else
                            {
                                length = Convert.ToInt32(WTable.WIP_contract.length);
                                width = Convert.ToInt32(WTable.WIP_contract.width);
                                height = Convert.ToInt32(WTable.WIP_contract.height);
                                height = Convert.ToInt32(WTable.WIP_contract.height);
                                flag = 1;
                            }

                            //添加生产成本
                            if (tables.cost > 0)
                            {
                                WIP_WO_salary WSTable = new WIP_WO_salary();
                                WSTable.user_id = tables.user_id;
                                WSTable.user_name = tables.user_name;
                                WSTable.price = tables.cost;
                                WSTable.qty = WTable.qty;
                                WSTable.amount = tables.cost * WTable.qty;
                                WSTable.SN = tables.name;
                                WSTable.check_user_id = tables.checked_user_id;
                                WSTable.check_user_name = tables.checked_user_name;
                                WSTable.status = 0;
                                WSTable.created_time = DateTime.Now;
                                WSTable.delete_flag = false;
                                db.WIP_WO_salary.Add(WSTable);
                            }
                            if (tables.name.Contains("开料"))
                            { WTable.status = 3; }
                            if (tables.name.Contains("雕花"))
                            { WTable.status = 4; }
                            if (tables.name.Contains("木工后段"))//半成品入库
                            {
                                WTable.status = 5;
                                int? SW_id = db.INV_semi.Where(k => k.Work_id == WTable.id).Count();//判断是否已经入库，防止多次入库
                                if (SW_id <= 0)
                                {
                                    int WCount = WTable.qty;
                                    if (WCount > 1)
                                    {
                                        for (int i = 0; i < WCount; i++)
                                        {
                                            INV_semi INTable = new INV_semi();
                                            INTable.product_id = ProductId;
                                            INTable.wood_id = WoodId;
                                            INTable.color = tables.reserved3;
                                            INTable.status = 0;
                                            INTable.created_time = DateTime.Now;
                                            INTable.delete_flag = false;
                                            INTable.CRM_id = CRM_Id;
                                            INTable.Work_id = WTable.id;
                                            INTable.WIP_id = WIP_Id;
                                            INTable.length = length;
                                            INTable.width = width;
                                            INTable.height = height;
                                            db.INV_semi.Add(INTable);
                                        }
                                    }
                                    else
                                    {
                                        INV_semi INTable = new INV_semi();
                                        INTable.product_id = ProductId;
                                        INTable.wood_id = WoodId;
                                        INTable.color = tables.reserved3;
                                        INTable.status = 0;
                                        INTable.created_time = DateTime.Now;
                                        INTable.delete_flag = false;
                                        INTable.CRM_id = CRM_Id;
                                        INTable.WIP_id = WIP_Id;
                                        INTable.Work_id = WTable.id;
                                        INTable.length = length;
                                        INTable.width = width;
                                        INTable.height = height;
                                        db.INV_semi.Add(INTable);
                                    }
                                }
                            }
                            if (tables.name.Contains("刮磨"))
                            { WTable.status = 6; }
                            if (tables.name.Contains("油漆"))
                            { WTable.status = 7; }
                            if (tables.name.Contains("配件安装"))//到这里，成品入库
                            {
                                WTable.status = 8;
                                int? SW_id = db.INV_semi.Where(k => k.Work_id == WTable.id).Count();//判断是否已经入库，防止多次入库
                                if (SW_id <= 0)
                                {
                                    Random r = new Random();
                                    int WCount = WTable.qty;
                                    if (WCount > 1)
                                    {
                                        for (int i = 0; i < WCount; i++)
                                        {
                                            if (!string.IsNullOrEmpty(ProductSN))
                                            {
                                                ProductNum = "XN" + ProductSN.Substring(0, 2) + DateTime.Now.ToString("MMdd") + r.Next(10000, 100000);
                                            }
                                            INV_labels INTable = new INV_labels();
                                            INTable.SN = "LA" + DateTime.Now.ToString("yyyyMMdd") + r.Next(100000, 1000000);
                                            INTable.product_SN = ProductNum;
                                            INTable.product_id = ProductId;
                                            INTable.style = length + "×" + width + "×" + height;
                                            INTable.wood_id = WoodId;
                                            INTable.color = tables.reserved3;
                                            INTable.customer_id = CustomersId;
                                            INTable.company = "上海香凝工艺品有限公司";
                                            INTable.address = "上海市青浦区朱家角朱枫公路1355号";
                                            INTable.website = "www.xiangninghm.com";
                                            INTable.status = 0;
                                            INTable.CRM_contract_detail_id = CRM_Id;
                                            INTable.flag = flag;
                                            INTable.created_time = DateTime.Now;
                                            INTable.delete_flag = false;
                                            INTable.WorkId = WTable.id;
                                            INTable.WIP_contract_id = WIP_Id;
                                            db.INV_labels.Add(INTable);
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(ProductSN))
                                        {
                                            ProductNum = "XN" + DateTime.Now.ToString("MMdd") + r.Next(10000, 100000);
                                        }
                                        INV_labels INTable = new INV_labels();
                                        INTable.SN = "LA" + DateTime.Now.ToString("yyyyMMdd") + r.Next(100000, 1000000);
                                        INTable.product_SN = ProductNum;
                                        INTable.product_id = ProductId;
                                        INTable.style = length + "×" + width + "×" + height;
                                        INTable.wood_id = WoodId;
                                        INTable.color = tables.reserved3;
                                        INTable.customer_id = CustomersId;
                                        INTable.company = "上海香凝工艺品有限公司";
                                        INTable.address = "上海市青浦区朱家角朱枫公路1355号";
                                        INTable.website = "www.xiangninghm.com";
                                        INTable.status = 0;
                                        INTable.CRM_contract_detail_id = CRM_Id;
                                        INTable.flag = flag;
                                        INTable.created_time = DateTime.Now;
                                        INTable.delete_flag = false;
                                        INTable.WorkId = WTable.id;
                                        INTable.WIP_contract_id = WIP_Id;
                                        db.INV_labels.Add(INTable);
                                    }
                                }
                            }
                        }
                    }
                }
                db.SaveChanges();
            }
        }
        //销售产品开工单
        public void AddSaleWorkOrder(string ListId)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        WIP_workorder WWTable = new WIP_workorder();
                        string workorder = "WO" + DateTime.Now.ToString("yyyyMMddfff");
                        byte status = 0;
                        int LabelseCount = 0;
                        int Qty = 0;
                        var tabls = db.CRM_contract_detail.Where(k => k.id == Id).SingleOrDefault();
                        tabls.status = 1;
                        LabelseCount = tabls.LabelsCount ?? 0;
                        Qty = tabls.qty;
                        if (!string.IsNullOrEmpty(tabls.SYS_product.BOM_path) && !string.IsNullOrEmpty(tabls.SYS_product.paper_path))
                        {
                            WWTable.BOM_over_date = DateTime.Now;
                            WWTable.BOM_ready_date = DateTime.Now;
                            WWTable.BOM_path = tabls.SYS_product.BOM_path;
                            WWTable.paper_path = tabls.SYS_product.paper_path;
                            status = 2;
                        }
                        WWTable.workorder = workorder;
                        WWTable.CRM_contract_detail_id = Id;
                        WWTable.WIP_contract_id = 0;
                        WWTable.qty = Qty - LabelseCount;
                        WWTable.status = status;
                        WWTable.closed_flag = false;
                        WWTable.created_time = DateTime.Now;
                        WWTable.delete_flag = false;
                        WWTable.remark = "";
                        db.WIP_workorder.Add(WWTable);

                        WIP_work_CW WCTable = new WIP_work_CW();
                        WCTable.workorder = workorder;
                        WCTable.CRM_contract_detail_id = Id;
                        WCTable.WIP_contract_id = 0;
                        WCTable.qty = Qty - LabelseCount;
                        WCTable.product_id = tabls.product_id;
                        WCTable.stye = tabls.length + "x" + tabls.width + "x" + tabls.height;
                        WCTable.CreateTime = DateTime.Now;
                        WCTable.GMqty = 0;
                        WCTable.YQqty = 0;
                        WCTable.WoodName = tabls.INV_wood_type.name;
                        WCTable.unit = "件";
                        db.WIP_work_CW.Add(WCTable);
                    }
                }
                db.SaveChanges();
            }
        }
        //预投产品添加
        public void AddYTWorkOrder(YTWorkOrder Models)
        {
            using (var db = new XNERPEntities())
            {
                var YTTable = new WIP_contract();
                YTTable.product_id = Models.product_id;
                YTTable.wood_id = Models.wood_id;
                YTTable.color = Models.color;
                YTTable.custom_flag = false;
                YTTable.length = Models.length;
                YTTable.width = Models.width;
                YTTable.height = Models.height;
                YTTable.created_time = DateTime.Now;
                YTTable.delete_flag = false;
                YTTable.qty = Models.qty;
                db.WIP_contract.Add(YTTable);
                db.SaveChanges();

                int Id = YTTable.id;
                byte status = 0;
                WIP_workorder WWTable = new WIP_workorder();
                WWTable.workorder = "WP" + DateTime.Now.ToString("yyyyMMddfff");
                var PTables = db.SYS_product.Where(k => k.id == Models.product_id).SingleOrDefault();
                if (!string.IsNullOrEmpty(PTables.BOM_path) && !string.IsNullOrEmpty(PTables.paper_path))
                {
                    WWTable.BOM_over_date = DateTime.Now;
                    WWTable.BOM_ready_date = DateTime.Now;
                    WWTable.BOM_path = YTTable.SYS_product.BOM_path;
                    WWTable.paper_path = YTTable.SYS_product.paper_path;
                    status = 2;
                }
                WWTable.CRM_contract_detail_id = 0;
                WWTable.WIP_contract_id = Id;
                WWTable.qty = Models.qty;
                WWTable.status = status;
                WWTable.closed_flag = false;
                WWTable.created_time = DateTime.Now;
                WWTable.delete_flag = false;
                WWTable.remark = "";
                db.WIP_workorder.Add(WWTable);

                WIP_work_CW WCTable = new WIP_work_CW();
                WCTable.workorder = WWTable.workorder;
                WCTable.CRM_contract_detail_id = 0;
                WCTable.WIP_contract_id = Id;
                WCTable.qty = Models.qty;
                WCTable.product_id = Models.product_id;
                WCTable.stye = Models.length + "x" + Models.width + "x" + Models.height;
                WCTable.CreateTime = DateTime.Now;
                WCTable.GMqty = 0;
                WCTable.YQqty = 0;
                WCTable.WoodName = Models.woodName;
                WCTable.unit = "件";
                db.WIP_work_CW.Add(WCTable);

                db.SaveChanges();
            }
        }

        public DataTable ToExcel(SWIP_WOXQModel SModel)
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
                var List = (from p in db.WIP_workflow.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.NavName) ? p.name == SModel.NavName : true
                            where !string.IsNullOrEmpty(SModel.HTSN) ? p.WIP_workorder.CRM_contract_detail.CRM_contract_header.SN == SModel.HTSN : true
                            where SModel.status != null ? p.status == SModel.status : true
                            where p.act_end_date > StartTime
                            where p.act_end_date < EndTime
                            orderby p.created_time descending
                            select new WIP_WOXQModel
                            {
                                id = p.id,
                                Name = p.name,
                                workorder = p.WIP_workorder.workorder,
                                ProductName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                user_name = p.user_name,
                                exp_begin_date = p.exp_begin_date,
                                exp_end_date = p.exp_end_date,
                                act_begin_date = p.act_begin_date,
                                act_end_date = p.act_end_date,
                                status = p.status,
                                checked_user_name = p.checked_user_name,
                                cost = p.cost,
                                remark = p.checked_reason,
                                WoodName=p.INV_wood_type.name,
                                source = p.reserved2
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("产品名称", typeof(string));
                    Exceltable.Columns.Add("材质", typeof(string));
                    Exceltable.Columns.Add("生产价格", typeof(string));
                    Exceltable.Columns.Add("生产状态", typeof(string));
                    Exceltable.Columns.Add("生产开始时间", typeof(string));
                    Exceltable.Columns.Add("生产完成时间", typeof(string));
                    Exceltable.Columns.Add("接单人", typeof(string));
                    Exceltable.Columns.Add("审核人", typeof(string));
                    Exceltable.Columns.Add("来源", typeof(string));

                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["产品名称"] = item.ProductXL + "_" + item.ProductName;
                        row["材质"] = item.WoodName;
                        row["生产价格"] = item.cost;
                        row["生产状态"] = item.status!=null && item.status==0?"生产中":item.status==1?"生产完成，待审核":item.status==2?"审核通过":"被驳回";
                        row["生产开始时间"] = Convert.ToDateTime(item.act_begin_date).ToString("yyyy-MM-dd");
                        row["生产完成时间"] = Convert.ToDateTime(item.act_end_date).ToString("yyyy-MM-dd");
                        row["接单人"] = item.user_name;
                        row["审核人"] = item.checked_user_name;
                        row["来源"] = item.source;
                        
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;

        }
        public DataTable ToWorkExcel()
        {
            DataTable Exceltable = new DataTable();
            
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.WIP_workflow.Where(k => k.delete_flag == false && k.WIP_workorder.closed_flag == false)
                        orderby p.created_time descending
                        select new WIP_WOXQModel
                        {
                            id = p.id,
                            Name = p.name,
                            workorder = p.WIP_workorder.workorder,
                            ProductName = p.SYS_product.name,
                            customer = p.WIP_workorder.CRM_contract_detail.CRM_contract_header.CRM_customers.name,
                            ProductXL = p.SYS_product.SYS_product_SN.name,
                            user_name = p.user_name,
                            exp_begin_date = p.exp_begin_date,
                            exp_end_date = p.exp_end_date,
                            act_begin_date = p.act_begin_date,
                            act_end_date = p.act_end_date,
                            status = p.status,
                            checked_user_name = p.checked_user_name,
                            cost = p.cost,
                            remark = p.checked_reason,
                            source = p.reserved2
                        }).ToList();
            if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("产品名称", typeof(string));
                    Exceltable.Columns.Add("生产工序", typeof(string));
                    Exceltable.Columns.Add("生产价格", typeof(string));
                    Exceltable.Columns.Add("生产状态", typeof(string));
                    Exceltable.Columns.Add("生产开始时间", typeof(string));
                    Exceltable.Columns.Add("生产预计完成时间", typeof(string));
                    Exceltable.Columns.Add("接单人", typeof(string));
                    Exceltable.Columns.Add("来源", typeof(string));

                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["产品名称"] = item.ProductXL + "_" + item.ProductName;
                        row["生产工序"] = item.Name;
                        row["生产价格"] = item.cost;
                        row["生产状态"] = item.status != null && item.status == 0 ? "生产中" : item.status == 1 ? "生产完成，待审核" : item.status == 2 ? "审核通过" : "被驳回";
                        row["生产开始时间"] = Convert.ToDateTime(item.act_begin_date).ToString("yyyy-MM-dd");
                        row["生产预计完成时间"] = Convert.ToDateTime(item.exp_end_date).ToString("yyyy-MM-dd");
                        row["接单人"] = item.user_name;
                        row["来源"] = item.source+","+ item.customer;

                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;

        }
        public PagedList<WIP_CWModel> GetWIP_CWPageList(SWIP_WOModel SModel, int PageIndex, int PageSize)
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
                var List = (from p in db.WIP_work_CW
                            where SModel.status != null && SModel.status == 0 ? p.CRM_contract_detail_id>0 : SModel.status == 1?p.WIP_contract_id>0:true
                            where p.CreateTime>StartTime
                            where p.CreateTime<EndTime
                            orderby p.CreateTime descending
                            select new WIP_CWModel
                            {
                                Id = p.Id,
                                CRM_contract_detail_id=p.CRM_contract_detail_id,
                                WIP_contract_id=p.WIP_contract_id,
                                customer = p.CRM_contract_detail.CRM_contract_header.CRM_customers.name,
                                workorder = p.workorder,
                                ProductName = p.SYS_product.name,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                style = p.stye,
                                qty = p.qty,
                                GMqty=p.GMqty,
                                YQqty=p.YQqty,
                                CreateTime = p.CreateTime,
                                WoodName = p.WoodName,
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public WIP_CWModel GetCWWIPById(int Id)
        {
            using (var db = new XNERPEntities())
            {

                var Tables = (from p in db.WIP_work_CW.Where(k => k.Id == Id)
                              select new WIP_CWModel
                              {
                                  Id = p.Id,
                                  GMqty = p.GMqty,
                                  YQqty = p.YQqty
                              }).FirstOrDefault();
                return Tables;
            }
        }
        public void UpdateWorkOrder(WIP_CWModel models)
        {
            using (var db = new XNERPEntities())
            {

                var Tables = db.WIP_work_CW.Where(k => k.Id == models.Id).FirstOrDefault();
                Tables.GMqty = models.GMqty;
                Tables.YQqty = models.YQqty;
                db.SaveChanges();
            }
        }
    }
}
