using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    
    //销售合同表
    public class SaleSerModel
    {
        public int Id { get; set; }
        public string SN { get; set; }
        public string Customer { get; set; }
        public string TelPhone { get; set; }
        public DateTime? GoHomeDate { get; set; }
        public DateTime? SaleSerDate { get; set; }//保修日期
        public decimal? Amount { get; set; }//维修金额
        public string SFState { get; set; }//收费情况：不收费/收费
        public string LinkAddress { get; set; }//联系地址
        public string userName { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? CheckedTime { get; set; }
        public int? status { get; set; }
        public string Remark { get; set; }
        public List<SaleSerProModel> SaleSerPro { get; set; }
    }
    public class SSaleSerModel
    {
        public string SN { get; set; }
        public string Customer { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class SaleSerProModel
    {
        public Guid Id { get; set; }
        public int? SaleServiceId { get; set; }
        public string ProductStyle { get; set; }
        public string ProductName { get; set; }
        public string WoodName { get; set; }
        public string QusState { get; set; }
        public string AnState { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Remark { get; set; }
        public List<SelectListItem> WoodDroList { get; set; }
        public int? status { get; set; }
    }
    
}
