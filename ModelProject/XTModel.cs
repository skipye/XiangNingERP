using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    public class XT_RMModel
    {
        public Guid Id { get; set; }
        public int? UId { get; set; }
        public string UName { get; set; }
        public int? DepartmentId { get; set; }
        public string Department { get; set; }
        public string StrLeve { get; set; }
        public DateTime? CreateTime { get; set; }
        public List<SelectListItem> UserList { get; set; }
        public string ArrUser { get; set; }
        public string SonLeve { get; set; }
        public string SSonLeve { get; set; }
    }
    public class SXT_RMModel
    {
        public string Name { get; set; }
    }
    public class XT_RMURLModel
    {
        public string StrLeve { get; set; }
        public string SonLeve { get; set; }
        public string SSonLeve { get; set; }
    }
}
