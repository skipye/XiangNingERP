using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    public class WIP_WOModel
    {
        public int? HTId { get; set; }
        public string HTSN { get; set; }
        public string ProductName { get; set; }
        public string ProductXL { get; set; }
        public string ProductGG { get; set; }
        public DateTime? JHDateTime { get; set; }
        public int id { get; set; }
        public string workorder { get; set; }
        public int qty { get; set; }
        public int status { get; set; }
        public DateTime? created_time { get; set; }
        public string remark { get; set; }
        public string WoodName { get; set;}
        public string Color { get; set; }
        public string SaleName { get; set; }
        public decimal? length { get; set; }
        public decimal? width { get; set; }
        public decimal? height { get; set; }
    }
    public class SWIP_WOModel
    {
        public string HTSN { get; set; }
        public int? IsYT { get; set; }
        public int? status { get; set; }
        public string SaleName { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public int? product_SN_id { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    public class WIP_WOXQModel
    {
        public string HTSN { get; set; }
        public string ProductName { get; set; }
        public string customer { get; set; }
        public string ProductXL { get; set; }
        public int? product_SN_id { get; set; }
        public int id { get; set; }
        public string Name { get; set; }
        public string workorder { get; set; }
        public int? status { get; set; }
        public decimal cost { get; set; }
        public string ArrUser { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public int? department_id { get; set; }
        public string department { get; set; }
        public DateTime? exp_begin_date { get; set; }
        public DateTime? exp_end_date { get; set; }
        public DateTime? act_begin_date { get; set; }
        public DateTime? act_end_date { get; set; }
        public string checked_user_name { get; set; }
        public List<SelectListItem> UserByJob { get; set; }
        public string remark { get; set; }
        public string GongXu { get; set; }
        public string Job { get; set; }
        public int wo_id { get; set; }
        public string source { get; set; }
        public string SaleName { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string ListId { get; set; }
    }
    public class SWIP_WOXQModel
    {
        public string NavName { get; set; }
        public string HTSN { get; set; }
        public int? status { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string GXName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    //操作表
    public class WIP_Even
    {
        public int wo_id { get; set; }
        public string name { get; set; }
        public int? user_id { get; set; }
        public string user_name { get; set; }
        public string event_log { get; set; }
        public string remark { get; set; }
        public DateTime? created_time { get; set; }
    }
    public class WIP_CWModel
    {
        public int Id { get; set; }
        public string style { get; set; }
        public string ProductName { get; set; }
        public string ProductXL { get; set; }
        public string workorder { get; set; }
        public string customer { get; set; }
        public int? qty { get; set; }
        public DateTime? CreateTime { get; set; }
        public string WoodName { get; set; }
        public int? GMqty { get; set; }
        public int? YQqty { get; set; }
        public int? CRM_contract_detail_id { get; set; }
        public int? WIP_contract_id { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
    }
}
