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
    public class INV_ZLDal
    {
        public PagedList<INV_ZLModel> GetPageList(SINV_ZLModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.INV_materials.Where(k => k.delete_flag == false)
                            where SModel.WoodId != null && SModel.WoodId > 0 ? p.WoodId == SModel.WoodId : true
                            where SModel.inventory_id != null && SModel.inventory_id > 0 ? p.inventory_id == SModel.inventory_id : true
                            orderby p.created_time descending
                            select new INV_ZLModel
                            {
                                id = p.id,
                                qty = p.qty,
                                price=p.price,
                                remark = p.remark,
                                INVName=p.INV_inventories.name,
                                inventory_id=p.inventory_id,
                                WoodName=p.INV_wood_type.name,
                                WoodId = p.WoodId,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        //新增和修改仓库设置
        public void AddOrUpdate(INV_ZLModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.INV_materials.Where(k => k.id == Models.id).SingleOrDefault();
                    table.qty = Models.qty;
                    table.inventory_id = Models.inventory_id;
                    table.price = Models.price;
                    table.remark = Models.remark;
                    table.WoodId = Models.WoodId;
                    table.inventory_id = Models.inventory_id;
                }
                else
                {
                    INV_materials table = new INV_materials();
                    table.qty = Models.qty;
                    table.inventory_id = Models.inventory_id;
                    table.price = Models.price;
                    table.remark = Models.remark;
                    table.WoodId = Models.WoodId;
                    table.inventory_id = Models.inventory_id;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.INV_materials.Add(table);
                }
                db.SaveChanges();

                AddOrUpdateTable(Models.inventory_id.Value, Models.WoodId.Value, Models.qty.Value);
            }
        }
        //写入关联表方法
        public static void AddOrUpdateTable(int CKId, int TypeId, int qty)
        {
            using (var db = new XNERPEntities())
            {
                var IsTable = db.INV_material_stock.Where(k => k.WoodId == TypeId && k.inventory_id == CKId).FirstOrDefault();
                if (IsTable != null)
                {
                    decimal NewCcount = IsTable.C_count ?? 0;
                    decimal NewW_count = IsTable.W_count ?? 0;
                    IsTable.C_count = NewCcount + qty;
                    IsTable.W_count = NewW_count + qty;
                }
                else {
                    INV_material_stock tables = new INV_material_stock();
                    tables.id = Guid.NewGuid();
                    tables.WoodId = TypeId;
                    tables.inventory_id = CKId;
                    tables.C_count = qty;
                    tables.W_count = qty;
                    db.INV_material_stock.Add(tables);
                }
                db.SaveChanges();
            }
        }

        //根据文章Id获取内容
        public INV_ZLModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.INV_materials.Where(k => k.id == Id)
                              select new INV_ZLModel
                              {
                                  id = p.id,
                                  WoodId = p.WoodId,
                                  inventory_id = p.inventory_id,
                                  qty = p.qty,
                                  price=p.price,
                                  remark = p.remark,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.INV_materials.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.INV_materials.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public DataTable ToExcel(SINV_ZLModel SModel)
        {
            DataTable Exceltable = new DataTable();

            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_materials.Where(k => k.delete_flag == false)
                            where SModel.WoodId != null && SModel.WoodId > 0 ? p.WoodId == SModel.WoodId : true
                            where SModel.inventory_id != null && SModel.inventory_id > 0 ? p.inventory_id == SModel.inventory_id : true
                            orderby p.created_time descending
                            select new INV_ZLModel
                            {
                                id = p.id,
                                qty = p.qty,
                                price = p.price,
                                remark = p.remark,
                                INVName = p.INV_inventories.name,
                                inventory_id = p.inventory_id,
                                WoodName = p.INV_wood_type.name,
                                WoodId = p.WoodId,
                                created_time = p.created_time
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("所入仓库", typeof(string));
                    Exceltable.Columns.Add("所买材质", typeof(string));
                    Exceltable.Columns.Add("购买数量(KG)", typeof(string));
                    Exceltable.Columns.Add("价格", typeof(string));
                    Exceltable.Columns.Add("上传时间", typeof(string));
                    Exceltable.Columns.Add("备注", typeof(string));
                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["所入仓库"] = item.INVName;
                        row["所买材质"] = item.WoodName;
                        row["购买数量(KG)"] = item.qty;
                        row["价格"] = item.price;
                        row["上传时间"] = Convert.ToDateTime(item.created_time).ToString("yyyy-MM-dd"); ;
                        row["备注"] = item.remark;

                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }
    }
}
