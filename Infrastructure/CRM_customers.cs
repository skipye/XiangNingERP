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
    
    public partial class CRM_customers
    {
        public CRM_customers()
        {
            this.CRM_contract_header = new HashSet<CRM_contract_header>();
            this.INV_labels = new HashSet<INV_labels>();
        }
    
        public int id { get; set; }
        public string SN { get; set; }
        public string name { get; set; }
        public string name_E { get; set; }
        public string shortname { get; set; }
        public string shortname_E { get; set; }
        public string address { get; set; }
        public string address_E { get; set; }
        public string address_delivery { get; set; }
        public string zipcode { get; set; }
        public string corporation { get; set; }
        public string linkman { get; set; }
        public string tel { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string account { get; set; }
        public string website { get; set; }
        public string remark { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
        public string ListUserId { get; set; }
    
        public virtual ICollection<CRM_contract_header> CRM_contract_header { get; set; }
        public virtual ICollection<INV_labels> INV_labels { get; set; }
    }
}
