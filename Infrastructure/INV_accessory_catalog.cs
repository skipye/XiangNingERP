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
    
    public partial class INV_accessory_catalog
    {
        public INV_accessory_catalog()
        {
            this.INV_accessory_type = new HashSet<INV_accessory_type>();
        }
    
        public int id { get; set; }
        public string SN { get; set; }
        public string name { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
    
        public virtual ICollection<INV_accessory_type> INV_accessory_type { get; set; }
    }
}
