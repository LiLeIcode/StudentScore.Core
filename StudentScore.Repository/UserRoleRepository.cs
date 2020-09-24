using System;
using System.Collections.Generic;
using System.Text;
using StudentScore.IRepository;
using StudentScore.Models;

namespace StudentScore.Repository
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IUnitWork unitWork) : base(unitWork)
        {
        }

        public UserRoleRepository():base(new UnitWork(new StudentScoreContext()))
        {
            
        }
    }
}
