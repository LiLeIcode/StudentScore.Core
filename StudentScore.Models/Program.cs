using System;
using System.Linq;

namespace StudentScore.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentScoreContext db = new StudentScoreContext();
            long id = db.Set<Users>().FirstOrDefault(x => x.Account.Equals("lilei")).ID;
            long roleId = db.Set<Roles>().FirstOrDefault(x => x.RoleName.Equals("student")).ID;
            db.UserRole.Add(new UserRole()
            {
                UserId = id,
                RoleId = roleId
            });
            //db.Users.Add(new Users()
            //{
            //    Account = "lilei",
            //    Password = "lilei"
            //});
            //db.Roles.Add(new Roles()
            //{
            //    RoleName = "student"
            //});
            //db.StudentInfo.Add(new StudentInfo()
            //{
            //    Name = "李雷",
            //    Age = 20,
            //    Sex = '男',
            //    IsRemove = false,
            //    StudentNumber = "1733150115",
            //    ReportCard = new ReportCard()
            //    {
            //        Chinese = 91,
            //        Math = 92,
            //        English = 93
            //    },
            //    AllStudentClass = new AllStudentClass()
            //    {
            //        Grades = "大一计算机1班"
            //    }
            //});
            //db.Users.Add(new Users()
            //{
            //    Account = "123456",Password = "123456"
            //});
            db.SaveChanges();
        }
    }
}
