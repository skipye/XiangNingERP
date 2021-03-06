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
    
    public partial class SYS_product
    {
        public SYS_product()
        {
            this.CRM_contract_detail = new HashSet<CRM_contract_detail>();
            this.INV_semi = new HashSet<INV_semi>();
            this.WIP_contract = new HashSet<WIP_contract>();
            this.WIP_workflow = new HashSet<WIP_workflow>();
            this.INV_labels = new HashSet<INV_labels>();
            this.WIP_work_CW = new HashSet<WIP_work_CW>();
            this.SYS_product_Cost = new HashSet<SYS_product_Cost>();
        }
    
        public int id { get; set; }
        public int product_SN_id { get; set; }
        public Nullable<int> product_area_id { get; set; }
        public string name { get; set; }
        public Nullable<int> length { get; set; }
        public Nullable<int> width { get; set; }
        public Nullable<int> height { get; set; }
        public string picture { get; set; }
        public string paper_path { get; set; }
        public string BOM_path { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> volume { get; set; }
        public string remark { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
        public Nullable<int> reserved1 { get; set; }
        public Nullable<int> reserved2 { get; set; }
        public Nullable<int> reserved3 { get; set; }
        public string reserved4 { get; set; }
        public string reserved5 { get; set; }
    
        public virtual ICollection<CRM_contract_detail> CRM_contract_detail { get; set; }
        public virtual SYS_product_area SYS_product_area { get; set; }
        public virtual SYS_product_SN SYS_product_SN { get; set; }
        public virtual ICollection<INV_semi> INV_semi { get; set; }
        public virtual ICollection<WIP_contract> WIP_contract { get; set; }
        public virtual ICollection<WIP_workflow> WIP_workflow { get; set; }
        public virtual ICollection<INV_labels> INV_labels { get; set; }
        public virtual ICollection<WIP_work_CW> WIP_work_CW { get; set; }
        public virtual ICollection<SYS_product_Cost> SYS_product_Cost { get; set; }
    }
}
