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
    public class CRM_HTProDal
    {
        public PagedList<CRM_HTProModel> GetPageList(int HTId, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.CRM_contract_detail.Where(k => k.delete_flag == false && k.header_id==HTId)
                            orderby p.created_time descending
                            select new CRM_HTProModel
                            {
                                id = p.id,
                                header_id = p.header_id,
                                productName=p.SYS_product.name,
                                productXL=p.SYS_product.SYS_product_SN.name,
                                product_id = p.product_id,
                                wood_type_id = p.wood_type_id,
                                woodName = p.INV_wood_type.name,
                                color_id = p.color_id,
                                colorName = p.SYS_colors.name,
                                custom_flag=p.custom_flag,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                price = p.price,
                                qty = p.qty,
                                hardware_part = p.hardware_part,
                                decoration_part = p.decoration_part,
                                req_others = p.req_others,
                                created_time = p.created_time,
                                status=p.CRM_contract_header.status,
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public void AddOrUpdate(CRM_HTProModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.CRM_contract_detail.Where(k => k.id == Models.id).SingleOrDefault();
                    table.header_id = Models.header_id;
                    table.color = Models.colorName;
                    table.product_id = Models.product_id;
                    table.wood_type_id = Models.wood_type_id;
                    table.color_id = Models.color_id;
                    table.custom_flag = Models.custom_flag;
                    table.length = Models.length;
                    table.width = Models.width;
                    table.height = Models.height;
                    table.price = Models.price;
                    table.qty = Models.qty;
                    table.hardware_part = Models.hardware_part;
                    table.decoration_part = Models.decoration_part;
                    table.req_others = Models.req_others;
                }
                else
                {
                    CRM_contract_detail table = new CRM_contract_detail();
                    table.header_id = Models.header_id;
                    table.color = Models.colorName;
                    table.product_id = Models.product_id;
                    table.wood_type_id = Models.wood_type_id;
                    table.color_id = Models.color_id;
                    table.custom_flag = Models.custom_flag;
                    table.length = Models.length;
                    table.width = Models.width;
                    table.height = Models.height;
                    table.price = Models.price;
                    table.qty = Models.qty;
                    table.hardware_part = Models.hardware_part;
                    table.decoration_part = Models.decoration_part;
                    table.req_others = Models.req_others;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    table.status = 0;
                    db.CRM_contract_detail.Add(table);
                }
                var HeadTable = db.CRM_contract_header.Where(k => k.id == Models.header_id).SingleOrDefault();
                HeadTable.amount = HeadTable.amount + Models.price.Value * Models.qty;
                db.SaveChanges();

            }
        }
        //根据文章Id获取内容
        public CRM_HTProModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.CRM_contract_detail.Where(k => k.id == Id)
                              select new CRM_HTProModel
                              {
                                  id = p.id,
                                  header_id = p.header_id,
                                  product_id = p.product_id,
                                  wood_type_id = p.wood_type_id,
                                  woodName = p.INV_wood_type.name,
                                  color_id = p.color_id,
                                  colorName = p.SYS_colors.name,
                                  custom_flag=p.custom_flag,
                                  length = p.length,
                                  width = p.width,
                                  height = p.height,
                                  price = p.price,
                                  qty = p.qty,
                                  hardware_part = p.hardware_part,
                                  decoration_part = p.decoration_part,
                                  req_others = p.req_others,
                                  created_time = p.created_time
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.CRM_contract_detail.Where(k => k.id == Id).SingleOrDefault();
                tables.delete_flag = true;

                var HeadId = tables.header_id;
                var HeadTable = db.CRM_contract_header.Where(k => k.id == HeadId).FirstOrDefault();
                HeadTable.amount = HeadTable.amount - tables.qty * tables.price.Value;

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
                        var tables = db.CRM_contract_detail.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public IList<CRMItem> GetProAutolist(string KeyWord,int? ProAreaId)
        {
            using (var db = new XNERPEntities())
            {
                var list = (from p in db.SYS_product.Where(b => b.delete_flag == false)
                            where ProAreaId != null && ProAreaId > 0 ? p.product_area_id == ProAreaId : true
                            where !string.IsNullOrEmpty(KeyWord) ? p.name.Contains(KeyWord) : true
                            orderby p.created_time descending
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name,
                                label = p.name,
                                length = p.length,
                                height = p.height,
                                width=p.width,
                            }).Take(10).ToList();
                return list;

            }
        }
    }
}
