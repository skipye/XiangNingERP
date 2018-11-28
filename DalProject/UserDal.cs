using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;
using ModelProject;
using System.Web.Mvc;
using System.Web.Security;

namespace DalProject
{
    public class UserDal
    {
        private static readonly UserLogsDal ULDal = new UserLogsDal();
        //用户登录
        public LoginModel IsLogin(LoginModel models)
        {
            using (var db = new XNHREntities())
            {
                var Tables = (from p in db.ehr_employee.Where(k => k.password ==models.PassWord )
                              where !string.IsNullOrEmpty(models.UserName) ? p.user == models.UserName : true
                              where !string.IsNullOrEmpty(models.Telephone) ? p.tel == models.Telephone : true
                              select p
                             ).FirstOrDefault();
                LoginModel ReturnModel = new LoginModel();
                if (Tables != null)
                {
                    //UserLogsModel ULModels = new UserLogsModel();
                    //ULModels.UserId = Tables.id;
                    //ULModels.UserCard = Tables.number;
                    //ULModels.Evenlog = "会员登录";
                    //ULDal.AddUserLogs(ULModels);

                    
                    ReturnModel.UserName = Tables.name;
                    ReturnModel.IsLogin = true;
                    ReturnModel.userid = Tables.id;
                    ReturnModel.departmentId = Tables.department;
                    ReturnModel.department = Tables.departmentname;
                    ReturnModel.level = Tables.level;
                }
                else { ReturnModel.IsLogin = false; }
                return ReturnModel;
            }
        }
        public UserCurrentModel GetCurrentUserName()
        {
            UserCurrentModel models = new UserCurrentModel();
            var List = FormsAuthentication.GetAuthCookie("MyCookie", false);
            if (System.Web.HttpContext.Current.User.Identity.Name.Contains("|") == false)
            {
                return new UserCurrentModel();
            }
            else
            {
                var CurrentModels = System.Web.HttpContext.Current.User.Identity.Name.Split('|');

                models.UserName = CurrentModels[0];
                models.UserId = Convert.ToInt32(CurrentModels[1]);
                models.RoleName = CurrentModels[2];
                models.DepartmentId = Convert.ToInt32(CurrentModels[3]);
                models.Department = CurrentModels[4];
                models.SonLeve = CurrentModels[5];
                models.SSonLeve = CurrentModels[6];
                return models;
            }
        }
        public List<SelectListItem> GeUserListByJob(string Job)
        {
            using (var db = new XNHREntities())
            {
                var list = (from p in db.ehr_employee.Where(k => k.status == 1)
                            where !string.IsNullOrEmpty(Job)?p.jobs.Contains(Job):true
                            orderby p.jobtime descending
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name,
                                department_id=p.department,
                                department=p.departmentname
                            }).ToList();
                List<SelectListItem> StrItem = new List<SelectListItem>();
                StrItem.Add(new SelectListItem { Text = "请选择", Value = "" });
                foreach (var item in list)
                {
                    StrItem.Add(new SelectListItem { Text = item.Name, Value = item.Id + "#" + item.Name + "#" + item.department_id + "#" + item.department });
                }
                return StrItem;
            }
        }
        public string GetUserDrolistByJob(string Job)
        {
            using (var db = new XNHREntities())
            {
                var list = (from p in db.ehr_employee.Where(k => k.status == 1)
                            where !string.IsNullOrEmpty(Job) ? p.jobs.Contains(Job) : true
                            orderby p.jobtime descending
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name,
                                department_id = p.department,
                                department = p.departmentname
                            }).ToList();
                string NewItme = "";
                foreach (var item in list)
                {
                    var strText = item.Name;
                    var IstrValue = item.Id + "#" + item.Name + "#" + item.department_id + "#" + item.department;
                    NewItme += "<option value=" + IstrValue + ">" + strText + "</option>";
                }
                return NewItme;
            }
        }
        //获取办公室用户
        public List<SelectListItem> GeXTUserListByJob()
        {
            using (var db = new XNHREntities())
            {
                var list = (from p in db.ehr_employee.Where(k => k.status == 1)
                            orderby p.jobtime descending
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name,
                                department_id = p.department,
                                department = p.departmentname
                            }).ToList();
                List<SelectListItem> StrItem = new List<SelectListItem>();
                StrItem.Add(new SelectListItem { Text = "请选择", Value = "" });
                foreach (var item in list)
                {
                    StrItem.Add(new SelectListItem { Text = item.Name, Value = item.Id + "#" + item.Name + "#" + item.department_id + "#" + item.department });
                }
                return StrItem;
            }
        }
        //获取销售部的用户
        public List<CRMItem> GetSaleUserList()
        {
            using (var db = new XNHREntities())
            {
                //&& k.department == 348 || k.department == 349
                var list = (from p in db.ehr_employee.Where(k => k.status == 1 && k.department!=347)
                            orderby p.jobtime descending
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name,
                            }).ToList();
                return list;
            }
        }
        //获取东阳考勤信息
        public List<CRMItem> GetDYUserList()
        {
            string StrNow = DateTime.Now.ToString("yyyy-MM-dd");
            string strSql = string.Format(@"select m.id as Id ,m.name as Name ,m.tel,n.time1,n.time2,n.time3,n.time4,n.time5,n.time6 
                                            from ehr_employee m left join ehr_postday n on m.id=n.employee 
                                            where n.date='{0}' and m.status='1' and m.branch='03'", StrNow);

            using (var db = new XNHREntities())
            {
                var table = db.Database.SqlQuery<CRMItem>(strSql);
                var list = (from p in table
                            select new CRMItem
                            {
                                Id = p.Id,
                                Name = p.Name,
                                tel = p.tel,
                                time1 = p.time1,
                                time2 = p.time2,
                                time3 = p.time3,
                                time4 = p.time4,
                                time5 = p.time5,
                                time6 = p.time6
                            }).ToList();
                return list;
            }
        }
        //所有员工信息列表
        public List<CRMItem> GetUserList()
        {
            string StrNow = DateTime.Now.ToString("yyyy-MM-dd");
            string strSql = string.Format(@"select m.id as Id ,m.name as Name ,m.tel,n.time1,n.time2,n.time3,n.time4,n.time5,n.time6 
                                            from ehr_employee m left join ehr_postday n on m.id=n.employee 
                                            where n.date='{0}' and m.status='1'", StrNow);
            
            using (var db = new XNHREntities())
            {
                var table= db.Database.SqlQuery<CRMItem>(strSql);
                var list = (from p in table
                            select new CRMItem
                            {
                                Id = p.Id,
                                Name = p.Name,
                                tel = p.tel,
                                time1=p.time1,
                                time2=p.time2,
                                time3=p.time3,
                                time4=p.time4,
                                time5=p.time5,
                                time6=p.time6
                            }).ToList();
                return list;
            }
        }
        //根据Uid获取权限列表
        public string GetLeve(int UId)
        {
            using (var db = new XNERPEntities())
            {
                var tabele = db.XT_RM.Where(k => k.UId == UId && k.State == true).Select(k => k.StrLeve).FirstOrDefault();
                return tabele;
            }
        }

    }
}
