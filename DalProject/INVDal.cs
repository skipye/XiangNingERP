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
    public class INVDal
    {
        public PagedList<INVCKModel> GetINVCKPageList(INVCKSerModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.INV_inventories.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.name) ? p.name.Contains(SModel.name) : true
                            where SModel.typeId != null && SModel.typeId > 0 ? p.type == SModel.typeId : true
                            orderby p.created_time descending
                            select new INVCKModel
                            {
                                id = p.id,
                                name = p.name,
                                typeId = p.type,
                                address = p.address,
                                remark = p.remark,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        //新增和修改仓库设置
        public void AddOrUpdate(INVCKModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.INV_inventories.Where(k => k.id == Models.id).SingleOrDefault();
                    table.name = Models.name;
                    table.type = Models.typeId;
                    table.address = Models.address;
                    table.remark = Models.remark;
                }
                else
                {
                    INV_inventories table = new INV_inventories();
                    table.name = Models.name;
                    table.type = Models.typeId;
                    table.address = Models.address;
                    table.remark = Models.remark;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.INV_inventories.Add(table);
                }
                db.SaveChanges();
            }
        }
        //根据文章Id获取内容
        public INVCKModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.INV_inventories.Where(k => k.id == Id)
                              select new INVCKModel
                              {
                                  id = p.id,
                                  name = p.name,
                                  typeId = p.type,
                                  address = p.address,
                                  remark = p.remark,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.INV_inventories.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.INV_inventories.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        //获取仓库列表
        public List<SelectListItem> GetCKDrolist(int? pId,int? CKType)
        {
            
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择仓库", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<INV_inventories> model = db.INV_inventories.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                if (CKType != null && CKType > 0)
                {
                    model= db.INV_inventories.Where(b => b.delete_flag == false && b.type==CKType).OrderBy(k => k.created_time).ToList();
                }
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        public IList<CRMItem> GetCKAutolist(string KeyWord)
        {
            using (var db = new XNERPEntities())
            {
                var list = (from p in db.INV_inventories.Where(b => b.delete_flag == false)
                            where !string.IsNullOrEmpty(KeyWord) ? p.name.Contains(KeyWord) : true
                            orderby p.created_time descending
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name
                            }).Take(10).ToList();
                return list;
                
            }
        }

    }
}
