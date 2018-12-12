using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelProject
{
    public class SuppliersModel
    {
        public int id { get; set; }
        public string SN { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string address { get; set; }
        public string linkman { get; set; }
        public string tel { get; set; }
        public string remark { get; set; }
        public DateTime? created_time { get; set; }
    }
    public class SSuppliersModel
    {
        public string name { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
