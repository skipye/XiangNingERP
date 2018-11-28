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
    
    public partial class INV_wood_type
    {
        public INV_wood_type()
        {
            this.CRM_contract_detail = new HashSet<CRM_contract_detail>();
            this.INV_material_stock = new HashSet<INV_material_stock>();
            this.INV_materials = new HashSet<INV_materials>();
            this.INV_semi = new HashSet<INV_semi>();
            this.WIP_contract = new HashSet<WIP_contract>();
            this.INV_labels = new HashSet<INV_labels>();
            this.INV_single_board = new HashSet<INV_single_board>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public string place { get; set; }
        public int safe_stock_max { get; set; }
        public int safe_stock_min { get; set; }
        public string remark { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
        public Nullable<decimal> g_ccl { get; set; }
        public Nullable<decimal> g_bz { get; set; }
        public Nullable<decimal> q_ccl { get; set; }
        public Nullable<decimal> q_bz { get; set; }
        public Nullable<decimal> prcie { get; set; }
        public Nullable<decimal> cc_prcie { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<decimal> PersonPrice { get; set; }
    
        public virtual ICollection<CRM_contract_detail> CRM_contract_detail { get; set; }
        public virtual ICollection<INV_material_stock> INV_material_stock { get; set; }
        public virtual ICollection<INV_materials> INV_materials { get; set; }
        public virtual ICollection<INV_semi> INV_semi { get; set; }
        public virtual ICollection<WIP_contract> WIP_contract { get; set; }
        public virtual ICollection<INV_labels> INV_labels { get; set; }
        public virtual ICollection<INV_single_board> INV_single_board { get; set; }
    }
}
