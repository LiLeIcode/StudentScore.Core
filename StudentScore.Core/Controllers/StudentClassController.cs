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

namespace StudentScore.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "UserRole")]
    [EnableCors("cors")]
    public class StudentClassController : ControllerBase
    {
        private readonly IStudentClassService _studentClassService;

        public StudentClassController(IStudentClassService studentClassService)
        {
            _studentClassService = studentClassService;
        }
        /// <summary>
        /// 新增班级
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        [Route("addClass")]
        [HttpPost]
        public async Task<MessageModel<long>> PostAddClass(string className)
        {
            long add = await _studentClassService.Add(new StudentClass()
            {
                Grades = className
            });
            return new MessageModel<long>()
            {
                msg = "添加成功",
                status = 200,
                success = true,
                response = add
            };
        }
    }
}
