using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    public class HRModel
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string ArrUser { get; set; }
        public string user_name { get; set; }
        public int? department_id { get; set; }
        public string department { get; set; }
        public byte? salary_type { get; set; }
        public decimal? amount { get; set; }
        public string socialsecurity { get; set; }
        public decimal? society_insure { get; set; }
        public string jobs { get; set; }
        public DateTime? jobtime { get; set; }
        public List<SelectListItem> UserList { get; set; }
        public int? status { get; set; }
        public int? PageIndex { get; set; }
        public string GJJ { get; set; }
    }
    public class SHRModel
    {
        public string userName { get; set; }
        public int? PageIndex { get; set; }
        public int? salary_type { get; set; }
        public string socialsecurity { get; set; }
        public int? status { get; set; }
    }
}
