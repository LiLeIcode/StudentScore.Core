using StudentScore.IRepository;
using StudentScore.Models;

namespace StudentScore.Repository
{
    public class StudentClassRepository:BaseRepository<StudentClass>,IStudentClassRepository
    {
        public StudentClassRepository(IUnitWork unitWork) : base(unitWork)
        {
        }

        public StudentClassRepository():base(new UnitWork(new StudentScoreContext()))
        {
            
        }
    }
}
