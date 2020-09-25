using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentScore.IService;
using StudentScore.Models;
using StudentScore.Models.ResponseModels;

namespace StudentScore.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    [Authorize(Policy = "UserRole")]
    public class ReportCardController : ControllerBase
    {
        private IReportCardService _reportCardService;
        private IStudentInfoService _studentInfoService;

        public ReportCardController(IReportCardService reportCardService, IStudentInfoService studentInfoService)
        {
            _reportCardService = reportCardService;
            _studentInfoService = studentInfoService;
        }
        /// <summary>
        /// 添加成绩给学生
        /// </summary>
        /// <param name="studentReport"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addReport")]
        public async Task<MessageModel<bool>> PostAddReport(StudentReportCardModel studentReport)
        {
            if (ModelState.IsValid)
            {
                StudentInfo result = await _studentInfoService.QueryById(studentReport.Id);
                long add = await _reportCardService.Add(
                    new ReportCard()
                    {
                        Chinese = studentReport.Chinese,
                        Math = studentReport.Math,
                        English = studentReport.English
                    }
                );
                result.ReportCardID = add;
                bool update = await _studentInfoService.Update(result);

                return new MessageModel<bool>()
                {
                    msg = "添加成绩完成",
                    status = 200,
                    success = true,
                    response = update
                };
            }
            else
            {
                return new MessageModel<bool>()
                {
                    msg = "添加成绩失败",
                    success = false
                };
            }
        }



    }
}
