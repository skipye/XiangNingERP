using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using MvcPager.WebControls.Mvc;
using ModelProject;
using System.Data;


namespace DalProject
{
    public class INVDBDal
    {
        public PagedList<OnlyboardModel> GetPageList(INVDBSModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.INV_single_board.Where(k => k.delete_flag == false)
                            where SModel.length != null && SModel.length > 0 ? p.length >= SModel.length : true
                            where SModel.width != null && SModel.width > 0 ? p.width >= SModel.width : true
                            where SModel.height != null && SModel.height > 0 ? p.height >= SModel.height : true
                            where SModel.inventory_id != null && SModel.inventory_id > 0 ? p.inventory_id == SModel.inventory_id : true
                            orderby p.created_time descending
                            select new OnlyboardModel
                            {
                                id = p.id,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                weight=p.weight,
                                price=p.price,
                                inventoryName=p.INV_inventories.name,
                                remark = p.remark,
                                created_time = p.created_time,
                                Wood_typeId = p.Wood_typeId,
                                WoodName=p.INV_wood_type.name,
                                qty =p.qty
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        //新增和修改仓库设置
        public void AddOrUpdate(OnlyboardModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.INV_single_board.Where(k => k.id == Models.id).SingleOrDefault();
                    table.length = Models.length;
                    table.width = Models.width;
                    table.height = Models.height;
                    table.weight = Models.weight;
                    table.price = Models.price;
                    table.remark = Models.remark;
                    table.inventory_id = Models.inventory_id;
                    table.Wood_typeId = Models.Wood_typeId;
                    table.qty = Models.qty;


                }
                else
                {
                    INV_single_board table = new INV_single_board();
                    table.length = Models.length;
                    table.width = Models.width;
                    table.height = Models.height;
                    table.weight = Models.weight;
                    table.price = Models.price;
                    table.remark = Models.remark;
                    table.inventory_id = Models.inventory_id;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    table.status = 0;
                    table.Wood_typeId = Models.Wood_typeId;
                    table.qty = Models.qty;
                    db.INV_single_board.Add(table);
                }
                db.SaveChanges();
            }
        }
        //根据文章Id获取内容
        public OnlyboardModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.INV_single_board.Where(k => k.id == Id)
                              select new OnlyboardModel
                              {
                                  id = p.id,
                                  length = p.length,
                                  width = p.width,
                                  height = p.height,
                                  weight = p.weight,
                                  price = p.price,
                                  remark = p.remark,
                                  inventory_id = p.inventory_id,
                                  Wood_typeId = p.Wood_typeId,
                                  qty = p.qty
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.INV_single_board.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.INV_single_board.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public DataTable ToExcel(INVDBSModel SModel)
        {
            DataTable Exceltable = new DataTable();
           
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_single_board.Where(k => k.delete_flag == false)
                            where SModel.length != null && SModel.length > 0 ? p.length >= SModel.length : true
                            where SModel.width != null && SModel.width > 0 ? p.width >= SModel.width : true
                            where SModel.height != null && SModel.height > 0 ? p.height >= SModel.height : true
                            where SModel.inventory_id != null && SModel.inventory_id > 0 ? p.inventory_id == SModel.inventory_id : true
                            orderby p.created_time descending
                            select new OnlyboardModel
                            {
                                id = p.id,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                weight = p.weight,
                                price = p.price,
                                inventoryName = p.INV_inventories.name,
                                remark = p.remark,
                                created_time = p.created_time,
                                Wood_typeId = p.Wood_typeId,
                                WoodName = p.INV_wood_type.name,
                                qty = p.qty
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("所属仓库", typeof(string));
                    Exceltable.Columns.Add("独板长", typeof(string));
                    Exceltable.Columns.Add("独板宽", typeof(string));
                    Exceltable.Columns.Add("独板高", typeof(string));
                    Exceltable.Columns.Add("材质", typeof(string));
                    Exceltable.Columns.Add("数量（块）", typeof(string));
                    Exceltable.Columns.Add("独板重量", typeof(string));
                    Exceltable.Columns.Add("独板价格", typeof(string));
                    Exceltable.Columns.Add("上传时间", typeof(string));
                    Exceltable.Columns.Add("备注", typeof(string));
                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["所属仓库"] = item.inventoryName;
                        row["独板长"] = item.length;
                        row["独板宽"] = item.width;
                        row["独板高"] = item.height;
                        row["材质"] = item.WoodName;
                        row["数量（块）"] = item.qty;
                        row["独板重量"] = item.weight;
                        row["独板价格"] = item.price;
                        row["上传时间"] = Convert.ToDateTime(item.created_time).ToString("yyyy-MM-dd"); ;
                        row["备注"] =  item.remark;
                       
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }
    }
}
