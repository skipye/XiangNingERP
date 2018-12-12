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
    
    public partial class INV_semi
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int wood_id { get; set; }
        public string color { get; set; }
        public Nullable<int> inv_id { get; set; }
        public Nullable<System.DateTime> input_date { get; set; }
        public Nullable<int> input_user_id { get; set; }
        public string input_user_name { get; set; }
        public Nullable<System.DateTime> output_date { get; set; }
        public Nullable<int> output_user_id { get; set; }
        public string output_user_name { get; set; }
        public int status { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
        public Nullable<int> CRM_id { get; set; }
        public Nullable<int> WIP_id { get; set; }
        public Nullable<int> Work_id { get; set; }
        public Nullable<int> length { get; set; }
        public Nullable<int> width { get; set; }
        public Nullable<int> height { get; set; }
    
        public virtual INV_inventories INV_inventories { get; set; }
        public virtual INV_wood_type INV_wood_type { get; set; }
        public virtual SYS_product SYS_product { get; set; }
    }
}
