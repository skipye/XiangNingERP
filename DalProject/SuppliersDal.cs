using Infrastructure;
using ModelProject;
using MvcPager.WebControls.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DalProject
{
    public class SuppliersDal
    {
        public PagedList<SuppliersModel> GetPageList(SSuppliersModel SModel, int PageIndex, int PageSize)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_suppliers.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.name) ? p.name.Contains(SModel.name) : true
                            orderby p.created_time descending
                            select new SuppliersModel
                            {
                                id = p.id,
                                SN = p.SN,
                                name = p.name,
                                remark = p.remark,
                                shortname = p.shortname,
                                address = p.address,
                                linkman = p.linkman,
                                tel = p.tel,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public void AddOrUpdate(SuppliersModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id > 0)
                {
                    var table = db.CRM_suppliers.Where(k => k.id == Models.id).SingleOrDefault();
                    table.SN = DateTime.Now.ToString("XN_HHmmss");
                    table.name = Models.name;
                    table.shortname = Models.shortname;
                    table.address = Models.address;
                    table.linkman = Models.linkman;
                    table.tel = Models.tel;
                    table.remark = Models.remark;
                }
                else
                {
                    CRM_suppliers table = new CRM_suppliers();
                    table.SN = DateTime.Now.ToString("XN_HHmmss");
                    table.name = Models.name;
                    table.shortname = Models.shortname;
                    table.address = Models.address;
                    table.linkman = Models.linkman;
                    table.tel = Models.tel;
                    table.remark = Models.remark;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.CRM_suppliers.Add(table);
                }
                db.SaveChanges();

            }
        }
        //根据文章Id获取内容
        public SuppliersModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.CRM_suppliers.Where(k => k.id == Id)
                              select new SuppliersModel
                              {
                                  id = p.id,
                                  SN = p.SN,
                                  name = p.name,
                                  remark = p.remark,
                                  shortname = p.shortname,
                                  address = p.address,
                                  linkman = p.linkman,
                                  tel = p.tel,
                                  created_time = p.created_time
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.CRM_suppliers.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.CRM_suppliers.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
