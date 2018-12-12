using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using MvcPager.WebControls.Mvc;
using ModelProject;
using System.Web.Mvc;

namespace DalProject
{
    public class INV_FLZLDal
    {
        public PagedList<INV_FLZLModel> GetPageList(SINV_FLZLModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.INV_accessory_type.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.name) ? p.name.Contains(SModel.name) : true
                            where SModel.catalogId > 0 ? p.catalog_id == SModel.catalogId : true
                            orderby p.created_time descending
                            select new INV_FLZLModel
                            {
                                id = p.Id,
                                name = p.name,
                                unit = p.unit,
                                remark = p.remark,
                                created_time = p.created_time,
                                catalogName = p.INV_accessory_catalog.name,
                                catalogId = p.catalog_id
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        //新增和修改仓库设置
        public void AddOrUpdate(INV_FLZLModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.INV_accessory_type.Where(k => k.Id == Models.id).SingleOrDefault();
                    table.name = Models.name;
                    table.unit = Models.unit;
                    table.remark = Models.remark;
                    table.catalog_id = Models.catalogId;
                    
                }
                else
                {
                    INV_accessory_type table = new INV_accessory_type();
                    table.name = Models.name;
                    table.unit = Models.unit;
                    table.remark = Models.remark;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    table.catalog_id = Models.catalogId;
                    db.INV_accessory_type.Add(table);
                }
                db.SaveChanges();
            }
        }
        //根据文章Id获取内容
        public INV_FLZLModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.INV_accessory_type.Where(k => k.Id == Id)
                              select new INV_FLZLModel
                              {
                                  id = p.Id,
                                  name = p.name,
                                  unit=p.unit,
                                  remark = p.remark,
                                  catalogId = p.catalog_id
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.INV_accessory_type.Where(k => k.Id == Id).SingleOrDefault();
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
                        var tables = db.INV_accessory_type.Where(k => k.Id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        //辅料种类
        public List<SelectListItem> GetFLZLDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择辅料种类", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<INV_accessory_type> model = db.INV_accessory_type.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name+"&"+item.unit, Value = item.Id.ToString(), Selected = pId.HasValue && item.Id.Equals(pId) });
                }
            }
            return items;
        }
        public List<SelectListItem> GetFLDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择辅料大类", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<INV_accessory_catalog> model = db.INV_accessory_catalog.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name , Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        public IList<CRMItem> GetFLZLAutolist(string KeyWord)
        {
            using (var db = new XNERPEntities())
            {
                var list = (from p in db.INV_accessory_type.Where(b => b.delete_flag == false)
                            where !string.IsNullOrEmpty(KeyWord) ? p.name.Contains(KeyWord) : true
                            orderby p.created_time descending
                            select new CRMItem
                            {
                                Id = p.Id,
                                Name = p.name,
                                unit=p.unit,
                                label = p.name + "[" + p.unit + "]",
                            }).Take(10).ToList();
                return list;

            }
        }
    }
}
