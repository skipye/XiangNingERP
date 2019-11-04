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
    public class CostDal
    {
        public PagedList<CostModel> GetPageList(SCostModel SModel)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.SYS_product_Cost.Where(k => k.DeleteFlag == false)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? p.SYS_product.product_SN_id == SModel.ProductSNId : true
                            where SModel.WoodId != null && SModel.WoodId > 0 ? p.Id == SModel.WoodId : true
                            orderby p.CreateTime
                            select new CostModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName = p.SYS_product.name,
                                WoodId = p.WoodId,
                                WoodName = p.INV_wood_type.name,
                                MCPrice = p.MCPrice,
                                KLPrice = p.KLPrice,
                                DHPrice = p.DHPrice,
                                MGQPrice = p.MGQPrice,
                                MGHPrice = p.MGHPrice,
                                GMPrice = p.GMPrice,
                                YQPrice = p.YQPrice,
                                FLPrice = p.FLPrice,
                                CreateTime = p.CreateTime
                            }).ToList();

                return List.ToPagedList(SModel.PageIndex??0, SModel.PageSize??10);
            }
        }
        //新增和修改仓库设置
        public void AddOrUpdate(CostModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.SYS_product_Cost.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.WoodId = Models.WoodId;
                    table.WoodName = Models.WoodName;
                    table.MCPrice = Models.MCPrice;
                    table.KLPrice = Models.KLPrice;
                    table.DHPrice = Models.DHPrice;
                    table.MGQPrice = Models.MGQPrice;
                    table.MGHPrice = Models.MGHPrice;
                    table.GMPrice = Models.GMPrice;
                    table.YQPrice = Models.YQPrice;
                    table.FLPrice = Models.FLPrice;
                }
                else
                {
                    SYS_product_Cost table = new SYS_product_Cost();
                    table.ProductId = Models.ProductId;
                    table.WoodId = Models.WoodId;
                    table.WoodName = Models.WoodName;
                    table.MCPrice = Models.MCPrice;
                    table.KLPrice = Models.KLPrice;
                    table.DHPrice = Models.DHPrice;
                    table.MGQPrice = Models.MGQPrice;
                    table.MGHPrice = Models.MGHPrice;
                    table.GMPrice = Models.GMPrice;
                    table.YQPrice = Models.YQPrice;
                    table.FLPrice = Models.FLPrice;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = false;
                    db.SYS_product_Cost.Add(table);
                }
                db.SaveChanges();
            }
        }
        //根据文章Id获取内容
        public CostModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.SYS_product_Cost.Where(k => k.Id == Id)
                              select new CostModel
                              {
                                  Id = p.Id,
                                  ProductId = p.ProductId,
                                  WoodId = p.WoodId,
                                  WoodName = p.WoodName,
                                  MCPrice = p.MCPrice,
                                  KLPrice = p.KLPrice,
                                  DHPrice = p.DHPrice,
                                  MGQPrice = p.MGQPrice,
                                  MGHPrice = p.MGHPrice,
                                  GMPrice = p.GMPrice,
                                  YQPrice = p.YQPrice,
                                  FLPrice = p.FLPrice,
                              }).SingleOrDefault();
                return tables;
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
                        var tables = db.SYS_product_Cost.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = true;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
