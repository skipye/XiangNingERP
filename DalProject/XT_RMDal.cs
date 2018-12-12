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
    public class XT_RMDal
    {
        public PagedList<XT_RMModel> GetPageList(SXT_RMModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNERPEntities())
            {
                var List = (from p in db.XT_RM.Where(k => k.State == true && k.Name!="yxf")
                            where !string.IsNullOrEmpty(SModel.Name) ? p.Name.Contains(SModel.Name) : true
                            orderby p.CreateTime descending
                            select new XT_RMModel
                            {
                                Id = p.Id,
                                UId = p.UId,
                                UName = p.Name,
                                DepartmentId = p.DepartmentId,
                                Department = p.Department,
                                StrLeve = p.StrLeve,
                                CreateTime = p.CreateTime
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        //新增和修改仓库设置
        public void AddOrUpdate(XT_RMModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.Id != null && Models.Id != Guid.Empty)
                {
                    var table = db.XT_RM.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.UId = Models.UId;
                    table.Name = Models.UName;
                    table.DepartmentId = Models.DepartmentId;
                    table.Department = Models.Department;
                    table.StrLeve = Models.StrLeve;
                    table.SonLeve = Models.SonLeve;
                    table.SSonLeve = Models.SSonLeve;
                }
                else
                {
                    XT_RM table = new XT_RM();
                    table.Id = Guid.NewGuid();
                    table.UId = Models.UId;
                    table.Name = Models.UName;
                    table.DepartmentId = Models.DepartmentId;
                    table.Department = Models.Department;
                    table.StrLeve = Models.StrLeve;
                    table.SonLeve = Models.SonLeve;
                    table.SSonLeve = Models.SSonLeve;
                    table.CreateTime = DateTime.Now;
                    table.State = true;
                    db.XT_RM.Add(table);
                }
                db.SaveChanges();
            }
        }
        //根据文章Id获取内容
        public XT_RMModel GetDetailById(Guid Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.XT_RM.Where(k => k.Id == Id)
                              select new XT_RMModel
                              {
                                  Id = p.Id,
                                  UId = p.UId,
                                  UName = p.Name,
                                  DepartmentId = p.DepartmentId,
                                  Department = p.Department,
                                  StrLeve = p.StrLeve,
                                  SonLeve = p.SonLeve,
                                  SSonLeve = p.SSonLeve,
                                  CreateTime = p.CreateTime
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(Guid Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.XT_RM.Where(k => k.Id == Id).SingleOrDefault();
                tables.State = false;
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
                        Guid Id = Guid.Parse(item);
                        var tables = db.XT_RM.Where(k => k.Id == Id).SingleOrDefault();
                        tables.State = false;
                    }
                }
                db.SaveChanges();
            }
        }
        public XT_RMURLModel GetRoleByUserId(int UserId)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.XT_RM.Where(k => k.UId == UserId && k.State == true)
                              select new XT_RMURLModel
                              {
                                  StrLeve=p.StrLeve,
                                  SonLeve = p.SonLeve,
                                  SSonLeve = p.SSonLeve,
                              }).SingleOrDefault();
                return tables;
            }
        }
    }
}
