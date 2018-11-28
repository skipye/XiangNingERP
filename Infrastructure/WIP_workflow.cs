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
    
    public partial class WIP_workflow
    {
        public int id { get; set; }
        public int wo_id { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        public Nullable<int> department_id { get; set; }
        public string department { get; set; }
        public Nullable<System.DateTime> exp_begin_date { get; set; }
        public Nullable<System.DateTime> exp_end_date { get; set; }
        public Nullable<System.DateTime> act_begin_date { get; set; }
        public Nullable<System.DateTime> act_end_date { get; set; }
        public decimal cost { get; set; }
        public Nullable<int> check_level { get; set; }
        public Nullable<int> checked_user_id { get; set; }
        public string checked_user_name { get; set; }
        public Nullable<System.DateTime> checked_date { get; set; }
        public string checked_reason { get; set; }
        public Nullable<int> Product_Id { get; set; }
        public string reserved2 { get; set; }
        public string reserved3 { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
        public Nullable<int> Wood_Id { get; set; }
    
        public virtual WIP_workorder WIP_workorder { get; set; }
        public virtual SYS_product SYS_product { get; set; }
    }
}
