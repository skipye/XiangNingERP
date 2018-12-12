using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    public class SYS_SHModel
    {
        public int id { get; set; }
        public string SN { get; set; }
        public string name { get; set; }
        public string RGB { get; set; }
        public string img { get; set; }
        public string remark { get; set; }
        public DateTime? created_time { get; set; }
    }
    public class SSYS_SHModel
    {
        public string name { get; set; }
    }
    public class SYS_XLModel
    {
        public int id { get; set; }
        public string SN { get; set; }
        public string name { get; set; }
        public string remark { get; set; }
        public DateTime? created_time { get; set; }
    }
    public class SSYS_XLModel
    {
        public string name { get; set; }
    }
    public class SYS_CPMCModel
    {
        public int id { get; set; }
        public int product_SN_id { get; set; }
        public string product_SN_name { get; set; }
        public string Pname { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public decimal? price { get; set; }
        public string remark { get; set; }
        public string ConvertImg { get; set; }
        public string paper_path { get; set; }
        public string BOM_path { get; set; }
        public int? product_area_id { get; set; }
        public string product_area_name { get; set; }
        public decimal? volume { get; set; }
        public DateTime? created_time { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> AreaDroList { get; set; }
        public SSYS_CPMCModel SModel { get; set; }
        public int? PersonPrice { get; set; }
    }
    public class SSYS_CPMCModel
    {
        public int? Id { get; set; }
        public string name { get; set; }
        public int? product_SN_id { get; set; }
        public int? product_area_id { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> AreaDroList { get; set; }
        public int? PageIndex { get; set; }
    }
}
