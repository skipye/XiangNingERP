using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelProject;
using Infrastructure;

namespace DalProject
{
    public class UserLogsDal
    {
        public void AddUserLogs(UserLogsModel models)
        {
            using(var db=new XNERPEntities())
            {
                var tables = new UserLogs();
                tables.Id = Guid.NewGuid();
                tables.UserId = models.UserId;
                tables.UserCard = models.UserCard;
                tables.Evenlog = models.Evenlog;
                tables.CreateTime = DateTime.Now;
                tables.Name = models.Name;
                db.UserLogs.Add(tables);
                db.SaveChanges();
            }
        }
    }
}
