using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelProject
{
    public class UserLogsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserCard { get; set; }
        public int? UserId { get; set; }
        public string Evenlog { get; set; }
        public DateTime? CreateTime { get; set; }
    }
    public class UserCurrentModel
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RealName { get; set; }
        public string SonLeve { get; set; }
        public string SSonLeve { get; set; }
    }
}
