using System;
using System.Collections.Generic;
using System.Text;
using StudentScore.IRepository;
using StudentScore.Models;

namespace StudentScore.Repository
{
    public class UserRepository:BaseRepository<Users>,IUserRepository
    {
        public UserRepository():base(new UnitWork(new StudentScoreContext()))
        {
            
        }

        public UserRepository(IUnitWork unitWork):base(unitWork)
        {
            
        }
    }
}
