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
    public class SYS_CPMCDal
    {
        public PagedList<SYS_CPMCModel> GetPageList(SSYS_CPMCModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.SYS_product.Where(k => k.delete_flag == false)
                            where SModel.product_SN_id != null && SModel.product_SN_id > 0 ? p.product_SN_id == SModel.product_SN_id : true
                            where SModel.product_area_id != null && SModel.product_area_id > 0 ? p.product_area_id == SModel.product_area_id : true
                            where !string.IsNullOrEmpty(SModel.name) ? p.name.Contains(SModel.name) : true
                            orderby p.created_time descending
                            select new SYS_CPMCModel
                            {
                                id = p.id,
                                product_SN_id = p.product_SN_id,
                                product_SN_name = p.SYS_product_SN.name,
                                Pname = p.name,
                                remark = p.remark,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                created_time = p.created_time,
                                ConvertImg = p.picture,
                                product_area_id = p.product_area_id,
                                product_area_name = p.SYS_product_area.name,
                                volume = p.volume,
                                PersonPrice = p.reserved1,
                                paper_path = p.paper_path,
                                BOM_path = p.BOM_path
                            }).ToList();
                return List.OrderByDescending(k=>k.created_time).ToPagedList(PageIndex, PageSize);
            }
        }
        public void AddOrUpdate(SYS_CPMCModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.id != null && Models.id >0)
                {
                    var table = db.SYS_product.Where(k => k.id == Models.id).SingleOrDefault();
                    table.product_SN_id = Models.product_SN_id;
                    table.name = Models.Pname;
                    table.length = Models.length;
                    table.remark = Models.remark;
                    table.width = Models.width;
                    table.height = Models.height;
                    table.product_area_id = Models.product_area_id;
                    table.volume = Models.volume;
                    table.picture = Models.ConvertImg;
                    table.paper_path = Models.paper_path;
                    table.BOM_path = Models.BOM_path;
                    table.reserved1 = Models.PersonPrice ?? 0;
                }
                else
                {
                    SYS_product table = new SYS_product();
                    table.product_SN_id = Models.product_SN_id;
                    table.name = Models.Pname;
                    table.length = Models.length;
                    table.remark = Models.remark;
                    table.width = Models.width;
                    table.height = Models.height;
                    table.product_area_id = Models.product_area_id;
                    table.volume = Models.volume;
                    table.picture = Models.ConvertImg;
                    table.paper_path = Models.paper_path;
                    table.BOM_path = Models.BOM_path;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    table.reserved1 = Models.PersonPrice??0;
                    db.SYS_product.Add(table);
                }
                db.SaveChanges();

            }
        }
        //根据文章Id获取内容
        public SYS_CPMCModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.SYS_product.Where(k => k.id == Id)
                              select new SYS_CPMCModel
                              {
                                  id = p.id,
                                  product_SN_id = p.product_SN_id,
                                  product_area_id=p.product_area_id,
                                  Pname = p.name,
                                  remark = p.remark,
                                  length = p.length,
                                  width = p.width,
                                  height = p.height,
                                  volume=p.volume,
                                  ConvertImg = p.picture,
                                  paper_path = p.paper_path,
                                  BOM_path = p.BOM_path
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.SYS_product.Where(k => k.id == Id).SingleOrDefault();
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
                        var tables = db.SYS_product.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public string GetProDrolist(int? pId)
        {
            using (var db = new XNERPEntities())
            {
                var list = (from p in db.SYS_product.Where(b => b.delete_flag == false && b.product_SN_id == pId).OrderBy(k => k.created_time)
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name,
                                Address = p.SYS_product_SN.name,
                                length = p.length,
                                width = p.width,
                                height = p.height
                            }).ToList();
                string NewItme = "";
                foreach (var item in list)
                {
                    var strText=item.Address + "_" + item.Name + "_" + item.length + "_" + item.width + "_" + item.height;
                    var IstrValue=item.Id.ToString();
                    NewItme+="<option value="+IstrValue+">"+strText+"</option>";
                }
                return NewItme;
            }
           
        }
    }
}
