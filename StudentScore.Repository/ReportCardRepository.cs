using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentScore.IRepository;
using StudentScore.Models;

namespace StudentScore.Repository
{
    public class ReportCardRepository : BaseRepository<ReportCard> ,IReportCardRepository
    {
        /// <summary>
        /// 自己创建一个unitWork操作对象
        /// </summary>
        /// <param name="unitWork"></param>
        public ReportCardRepository(IUnitWork unitWork) : base(unitWork)
        {
        }
        /// <summary>
        /// 使用父类的unitWork操作对象
        /// </summary>
        public ReportCardRepository() : base(new UnitWork(new StudentScoreContext()))
        {
        }
    }
}
