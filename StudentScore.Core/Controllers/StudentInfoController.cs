using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentScore.IService;
using StudentScore.Models;
using StudentScore.Models.ResponseModels;

namespace StudentScore.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("cors")]
    public class StudentInfoController : ControllerBase
    {
        private readonly IReportCardService _reportCardService;
        private readonly IStudentClassService _studentClassService;
        private readonly IStudentInfoService _studentInfoService;

        public StudentInfoController(IStudentInfoService studentInfoService,IStudentClassService studentClassService,IReportCardService reportCardService)
        {
            _studentInfoService = studentInfoService;
            _studentClassService = studentClassService;
            _reportCardService = reportCardService;
        }
        /// <summary>
        /// 获取所有学生的所有信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AllStudentInfo")]
        public async Task<MessageModel<List<AllStudentInfoModel>>> GetAllStudentInfo()
        {
            List<AllStudentInfoModel> studentAllInfo = new List<AllStudentInfoModel>();
            var studentInfos = await _studentInfoService.QueryAll().ToListAsync();
            var studentClasses = await _studentClassService.QueryAll().ToListAsync();
            var reportCards = await _reportCardService.QueryAll().ToListAsync();
            foreach (StudentInfo info in studentInfos)
            {
                studentAllInfo.Add(new AllStudentInfoModel()
                {
                    Id = info.ID,
                    Name = info.Name,
                    StringNumber = info.StudentNumber,
                    Age = info.Age,
                    Sex = info.Sex,
                    AllStudentClass = new AllStudentClassModel()
                    {
                        Id = info.StudentClassID,
                        Grades = studentClasses.FirstOrDefault(x=>x.ID==info.StudentClassID)?.Grades
                    },
                    StudentReportCard = new StudentReportCardModel()
                    {
                        Id = info.ReportCardID,
                        Chinese = reportCards.FirstOrDefault(x=>x.ID==info.ReportCardID)?.Chinese==null?0: reportCards.FirstOrDefault(x => x.ID == info.ReportCardID).Chinese,
                        Math = reportCards.FirstOrDefault(x=>x.ID==info.ReportCardID)?.Math==null?0: reportCards.FirstOrDefault(x => x.ID == info.ReportCardID).Math,
                        English = reportCards.FirstOrDefault(x=>x.ID==info.ReportCardID)?.English==null?0: reportCards.FirstOrDefault(x => x.ID == info.ReportCardID).English
                    }
                });
            }

            return new MessageModel<List<AllStudentInfoModel>>() 
            {
                msg = "请求成功",
                status = 200,
                success = true,
                response = studentAllInfo
            };

        }
        /// <summary>
        /// 根据班级获得本班所有学生 例：grades=大一计算机1班
        /// </summary>
        /// <param name="grades">大一计算机1班</param>
        /// <returns></returns>
        [Route("GradesStudentInfo")]
        [HttpGet]
        public async Task<MessageModel<List<GradesStudentInfoModel>>> GetGradesStudentInfo(string grades)
        {
            List<GradesStudentInfoModel> allStudentInfo = new List<GradesStudentInfoModel>();
            var studentClass = await _studentClassService.QueryExp(x => x.Grades.Equals(grades)).FirstOrDefaultAsync();
            var studentInfos = await _studentInfoService.QueryExp(x => x.StudentClassID == studentClass.ID).ToListAsync();
            foreach (var info in studentInfos)
            {
                ReportCard reportCard = await _reportCardService.QueryById(info.ReportCardID);
                allStudentInfo.Add(new GradesStudentInfoModel()
                {
                    Grades = grades,
                    GradesStudent = new GradesStudentModel() { Name = info.Name,StudentNumber = info.StudentNumber,Age = info.Age,Sex = info.Sex},
                    StudentReportCard = new StudentReportCardModel()
                    {
                        Chinese = reportCard.Chinese,
                        Math = reportCard.Math,
                        English = reportCard.English
                    }
                });
                
            }
            return new MessageModel<List<GradesStudentInfoModel>>()
            {
                msg = "请求成功",
                status = 200,
                success = true,
                response = allStudentInfo
            };
            
        }
        /// <summary>
        /// 添加新学生
        /// </summary>
        /// <param name="studentInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addStudent")]
        [Authorize(Policy = "UserRole")]
        public async Task<MessageModel<string>> PostAddStudent(StudentInfoModel studentInfo)
        {
            if (ModelState.IsValid)
            {
                var addStudent = await _studentInfoService.Add(new StudentInfo()
                {
                    Name = studentInfo.Name,
                    StudentNumber = studentInfo.StudentNumber,
                    Sex = studentInfo.Sex,
                    Age = studentInfo.Age
                });
                return new MessageModel<string>()
                {
                    msg = "添加学生信息成功",
                    status = 200,
                    success = true,
                    response = addStudent.ToString()
                };
            }
            else
            {
                return new MessageModel<string>()
                {
                    msg = "添加学生信息成功",
                    success = false,
                };
            }
        }
        /// <summary>
        /// 给无班级学生添加班级
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentClassId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "UserRole")]
        [EnableCors("cors")]
        public async Task<MessageModel<bool>> PostStudentClass(long id,long studentClassId)
        {
            StudentInfo studentInfo = await _studentInfoService.QueryById(id);
            studentInfo.StudentClassID = studentClassId;
            bool update = await _studentInfoService.Update(studentInfo);
            return new MessageModel<bool>()
            {
                msg = "添加班级成功",
                response = update,
                success = true,
                status = 200
            };
        }
    }
}
