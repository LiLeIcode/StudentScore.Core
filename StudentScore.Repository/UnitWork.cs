using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentScore.IRepository;
using StudentScore.Models;

namespace StudentScore.Repository
{
    public class UnitWork:IUnitWork
    {
        private readonly DbContext _db;


        public UnitWork(DbContext db)
        {
            _db = db;
        }

        public StudentScoreContext GetDbContext()
        {
            return _db as StudentScoreContext;
        }
    }
}
