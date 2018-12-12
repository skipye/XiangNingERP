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
    
    public partial class ehr_day
    {
        public long id { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public long employee { get; set; }
        public int department { get; set; }
        public string departmentname { get; set; }
        public System.DateTime date { get; set; }
        public string shiftname { get; set; }
        public string shiftnumber { get; set; }
        public Nullable<int> should { get; set; }
        public Nullable<int> actual { get; set; }
        public Nullable<int> late { get; set; }
        public Nullable<int> leave { get; set; }
        public Nullable<int> absenteeism { get; set; }
        public Nullable<int> ask_leave { get; set; }
        public Nullable<int> overtime { get; set; }
        public Nullable<System.DateTime> time1 { get; set; }
        public Nullable<System.DateTime> time2 { get; set; }
        public Nullable<System.DateTime> time3 { get; set; }
        public Nullable<System.DateTime> time4 { get; set; }
        public Nullable<System.DateTime> time5 { get; set; }
        public Nullable<System.DateTime> time6 { get; set; }
        public Nullable<System.DateTime> time7 { get; set; }
        public Nullable<System.DateTime> time8 { get; set; }
        public Nullable<System.DateTime> time9 { get; set; }
        public Nullable<System.DateTime> time10 { get; set; }
        public Nullable<short> time_mores { get; set; }
        public Nullable<int> rest_overtime { get; set; }
        public Nullable<int> holiday_overtime { get; set; }
        public Nullable<float> should_days { get; set; }
        public Nullable<float> attendance_days { get; set; }
        public Nullable<float> leave_days { get; set; }
        public Nullable<short> leave_time { get; set; }
        public Nullable<short> late_times { get; set; }
        public Nullable<short> leave_early_times { get; set; }
        public Nullable<short> absenteeism_times { get; set; }
        public Nullable<short> up_card_times { get; set; }
        public string mark { get; set; }
        public string times { get; set; }
        public Nullable<short> HaveUpdate { get; set; }
        public Nullable<short> NotAbnormal { get; set; }
        public string leaverecord { get; set; }
        public Nullable<int> halong { get; set; }
        public string haabs { get; set; }
        public Nullable<System.DateTime> t1 { get; set; }
        public Nullable<System.DateTime> t2 { get; set; }
        public Nullable<System.DateTime> t3 { get; set; }
        public Nullable<System.DateTime> t4 { get; set; }
        public Nullable<System.DateTime> t5 { get; set; }
        public Nullable<System.DateTime> t6 { get; set; }
        public Nullable<System.DateTime> st1 { get; set; }
        public Nullable<System.DateTime> st2 { get; set; }
        public Nullable<System.DateTime> st3 { get; set; }
        public Nullable<System.DateTime> st4 { get; set; }
        public Nullable<System.DateTime> st5 { get; set; }
        public Nullable<System.DateTime> st6 { get; set; }
        public string xt1 { get; set; }
        public string xt2 { get; set; }
        public string xt3 { get; set; }
        public string xt4 { get; set; }
        public string xt5 { get; set; }
        public string xt6 { get; set; }
    }
}
