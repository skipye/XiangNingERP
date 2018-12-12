using Infrastructure;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DalProject
{
    public class ToExcelDal
    {
        public DataTable HRToExcel()
        {
            string strSql = string.Format(@"select m.id as Id ,m.name as Name ,m.tel as TelPhone,m.departmentname as DepartmentName
                        ,n.d_start as HRTime,n.d1,n.d2,n.d3,n.d4,n.d5,n.d6,n.d7,n.d8,n.d9,n.d10,n.d11,n.d12,n.d13,n.d14,n.d15,n.d16,n.d17,n.d18,n.d19,n.d20,
                        n.d21,n.d22,n.d23,n.d24,n.d25,n.d26,n.d27,n.d28,n.d29,n.d30,n.d31
                        from ehr_employee m left join ehr_sum n on m.id=n.employee 
                        where m.status='1' and m.branch='03'");
            DataTable Exceltable = new DataTable();
            using (var db = new XNHREntities())
            {
                var table = db.Database.SqlQuery<HRExcelModel>(strSql);
                if (table != null && table.Count() > 0)
                {
                    Exceltable.Columns.Add("部门", typeof(string));
                    Exceltable.Columns.Add("姓名", typeof(string));
                    Exceltable.Columns.Add("工号", typeof(int));
                    Exceltable.Columns.Add("联系电话", typeof(string));
                    Exceltable.Columns.Add("月份", typeof(string));
                    Exceltable.Columns.Add("1", typeof(string));
                    Exceltable.Columns.Add("2", typeof(string));
                    Exceltable.Columns.Add("3", typeof(string));
                    Exceltable.Columns.Add("4", typeof(string));
                    Exceltable.Columns.Add("5", typeof(string));
                    Exceltable.Columns.Add("6", typeof(string));
                    Exceltable.Columns.Add("7", typeof(string));
                    Exceltable.Columns.Add("8", typeof(string));
                    Exceltable.Columns.Add("9", typeof(string));
                    Exceltable.Columns.Add("10", typeof(string));
                    Exceltable.Columns.Add("11", typeof(string));
                    Exceltable.Columns.Add("12", typeof(string));
                    Exceltable.Columns.Add("13", typeof(string));
                    Exceltable.Columns.Add("14", typeof(string));
                    Exceltable.Columns.Add("15", typeof(string));
                    Exceltable.Columns.Add("16", typeof(string));
                    Exceltable.Columns.Add("17", typeof(string));
                    Exceltable.Columns.Add("18", typeof(string));
                    Exceltable.Columns.Add("19", typeof(string));
                    Exceltable.Columns.Add("20", typeof(string));
                    Exceltable.Columns.Add("21", typeof(string));
                    Exceltable.Columns.Add("22", typeof(string));
                    Exceltable.Columns.Add("23", typeof(string));
                    Exceltable.Columns.Add("24", typeof(string));
                    Exceltable.Columns.Add("25", typeof(string));
                    Exceltable.Columns.Add("26", typeof(string));
                    Exceltable.Columns.Add("27", typeof(string));
                    Exceltable.Columns.Add("28", typeof(string));
                    Exceltable.Columns.Add("29", typeof(string));
                    Exceltable.Columns.Add("30", typeof(string));
                    Exceltable.Columns.Add("31", typeof(string));
                    foreach (var item in table)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["部门"] = item.DepartmentName;
                        row["姓名"] = item.Name;
                        row["工号"] = item.Id;
                        row["联系电话"] = item.TelPhone;
                        row["月份"] = Convert.ToDateTime(item.HRTime).ToString("yyyy-MM-dd");
                        row["1"] = item.d1;
                        row["2"] = item.d2;
                        row["3"] = item.d3;
                        row["4"] = item.d4;
                        row["5"] = item.d5;
                        row["6"] = item.d6;
                        row["7"] = item.d7;
                        row["8"] = item.d8;
                        row["9"] = item.d9;
                        row["10"] = item.d10;
                        row["11"] = item.d11;
                        row["12"] = item.d12;
                        row["13"] = item.d13;
                        row["14"] = item.d14;
                        row["15"] = item.d15;
                        row["16"] = item.d16;
                        row["17"] = item.d17;
                        row["18"] = item.d18;
                        row["19"] = item.d19;
                        row["20"] = item.d20;
                        row["21"] = item.d21;
                        row["22"] = item.d22;
                        row["23"] = item.d23;
                        row["24"] = item.d24;
                        row["25"] = item.d25;
                        row["26"] = item.d26;
                        row["27"] = item.d27;
                        row["28"] = item.d28;
                        row["29"] = item.d29;
                        row["30"] = item.d30;
                        row["31"] = item.d31;
                        Exceltable.Rows.Add(row);
                    }
                }
                
            }
            return Exceltable;
        }
        public List<HRExcelModel> GetMouthHRList(string TypeId)
        {
             string strSql = string.Format(@"select m.id as Id ,m.name as Name ,m.tel as TelPhone,m.departmentname as DepartmentName
                        ,n.d_start as HRTime,n.d1,n.d2,n.d3,n.d4,n.d5,n.d6,n.d7,n.d8,n.d9,n.d10,n.d11,n.d12,n.d13,n.d14,n.d15,n.d16,n.d17,n.d18,n.d19,n.d20,
                        n.d21,n.d22,n.d23,n.d24,n.d25,n.d26,n.d27,n.d28,n.d29,n.d30,n.d31
                        from ehr_employee m left join ehr_sum n on m.id=n.employee 
                        where m.status='1' and m.branch='{0}'", TypeId);
             List<HRExcelModel> List = new List<HRExcelModel>();
            using (var db = new XNHREntities())
            {
                List = db.Database.SqlQuery<HRExcelModel>(strSql).ToList();
            }
            return List;
        }
    }
}
