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
    
    public partial class HR_salary_info
    {
        public System.Guid id { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public Nullable<int> department_id { get; set; }
        public string department { get; set; }
        public Nullable<byte> salary_type { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<bool> society_insure_flag { get; set; }
        public Nullable<decimal> society_insure { get; set; }
        public string remark { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
    }
}
