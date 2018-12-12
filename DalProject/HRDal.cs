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
    public class HRDal
    {
        public PagedList<HRModel> GetPageList(SHRModel SModel, int PageIndex, int PageSize)
        { 
            using(var db=new XNHREntities())
            {
                var List = (from p in db.ehr_employee
                            where !string.IsNullOrEmpty(SModel.userName) ? p.name.Contains(SModel.userName) : true
                            where SModel.salary_type>=0 ? p.salary_type==SModel.salary_type : true
                            where !string.IsNullOrEmpty(SModel.socialsecurity) ? p.socialsecurity==SModel.socialsecurity : true
                            where SModel.status >= 0 ? p.status == SModel.status : true
                            orderby p.id descending
                            select new HRModel
                            {
                                id = p.id,
                                user_name = p.name,
                                department_id = p.department,
                                department = p.departmentname,
                                salary_type = p.salary_type,
                                amount = p.amount,
                                socialsecurity = p.socialsecurity,
                                society_insure = p.society_insure,
                                jobtime = p.jobtime,
                                jobs = p.jobs,
                                GJJ=p.GJJ,
                                status=p.status
                            }).ToList();
                return List.ToPagedList(PageIndex, PageSize);
            }
        }
        public void AddOrUpdate(HRModel Models)
        {
            using (var db = new XNHREntities())
            {
                if (Models.id != null && Models.id>0)
                {
                    var table = db.ehr_employee.Where(k => k.id == Models.id).SingleOrDefault();
                    table.amount = Models.amount;
                    table.salary_type = Models.salary_type;
                    table.society_insure = Models.society_insure;
                    table.socialsecurity = Models.socialsecurity;
                    table.GJJ = Models.GJJ;
                }
                
                db.SaveChanges();

            }
        }
        //根据文章Id获取内容
        public HRModel GetDetailById(int Id)
        {
            using (var db = new XNHREntities())
            {
                var tables = (from p in db.ehr_employee.Where(k => k.id == Id)
                              select new HRModel
                              {
                                  id = p.id,
                                  user_id = p.id,
                                  user_name = p.name,
                                  department_id = p.department,
                                  department = p.departmentname,
                                  salary_type = p.salary_type,
                                  amount = p.amount,
                                  society_insure = p.society_insure,
                                  socialsecurity = p.socialsecurity,
                                  GJJ=p.GJJ,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteOne(Guid Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = db.HR_salary_info.Where(k => k.id == Id).SingleOrDefault();
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
                        Guid Id = Guid.Parse(item);
                        var tables = db.HR_salary_info.Where(k => k.id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
