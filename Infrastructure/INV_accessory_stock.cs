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
    
    public partial class INV_accessory_stock
    {
        public System.Guid Id { get; set; }
        public Nullable<int> accessory_type_id { get; set; }
        public Nullable<int> inventory_id { get; set; }
        public Nullable<decimal> C_count { get; set; }
        public Nullable<decimal> W_count { get; set; }
    
        public virtual INV_accessory_type INV_accessory_type { get; set; }
        public virtual INV_inventories INV_inventories { get; set; }
    }
}
