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
    public class CRM_KHDal
    {
        public PagedList<CRM_KHModel> GetPageList(SCRM_KHModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.CRM_customers.Where(k => k.delete_flag == false)
                            where !string.IsNullOrEmpty(SModel.name) ? p.name.Contains(SModel.name) : true
                            orderby p.created_time descending
                            select new CRM_KHModel
                            {
                                id = p.id,
                                SN = p.SN,
                                name = p.name,
                                remark = p.remark,
                                shortname = p.shortname,
                                address = p.address,
                                address_delivery = p.address_delivery,
                                linkman = p.linkman,
                                tel = p.tel,
                                created_time = p.created_time
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        //新增和修改仓库设置
        public void AddOrUpdate(CRM_KHModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.CRM_customers.Where(k => k.id == Models.id).SingleOrDefault();
                    table.SN = DateTime.Now.ToString("XN_HHmmss");
                    table.name = Models.name;
                    table.shortname = Models.shortname;
                    table.address = Models.address;
                    table.address_delivery = Models.address_delivery;
                    table.linkman = Models.linkman;
                    table.tel = Models.tel;
                    table.remark = Models.remark;
                }
                else
                {
                    CRM_customers table = new CRM_customers();
                    table.SN = DateTime.Now.ToString("XN_HHmmss");
                    table.name = Models.name;
                    table.shortname = Models.shortname;
                    table.address = Models.address;
                    table.address_delivery = Models.address_delivery;
                    table.linkman = Models.linkman;
                    table.tel = Models.tel;
                    table.remark = Models.remark;
                    table.ListUserId = "," + Models.UserId + ",";
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.CRM_customers.Add(table);
                }
                db.SaveChanges();

            }
        }
        public void AddHKByUser(int KHId,string ListUserId)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.CRM_customers.Where(k => k.id == KHId && k.delete_flag == false).FirstOrDefault();
                if (tables != null)
                { tables.ListUserId = ListUserId; db.SaveChanges(); }

            }
        }
        //根据文章Id获取内容
        public CRM_KHModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.CRM_customers.Where(k => k.id == Id)
                              select new CRM_KHModel
                              {
                                  id = p.id,
                                  SN = p.SN,
                                  name = p.name,
                                  remark = p.remark,
                                  shortname = p.shortname,
                                  address = p.address,
                                  address_delivery = p.address_delivery,
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
                var tables = db.CRM_customers.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.CRM_customers.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public string CrrKHByUser(int KHId)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.CRM_customers.Where(k => k.id == KHId).SingleOrDefault();
                return tables.ListUserId;
            }
        }
        public List<SelectListItem> GetKHDroList(int UId)
        {
            string StrUserId = "," + UId + ",";
            using (var db = new XNERPEntities())
            {
                var list = (from p in db.CRM_customers.Where(k => k.delete_flag==false)
                            where UId>0?p.ListUserId.Contains(StrUserId):true
                            orderby p.created_time descending
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name,
                                Address = p.address_delivery
                            }).ToList();
                List<SelectListItem> children = new List<SelectListItem>();
                children.Add(new SelectListItem() { Text = "请选择您的客户", Value = "" });

                foreach (var item in list)
                {
                    children.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() + "|" + item.Address });
                }
                return children;
            }
        }
    }
}
