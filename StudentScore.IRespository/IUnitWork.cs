using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentScore.Models;

namespace StudentScore.IRepository
{
    public interface IUnitWork
    {
        StudentScoreContext GetDbContext();
    }
}
