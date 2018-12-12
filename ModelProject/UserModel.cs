using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelProject
{
    public class LoginModel
    {
        public string UserName { get; set; }//昵称
        public string PassWord { get; set; }//用户密码
        public string valiCode { get; set; }
        public string Telephone { get; set; }//手机号码
        public int userid { get; set; }
        public int departmentId { get; set; }
        public string department { get; set; }
        public int? level { get; set; }
        public bool IsLogin { get; set; }
    }
}
