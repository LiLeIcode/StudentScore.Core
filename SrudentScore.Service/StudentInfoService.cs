using System;
using System.Collections.Generic;
using System.Text;
using StudentScore.IRepository;
using StudentScore.IService;
using StudentScore.Models;

namespace StudentScore.Service
{
    public class StudentInfoService:BaseService<StudentInfo>,IStudentInfoService
    {
        private IStudentInfoRepository _dal;
        public StudentInfoService(IStudentInfoRepository dal)
        {
            _dal = dal;
            base.BaseDal = dal;
        }
    }
}
