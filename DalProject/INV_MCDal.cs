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
    public class INV_MCDal
    {
        public PagedList<INV_MCModel> GetPageList(SINV_MCModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.INV_wood_type.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.name) ? p.name.Contains(SModel.name) : true
                            orderby p.created_time descending
                            select new INV_MCModel
                            {
                                id = p.id,
                                name = p.name,
                                nickname = p.nickname,
                                place = p.place,
                                remark = p.remark,
                                g_ccl = p.g_ccl,
                                g_bz = p.g_bz,
                                q_ccl = p.q_ccl,
                                q_bz = p.q_bz,
                                prcie = p.prcie,
                                cc_prcie = p.cc_prcie,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public PagedList<INV_MCModel> GetXTWoodPageList(SINV_MCModel SModel, int PageIndex, int PageSize)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_wood_type.Where(k => k.delete_flag == false && k.Sort==1)
                            where !string.IsNullOrEmpty(SModel.name) ? p.name.Contains(SModel.name) : true
                            orderby p.created_time descending
                            select new INV_MCModel
                            {
                                id = p.id,
                                name = p.name,
                                nickname = p.nickname,
                                place = p.place,
                                remark = p.remark,
                                g_ccl = p.g_ccl,
                                g_bz = p.g_bz,
                                q_ccl = p.q_ccl,
                                q_bz = p.q_bz,
                                prcie = p.prcie,
                                cc_prcie = p.cc_prcie,
                                created_time = p.created_time,
                                personprice=p.PersonPrice
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public void AddOrUpdate(INV_MCModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.INV_wood_type.Where(k => k.id == Models.id).SingleOrDefault();
                    table.name = Models.name;
                    table.nickname = Models.nickname;
                    table.place = Models.place;
                    table.remark = Models.remark;
                    table.g_ccl = Models.g_ccl;
                    table.g_bz = Models.g_bz;
                    table.q_ccl = Models.q_ccl;
                    table.q_bz = Models.q_bz;
                    table.prcie = Models.prcie;
                    table.cc_prcie = Models.cc_prcie;
                    table.PersonPrice = Models.personprice;
                }
                else
                {
                    INV_wood_type table = new INV_wood_type();
                    table.name = Models.name;
                    table.nickname = Models.nickname;
                    table.place = Models.place;
                    table.remark = Models.remark;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.INV_wood_type.Add(table);
                }
                db.SaveChanges();
            }
        }
        public void UpdateWood(INV_MCModel Models)
        {
            using (var db = new XNERPEntities())
            {

                var table = db.INV_wood_type.Where(k => k.id == Models.id).SingleOrDefault();
                table.g_ccl = Models.g_ccl;
                table.g_bz = Models.g_bz;
                table.q_ccl = Models.q_ccl;
                table.q_bz = Models.q_bz;
                table.prcie = Models.prcie;
                table.cc_prcie = Models.cc_prcie;
                table.PersonPrice = Models.personprice;
                db.SaveChanges();
            }
        }
        public INV_MCModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.INV_wood_type.Where(k => k.id == Id)
                              select new INV_MCModel
                              {
                                  id = p.id,
                                  name = p.name,
                                  nickname = p.nickname,
                                  place = p.place,
                                  remark = p.remark,
                                  g_ccl = p.g_ccl,
                                  g_bz = p.g_bz,
                                  q_ccl = p.q_ccl,
                                  q_bz = p.q_bz,
                                  prcie = p.prcie,
                                  cc_prcie = p.cc_prcie,
                                  personprice=p.PersonPrice,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.INV_wood_type.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.INV_wood_type.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        //木材列表
        public List<SelectListItem> GetWoodDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择材质", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<INV_wood_type> model = db.INV_wood_type.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        public List<SelectListItem> GetXTWoodDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择材质", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<INV_wood_type> model = db.INV_wood_type.Where(b => b.delete_flag == false && b.Sort==1).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name, Value = item.id.ToString() + "$" + item.g_ccl + "$" + item.g_bz + "$" + item.q_ccl + "$" + item.q_bz + "$" + item.prcie + "$" + item.cc_prcie });
                }
            }
            return items;
        }
        public decimal? GetProvolume(int product_id)
        {
            using (var db = new XNERPEntities())
            {
                var table = db.SYS_product.Where(k => k.id == product_id).SingleOrDefault();
                return table.volume;
            }
        }
    }
}
