using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StudentScore.IService;
using StudentScore.Models;
using StudentScore.Models.ResponseModels;

namespace StudentScore.Core.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "UserRole")]
    [EnableCors("cors")]
    [Produces("application/json")]
    public class OperationController : ControllerBase
    {
        private readonly IStudentInfoService _studentInfoService;
        private readonly IReportCardService _reportCardService;

        public OperationController(IStudentInfoService studentInfoService, IReportCardService reportCardService)
        {
            _studentInfoService = studentInfoService;
            _reportCardService = reportCardService;
        }
        /// <summary>
        /// 修改成绩
        /// 根据学生id查询学生信息查出成绩单id再修改成绩单
        /// </summary>
        /// <param name="studentId">2</param>
        /// <param name="Chinese">88</param>
        /// <param name="English">88</param>
        /// <param name="Math">77</param>
        /// <returns></returns>
        [HttpGet]
        [Route("UpdateScore")]
        public async Task<MessageModel<bool>> UpdateScore(long studentId, int Chinese, int English, int Math)
        {
            StudentInfo student = await _studentInfoService.QueryById(studentId);
            //ReportCard card = await _reportCardService.QueryById(student.ReportCardID);
            
            var update = await _reportCardService.Update(new ReportCard()
            {
                ID = student.ReportCardID,
                Math = Math,
                Chinese = Chinese,
                English = English,
            });

            if (update)
            {
                return new MessageModel<bool>()
                {
                    msg = "修改成功",
                    success = true,
                    status = 200,
                    response = update
                };
            }
            else
            {
                return new MessageModel<bool>()
                {
                    msg = "修改失败",
                    success = false,
                };
            }

        }
        /// <summary>
        /// 修改学生班级
        /// </summary>
        /// <param name="studentId">1</param>
        /// <param name="classId">1</param>
        /// <returns></returns>
        [Route("UpdateClass")]
        [HttpGet]
        public async Task<MessageModel<bool>> UpdateClass(long studentId, long classId)
        {
            StudentInfo studentInfo = await _studentInfoService.QueryById(studentId);
            studentInfo.StudentClassID = classId;
            bool update = await _studentInfoService.Update(studentInfo);
            if (update)
            {
                return new MessageModel<bool>()
                {
                    msg = "修改成功",
                    status = 200,
                    success = true,
                    response = update
                };
            }
            else
            {
                return new MessageModel<bool>()
                {
                    msg = "修改失败",
                    success = false,
                };
            }
            
        }
        /// <summary>
        /// 更改学生信息
        /// </summary>
        /// <param name="studentId">1</param>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("UpdateStudentInfo")]
        [HttpPost]
        public async Task<MessageModel<bool>> UpdateStudentInfo(long studentId,StudentNewInfo info)
        {
            if (ModelState.IsValid)
            {
                StudentInfo studentInfo = await _studentInfoService.QueryById(studentId);
                studentInfo.Name = info.Name;
                studentInfo.StudentNumber = info.StudentNumber;
                studentInfo.Sex = info.Sex;
                studentInfo.Age = info.Age;
                bool update = await _studentInfoService.Update(studentInfo);
                if (update)
                {
                    return new MessageModel<bool>()
                    {
                        msg = "修改成功",
                        status = 200,
                        success = true,
                        response = update
                    };
                }
                else
                {
                    return new MessageModel<bool>()
                    {
                        msg = "修改失败",
                        success = false,
                    };
                }
        }
            else
            {
                return new MessageModel<bool>()
                {
                    msg = "数据模型校验失败",
                    success = false,
                };
}

        }
    }
}
