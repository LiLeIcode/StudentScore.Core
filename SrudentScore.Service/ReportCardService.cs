using System;
using System.Collections.Generic;
using System.Text;
using StudentScore.IRepository;
using StudentScore.IService;
using StudentScore.Models;
using StudentScore.Repository;

namespace StudentScore.Service
{
    public class ReportCardService:BaseService<ReportCard>,IReportCardService
    {
        private IReportCardRepository _dal;
        public ReportCardService(IReportCardRepository dal)
        {
            _dal = dal;
            base.BaseDal = dal;
        }
    }
}
