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
    
    public partial class INV_accessory_PO_input
    {
        public int id { get; set; }
        public Nullable<int> PO_id { get; set; }
        public Nullable<int> qty { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<System.DateTime> input_date { get; set; }
        public Nullable<int> inventory_id { get; set; }
        public string remark { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
    }
}
