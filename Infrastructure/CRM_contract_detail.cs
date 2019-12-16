//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class CRM_contract_detail
    {
        public CRM_contract_detail()
        {
            this.WIP_workorder = new HashSet<WIP_workorder>();
            this.INV_labels = new HashSet<INV_labels>();
            this.CRM_delivery_tmp_header = new HashSet<CRM_delivery_tmp_header>();
            this.WIP_work_CW = new HashSet<WIP_work_CW>();
            this.CRM_delivery_detail = new HashSet<CRM_delivery_detail>();
        }
    
        public int id { get; set; }
        public int header_id { get; set; }
        public int product_id { get; set; }
        public int wood_type_id { get; set; }
        public int color_id { get; set; }
        public string color { get; set; }
        public Nullable<bool> custom_flag { get; set; }
        public Nullable<decimal> length { get; set; }
        public Nullable<decimal> width { get; set; }
        public Nullable<decimal> height { get; set; }
        public Nullable<decimal> price { get; set; }
        public int qty { get; set; }
        public string unit { get; set; }
        public string hardware_part { get; set; }
        public string decoration_part { get; set; }
        public string req_others { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
        public int status { get; set; }
        public Nullable<decimal> price_recommend { get; set; }
        public Nullable<int> LabelsCount { get; set; }
        public Nullable<int> SemiCount { get; set; }
        public Nullable<int> SendCount { get; set; }
    
        public virtual INV_wood_type INV_wood_type { get; set; }
        public virtual SYS_colors SYS_colors { get; set; }
        public virtual ICollection<WIP_workorder> WIP_workorder { get; set; }
        public virtual SYS_product SYS_product { get; set; }
        public virtual CRM_contract_header CRM_contract_header { get; set; }
        public virtual ICollection<INV_labels> INV_labels { get; set; }
        public virtual ICollection<CRM_delivery_tmp_header> CRM_delivery_tmp_header { get; set; }
        public virtual ICollection<WIP_work_CW> WIP_work_CW { get; set; }
        public virtual ICollection<CRM_delivery_detail> CRM_delivery_detail { get; set; }
    }
}
