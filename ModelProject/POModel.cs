using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    
    public class PO_ACModel
    {
        public int id { get; set; }
        public string SN { get; set; }
        public string ArrUser { get; set; }
        public int apply_id { get; set; }
        public string apply_name { get; set; }
        public string apply_department { get; set; }
        public DateTime? require_date { get; set; }
        public int accessory_id { get; set; }
        public string accessory_name { get; set; }
        public decimal? qty { get; set; }
        public string unit { get; set; }
        public decimal? price { get; set; }
        public string remark { get; set; }
        public int? supplier_id { get; set; }
        public string supplier_name { get; set; }
        public int status { get; set; }
        public string check_user_name { get; set; }
        public DateTime? checked_date { get; set; }
        public int? input_user_id { get; set; }
        public string input_user_name { get; set; }
        public DateTime? created_time { get; set; }

        public List<SelectListItem> UserDroList { get; set; }
        public List<SelectListItem> FLDroList { get; set; }
        public List<SelectListItem> GYSDroList { get; set; }
        public SPO_ACModel SModel { get; set; }
        public int? FR_flag { get; set; }

        public string accessoryname { get; set; }
    }
    public class SPO_ACModel
    {
        public string SN { get; set; }
        public int? accessory_id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CheckState { get; set; }
        public List<SelectListItem> UserDroList { get; set; }
        public List<SelectListItem> FLDroList { get; set; }
        public List<SelectListItem>GYDroList { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? supplier_id { get; set; }
        public int? FR_flag { get; set; }
        public string supplier_name { get; set; }
    }
}
