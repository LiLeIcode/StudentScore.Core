using System;
using System.Collections.Generic;
using System.Text;
using StudentScore.IRepository;
using StudentScore.Models;

namespace StudentScore.Repository
{
    public class StudentInfoRepository:BaseRepository<StudentInfo>,IStudentInfoRepository
    {
        public StudentInfoRepository(IUnitWork unitWork) : base(unitWork)
        {
        }

        public StudentInfoRepository() : base(new UnitWork(new StudentScoreContext()))
        {
        }
    }
}
