using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    //仓库设置
    public class INVCKModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public byte typeId { get; set; }
        public string address { get; set; }//仓库位置
        
        public string remark { get; set; }//仓库功能说明
        public DateTime? created_time { get; set; }
    }
    public class INVCKSerModel
    {
        public string name { get; set; }
        public int typeId { get; set; }
    }
    //独板model
    public class OnlyboardModel
    {
        public int id { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public string weight { get; set; }
        public decimal? price { get; set; }
        public string remark { get; set; }
        public DateTime? created_time { get; set; }
        public int? inventory_id { get; set; }
        public string inventoryName { get; set; }
        public List<SelectListItem> CKDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
        public int? Wood_typeId { get; set; }
        public int? qty { get; set; }
        public string WoodName { get; set; }
    }
    public class INVDBSModel
    {
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public int? inventory_id { get; set; }
        public int? Wood_typeId { get; set; }
        public List<SelectListItem> CKDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
    //木材设置
    public class INV_MCModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public string place { get; set; }
        public string remark { get; set; }//仓库功能说明
        public DateTime? created_time { get; set; }
        public decimal? g_ccl { get; set; }
        public decimal? g_bz { get; set; }
        public decimal? q_ccl { get; set; }
        public decimal? q_bz { get; set; }
        public decimal? prcie { get; set; }
        public decimal? cc_prcie { get; set; }
        public decimal? volume { get; set; }
        public decimal? personprice { get; set; }
    }
    public class ReckonModel
    {
        public int? product_id { get; set; }
        public int? product_SN_id { get; set; }
        public int? product_area_id { get; set; }
        public int? wood_id { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> AreaDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
        public decimal? g_ccl { get; set; }
        public decimal? g_bz { get; set; }
        public decimal? q_ccl { get; set; }
        public decimal? q_bz { get; set; }
        public decimal? prcie { get; set; }
        public decimal? cc_prcie { get; set; }
        public decimal? volume { get; set; }
        public decimal? personprice { get; set; }
    }
    public class SINV_MCModel
    {
        public string name { get; set; }
    }
    //辅料种类设置
    public class INV_FLZLModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string unit { get; set; }
        public string remark { get; set; }//仓库功能说明
        public DateTime? created_time { get; set; }
        public int? catalogId { get; set; }
        public string catalogName { get; set; }
        public List<SelectListItem> catalogDroList { get; set; }
    }
    public class SINV_FLZLModel
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string name { get; set; }
        public int? catalogId { get; set; }
        public string catalogName { get; set; }
        public List<SelectListItem> catalogDroList { get; set; }
    }
    //辅料入库
    public class INV_FLModel
    {
        public int id { get; set; }
        public int? units_id { get; set; }
        public string FLZL { get; set; }
        public string unitName { get; set; }
        public decimal qty { get; set; }//购买数量
        public decimal? price { get; set; }
        public string INVName { get; set; }
        public string accessoryName { get; set; }
        public string remark { get; set; }//说明
        public DateTime? created_time { get; set; }
        public List<SelectListItem> unitList { get; set; }
        public int accessory_type_id { get; set; }
        public List<SelectListItem> FLZLDroList { get; set; }
        public int inventory_id { get; set; }
        public List<SelectListItem> CKDroList { get; set; }
        public decimal? W_count { get; set; }
        public Guid CKId { get; set; }
    }
    public class SINV_FLModel
    {
        public string accessoryname { get; set; }
        public int? accessory_type_id { get; set; }
        public List<SelectListItem> FLZLDroList { get; set; }
        public int? inventory_id { get; set; }
        public List<SelectListItem> CKDroList { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public Guid Id { get; set; }
        public decimal? Oldqty { get; set; }
        public decimal? qty { get; set; }
    }
    //主料入库
    public class INV_ZLModel
    {
        public int id { get; set; }
        public int? qty { get; set; }//购买数量
        public decimal? price { get; set; }
        public string INVName { get; set; }
        public string WoodName { get; set; }
        public string remark { get; set; }//说明
        public DateTime? created_time { get; set; }
        public int? WoodId { get; set; }
        public List<SelectListItem> WoodDroList { get; set; }
        public int? inventory_id { get; set; }
        public List<SelectListItem> CKDroList { get; set; }
    }
    public class SINV_ZLModel
    {
        public int? WoodId { get; set; }
        public List<SelectListItem> WoodDroList { get; set; }
        public int? inventory_id { get; set; }
        public List<SelectListItem> CKDroList { get; set; }
    }
    public class CheckOut
    {
        public int Id { get; set; }
        public int Oldqty { get; set; }
        public int qty { get; set; }
    }
}
