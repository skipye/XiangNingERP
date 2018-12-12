using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    public class SemiModel
    {
        public int id { get; set; }
        public string productName { get; set; }
        public string ProductXL { get; set; }
        public int product_id { get; set; }
        public int wood_id { get; set; }
        public string woodname { get; set; }
        public int? inv_id { get; set; }
        public string invname { get; set; }
        public DateTime? input_date { get; set; }
        public int? input_user_id { get; set; }
        public string input_user_name { get; set; }
        public int status { get; set; }
        public int? CRM_id { get; set; }
        public int? WIP_id { get; set; }
        public int? product_SN_id { get; set; }
        public int? product_area_id { get; set; }
        public int? product_SN { get; set; }
        public int? product_area { get; set; }
        public string areaName { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> AreaDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
        public SSemiModel SModel { get; set; }
        public List<SelectListItem> CKDroList { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public string Remark { get; set; }
        public string ListId { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? qty { get; set; }
        public decimal? W_BZ { get; set; }
        public decimal? volume { get; set; }
        public decimal? W_price { get; set; }
    }
    public class SSemiModel
    {
        public int product_id { get; set; }
        public string productName { get; set; }
        public int wood_id { get; set; }
        public int inv_id { get; set; }
        public DateTime? input_date { get; set; }
        public int? status { get; set; }
        public int? product_SN_id { get; set; }
        public int? product_area_id { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> AreaDroList { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public List<SelectListItem> CKDroList { get; set; }
        public int ProState { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
