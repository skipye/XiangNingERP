using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using MvcPager.WebControls.Mvc;
using ModelProject;
using System.Web.Mvc;
using System.Data;

namespace DalProject
{
    public class SaleSerDal
    {
        public PagedList<SaleSerModel> GetPageList(SSaleSerModel SModel)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.SaleServiceInfo.Where(k => k.DelState == false)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN.Contains(SModel.SN) : true
                            where !string.IsNullOrEmpty(SModel.Customer) ? p.Customer.Contains(SModel.Customer) : true
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new SaleSerModel
                            {
                                Id = p.Id,
                                SN = p.SN,
                                Customer = p.Customer,
                                TelPhone = p.TelPhone,
                                GoHomeDate = p.GoHomeDate,
                                SaleSerDate = p.SaleSerDate,
                                Amount = p.Amount,
                                SFState = p.SFState,
                                LinkAddress = p.LinkAddress,
                                userName = p.userName,
                                status = p.status,
                                CheckedTime = p.CheckedTime,
                                CreateTime = p.CreateTime,
                                Remark = p.Remark,
                            }).ToList();
                return List.ToPagedList(SModel.PageIndex, SModel.PageSize);
            }
        }
        
        public void AddOrUpdate(SaleSerModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.SaleServiceInfo.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.Customer = Models.Customer;
                    table.TelPhone = Models.TelPhone;
                    table.GoHomeDate = Models.GoHomeDate;
                    table.SaleSerDate = Models.SaleSerDate;
                    table.Amount = Models.Amount;
                    table.SFState = Models.SFState;
                    table.LinkAddress = Models.LinkAddress;
                    table.userName = Models.userName;
                }
                else
                {
                    SaleServiceInfo table = new SaleServiceInfo();
                    table.SN = Models.SN;
                    table.Customer = Models.Customer;
                    table.TelPhone = Models.TelPhone;
                    table.GoHomeDate = Models.GoHomeDate;
                    table.SaleSerDate = Models.SaleSerDate;
                    table.Amount = Models.Amount;
                    table.SFState = Models.SFState;
                    table.LinkAddress = Models.LinkAddress;
                    table.userName = Models.userName;
                    table.status = 0;
                    table.CreateTime = DateTime.Now;
                    table.DelState = false;
                    table.Remark=Models.Remark;
                    db.SaleServiceInfo.Add(table);
                }
                db.SaveChanges();

            }
        }
        
        public SaleSerModel GetDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.SaleServiceInfo.Where(k => k.Id==Id)
                              select new SaleSerModel
                              {
                                  Id = p.Id,
                                  SN = p.SN,
                                  Customer = p.Customer,
                                  TelPhone = p.TelPhone,
                                  GoHomeDate = p.GoHomeDate,
                                  SaleSerDate = p.SaleSerDate,
                                  Amount = p.Amount,
                                  SFState = p.SFState,
                                  LinkAddress = p.LinkAddress,
                                  userName = p.userName,
                                  status = p.status,
                                  CheckedTime = p.CheckedTime,
                                  CreateTime = p.CreateTime,
                                  Remark = p.Remark,
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
                        var tables = db.SaleServiceInfo.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DelState = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public void DeleteProMore(string ListId)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        Guid Id = Guid.Parse(item);
                        var tables = db.SaleService_Products.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DelState = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public void Checked(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.SaleServiceInfo.Where(k => k.Id == Id).SingleOrDefault();
                tables.status = 1;
                tables.CheckedTime = DateTime.Now;

                db.SaveChanges();
            }
        }
        //根据日期获取合同总个数
        public int GetSaleSerCount(DateTime CreateTime)
        { 
             using (var db = new XNERPEntities())
            {
                var List = db.SaleServiceInfo.Where(k => k.CreateTime > CreateTime && k.DelState==false).Count();
                if (List > 0)
                { return List+1; }
                else { return 1; }
             }
        }
        public void AddSaleSerPro(SaleSerProModel Models)
        {
            using (var db = new XNERPEntities())
            {
                SaleService_Products table = new SaleService_Products();
                table.Id = Guid.NewGuid();
                table.SaleServiceId = Models.SaleServiceId;
                table.ProductStyle = Models.ProductStyle;
                table.ProductName = Models.ProductName;
                table.WoodName = Models.WoodName;
                table.QusState = Models.QusState;
                table.AnState = Models.AnState;
                table.Remark = Models.Remark;
                table.CreateTime = DateTime.Now;
                table.DelState = false;
                db.SaleService_Products.Add(table);
                db.SaveChanges();
            }
        }
        public List<SaleSerProModel> GetProPageList(int SSId)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.SaleService_Products.Where(k => k.DelState == false && k.SaleServiceId == SSId)
                            orderby p.CreateTime descending
                            select new SaleSerProModel
                            {
                                Id = p.Id,
                                SaleServiceId = p.SaleServiceId,
                                ProductName = p.ProductName,
                                ProductStyle = p.ProductStyle,
                                WoodName = p.WoodName,
                                QusState = p.QusState,
                                AnState = p.AnState,
                                Remark = p.Remark,
                                CreateTime = p.CreateTime,
                                status = p.SaleServiceInfo.status,
                            }).ToList();
                return List;
            }
        }
        
    }
}
