using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    public class SearchModel
    {
        public string Pname { get; set; }
        public int? product_SN_id { get; set; }
        public int? product_area_id { get; set; }
        public int wood_id { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> AreaDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? SaleCount { get; set; }
        public int? SemiCount { get; set; }
        public int? LabelsCount { get; set; }
    }
    public class AdminModel
    {
        public int TotalWorker { get; set; }
        public int TodayWorker { get; set; }

        public int TotalWorkProduct { get; set; }
        public int YTWorkProduct { get; set; }
        public int HTWorkProduct { get; set; }

        public int TotalSemi { get; set; }
        public int confirmSemi { get; set; }//确认
        public int NoSemi { get; set; }//未确认

        public int TotalLabel { get; set; }
        public int confirmLabel { get; set; }
        public int NoLabel { get; set; }

        public decimal TotalSale { get; set; }
        public decimal confirmSale { get; set; }
        public decimal YearSale { get; set; }
        public decimal MonthSale { get; set; }

        public decimal? TotalCG { get; set; }
        public decimal? YearCG { get; set; }
        public decimal? MonthCG { get; set; }
    }
}
