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
    
    public partial class INV_labels
    {
        public INV_labels()
        {
            this.CRM_delivery_tmp_header = new HashSet<CRM_delivery_tmp_header>();
        }
    
        public int id { get; set; }
        public string SN { get; set; }
        public string product_SN { get; set; }
        public int product_id { get; set; }
        public string style { get; set; }
        public Nullable<int> wood_id { get; set; }
        public string color { get; set; }
        public int customer_id { get; set; }
        public Nullable<System.DateTime> check_date { get; set; }
        public string company { get; set; }
        public string address { get; set; }
        public string website { get; set; }
        public int status { get; set; }
        public Nullable<int> inv_id { get; set; }
        public Nullable<int> CRM_contract_detail_id { get; set; }
        public Nullable<int> flag { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
        public Nullable<int> WorkId { get; set; }
        public Nullable<int> WIP_contract_id { get; set; }
    
        public virtual SYS_product SYS_product { get; set; }
        public virtual INV_wood_type INV_wood_type { get; set; }
        public virtual INV_inventories INV_inventories { get; set; }
        public virtual CRM_customers CRM_customers { get; set; }
        public virtual CRM_contract_detail CRM_contract_detail { get; set; }
        public virtual ICollection<CRM_delivery_tmp_header> CRM_delivery_tmp_header { get; set; }
    }
}
