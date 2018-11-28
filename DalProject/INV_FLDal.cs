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
    public class INV_FLDal
    {
        public PagedList<INV_FLModel> GetPageList(SINV_FLModel SModel, int PageIndex, int PageSize)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_accessory.Where(k => k.delete_flag == false)
                            where SModel.accessory_type_id != null && SModel.accessory_type_id > 0 ? p.wood_id == SModel.accessory_type_id : true
                            where SModel.inventory_id != null && SModel.inventory_id > 0 ? p.inv_id == SModel.inventory_id : true
                            orderby p.created_time descending
                            select new INV_FLModel
                            {
                                id = p.id,
                                qty = p.qty,
                                price = p.price,
                                remark = p.remark,
                                inventory_id = p.inv_id,
                                accessory_type_id = p.wood_id,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public PagedList<INV_FLModel> GetFLPageList(SINV_FLModel SModel, int PageIndex, int PageSize)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_accessory_stock
                            where SModel.accessory_type_id != null && SModel.accessory_type_id > 0 ? p.accessory_type_id == SModel.accessory_type_id : true
                            where SModel.inventory_id != null && SModel.inventory_id > 0 ? p.inventory_id == SModel.inventory_id : true
                            select new INV_FLModel
                            {
                                CKId = p.Id,
                                INVName = p.INV_accessory_type.name,
                                FLZL=p.INV_accessory_type.INV_accessory_catalog.name,
                                accessoryName = p.INV_inventories.name,
                                W_count = p.C_count
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        //新增和修改仓库设置
        public void AddOrUpdate(INV_FLModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id > 0)
                {
                    var table = db.INV_accessory.Where(k => k.id == Models.id).SingleOrDefault();
                    table.qty = Models.qty;
                    table.wood_id = Models.accessory_type_id;
                    table.inv_id = Models.inventory_id;
                    table.price = Models.price;
                    table.remark = Models.remark;
                }
                else
                {
                    INV_accessory table = new INV_accessory();
                    table.wood_id = Models.accessory_type_id;
                    table.inv_id = Models.inventory_id;
                    table.qty = Models.qty;
                    table.price = Models.price;
                    table.remark = Models.remark;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.INV_accessory.Add(table);
                }
                db.SaveChanges();

                AddOrUpdateTable(Models.inventory_id, Models.accessory_type_id, Models.qty);
            }
        }
        //写入关联表方法
        public static void AddOrUpdateTable(int CKId, int TypeId, decimal qty)
        {
            using (var db = new XNERPEntities())
            {
                var IsTable = db.INV_accessory_stock.Where(k => k.accessory_type_id == TypeId && k.inventory_id == CKId).FirstOrDefault();
                if (IsTable != null)
                {
                    decimal NewCcount = IsTable.C_count ?? 0;
                    decimal NewW_count = IsTable.W_count ?? 0;
                    IsTable.C_count = NewCcount + qty;
                    IsTable.W_count = NewW_count + qty;
                }
                else
                {
                    INV_accessory_stock tables = new INV_accessory_stock();
                    tables.Id = Guid.NewGuid();
                    tables.accessory_type_id = TypeId;
                    tables.inventory_id = CKId;
                    tables.C_count = qty;
                    tables.W_count = qty;
                    db.INV_accessory_stock.Add(tables);
                }
                db.SaveChanges();
            }
        }

        //根据文章Id获取内容
        public INV_FLModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.INV_accessory.Where(k => k.id == Id)
                              select new INV_FLModel
                              {
                                  id = p.id,
                                  accessory_type_id = p.wood_id,
                                  inventory_id = p.inv_id,
                                  qty = p.qty,
                                  price = p.price,
                                  remark = p.remark,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.INV_accessory.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.INV_accessory.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public void CheckOut(Guid Id, decimal qty)
        {
            using (var db = new XNERPEntities())
            {
                var Table = db.INV_accessory_stock.Where(k => k.Id == Id ).FirstOrDefault();
                Table.C_count = Table.C_count - qty;
                db.SaveChanges();
            }
        }
        public DataTable ToExcel(SINV_FLModel SModel)
        {
            DataTable Exceltable = new DataTable();
            
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_accessory_stock
                            where SModel.accessory_type_id != null && SModel.accessory_type_id > 0 ? p.accessory_type_id == SModel.accessory_type_id : true
                            where SModel.inventory_id != null && SModel.inventory_id > 0 ? p.inventory_id == SModel.inventory_id : true
                            select new INV_FLModel
                            {
                                CKId = p.Id,
                                INVName = p.INV_accessory_type.name,
                                FLZL = p.INV_accessory_type.INV_accessory_catalog.name,
                                accessoryName = p.INV_inventories.name,
                                W_count = p.C_count
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("所入仓库", typeof(string));
                    Exceltable.Columns.Add("所属种类", typeof(string));
                    Exceltable.Columns.Add("所买产品", typeof(string));
                    Exceltable.Columns.Add("库存数量", typeof(string));

                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["所入仓库"] = item.accessoryName;
                        row["所属种类"] = item.FLZL;
                        row["所买产品"] = item.INVName;
                        row["库存数量"] = item.W_count;
                       
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;

        }
    }
}
