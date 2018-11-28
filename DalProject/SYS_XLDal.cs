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
    public class SYS_XLDal
    {
        public PagedList<SYS_XLModel> GetPageList(SSYS_XLModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.SYS_product_SN.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.name) ? p.name.Contains(SModel.name) : true
                            orderby p.created_time descending
                            select new SYS_XLModel
                            {
                                id = p.id,
                                SN = p.SN,
                                name = p.name,
                                remark = p.remark,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        //新增和修改仓库设置
        public void AddOrUpdate(SYS_XLModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.SYS_product_SN.Where(k => k.id == Models.id).SingleOrDefault();
                    table.SN = Models.SN;
                    table.name = Models.name;
                    table.remark = Models.remark;
                }
                else
                {
                    SYS_product_SN table = new SYS_product_SN();
                    table.SN = Models.SN;
                    table.name = Models.name;
                    table.remark = Models.remark;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.SYS_product_SN.Add(table);
                }
                db.SaveChanges();

            }
        }
        //根据文章Id获取内容
        public SYS_XLModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.SYS_product_SN.Where(k => k.id == Id)
                              select new SYS_XLModel
                              {
                                  id = p.id,
                                  SN = p.SN,
                                  name = p.name,
                                  remark = p.remark,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.SYS_product_SN.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.SYS_product_SN.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public List<SelectListItem> GetXLDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择产品系列", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<SYS_product_SN> model = db.SYS_product_SN.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        //产品区域
        public List<SelectListItem> GetAreaDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择产品区域", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<SYS_product_area> model = db.SYS_product_area.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
    }
}
