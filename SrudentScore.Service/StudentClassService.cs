using System;
using System.Collections.Generic;
using System.Text;
using StudentScore.IRepository;
using StudentScore.IService;
using StudentScore.Models;

namespace StudentScore.Service
{
    public class StudentClassService:BaseService<StudentClass>,IStudentClassService
    {
        private IStudentClassRepository _dal;
        public StudentClassService(IStudentClassRepository dal)
        {
            _dal = dal;
            base.BaseDal = dal;
        }
    }
}
