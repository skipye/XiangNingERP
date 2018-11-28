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
    public class SYS_SHDal
    {
        public PagedList<SYS_SHModel> GetPageList(SSYS_SHModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.SYS_colors.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.name) ? p.name.Contains(SModel.name) : true
                            orderby p.created_time descending
                            select new SYS_SHModel
                            {
                                id = p.id,
                                SN = p.SN,
                                name = p.name,
                                remark = p.remark,
                                RGB = p.RGB,
                                img = p.img,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        //新增和修改仓库设置
        public void AddOrUpdate(SYS_SHModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.SYS_colors.Where(k => k.id == Models.id).SingleOrDefault();
                    table.SN = Models.SN;
                    table.name = Models.name;
                    table.RGB = Models.RGB;
                    table.remark = Models.remark;
                    table.img = Models.img;
                }
                else
                {
                    SYS_colors table = new SYS_colors();
                    table.SN = Models.SN;
                    table.name = Models.name;
                    table.RGB = Models.RGB;
                    table.remark = Models.remark;
                    table.img = Models.img;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.SYS_colors.Add(table);
                }
                db.SaveChanges();

            }
        }
        //根据文章Id获取内容
        public SYS_SHModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.SYS_colors.Where(k => k.id == Id)
                              select new SYS_SHModel
                              {
                                  id = p.id,
                                  SN = p.SN,
                                  name = p.name,
                                  RGB = p.RGB,
                                  img = p.img,
                                  remark = p.remark,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.SYS_colors.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.SYS_colors.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public List<SelectListItem> GetSHDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择色号", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<SYS_colors> model = db.SYS_colors.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
    }
}
