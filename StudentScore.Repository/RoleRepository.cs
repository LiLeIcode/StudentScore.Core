using System;
using System.Collections.Generic;
using System.Text;
using StudentScore.IRepository;
using StudentScore.Models;

namespace StudentScore.Repository
{
    public class RoleRepository:BaseRepository<Roles>,IRoleRepository
    {
        public RoleRepository(IUnitWork unitWork) : base(unitWork)
        {
        }

        public RoleRepository():base(new UnitWork(new StudentScoreContext()))
        {
            
        }
    }
}
