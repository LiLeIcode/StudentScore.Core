using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentScore.Extensions.Authorizations;
using StudentScore.IRepository;
using StudentScore.IService;
using StudentScore.Models;
using StudentScore.Repository;
using StudentScore.Service;

namespace StudentSCore.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string jwt = JwtHelper.IssueJwt(new TokenModelJwt()
            {
                Uid = 1,
                Role = "admin"
            });
            Console.WriteLine(jwt);
            TokenModelJwt modelJwt = JwtHelper.SerializeJwt(jwt);
            Console.WriteLine(modelJwt.Uid);
            Console.WriteLine(modelJwt.Role);
            //IUserService service = new UserService(new UserRepository(new UnitWork(new StudentScoreContext())));
            //long id = await service.Add(new Users()
            //{
            //    Account = "123456",
            //    Password = "123456",
            //});
            //IRoleService roleService = new RoleService(new RoleRepository(new UnitWork(new StudentScoreContext())));
            //long roleId = await roleService.Add(new Roles()
            //{
            //    RoleName = "admin"
            //});
            //IUserRoleService userRoleService = new UserRoleService(new UserRoleRepository(new UnitWork(new StudentScoreContext())));
            //long userRoleId = await userRoleService.Add(new UserRole()
            //{
            //    RoleId = 1,
            //    UserId = 1
            //});
            //Console.WriteLine(id);
            //Console.WriteLine(roleId);
            //Console.WriteLine(userRoleId);
            //IStudentInfoService service = new StudentInfoService(new StudentInfoRepository());
            //var task = await service.Add(new StudentInfo(){
            //    Name = "wangwu",
            //    StudentNumber = "1456984",
            //    Sex = '女',
            //    Age = 19,
            //    ReportCard = new ReportCard()
            //    {
            //        Chinese = 88,
            //        Math = 99,
            //        English = 89
            //    },
            //    AllStudentClass = new AllStudentClass()
            //    {
            //        Grades = "大二3班"
            //    }
            //});
            //Console.WriteLine(task);


            //IStudentInfoRepository repositoryBase = new StudentInfoRepository();
            //var allBase = repositoryBase.QueryAll().ToList();
            //foreach (StudentInfo info in allBase)
            //{
            //    Console.WriteLine(info.Name);
            //}
            //long add = await repositoryBase.Add(new StudentInfo()
            //{
            //    Name = "汪大椎",
            //    StudentNumber = "1236522289",
            //    Sex = '男',
            //    Age = 30,
            //    ReportCardID = 2,
            //    StudentClassID = 2
            //});
            //Console.WriteLine("标记"+add);
            //bool update = await repositoryBase.Update(new StudentInfo()
            //{
            //    ID = 7,
            //    Name = "汪大椎",
            //    StudentNumber = "1236522289",
            //    Sex = '男',
            //    Age = 40,
            //    ReportCardID = 1,
            //    StudentClassID = 1
            //});
            //Console.WriteLine(update);
            //var byId = await repositoryBase.DeleteById(7);
            //bool byId = await repositoryBase.DeleteByObj(new StudentInfo()
            //{
            //    ID = 7,
            //    Name = "汪大椎",
            //});
            //Console.WriteLine(byId);
            //var byId = await repositoryBase.QueryById(1);
            //Console.WriteLine(byId);
        }
    }
}
