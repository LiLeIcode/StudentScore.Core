using System;
using System.Collections.Generic;
using System.Text;
using StudentScore.IRepository;
using StudentScore.IService;
using StudentScore.Models;

namespace StudentScore.Service
{
    public class RoleService:BaseService<Roles>,IRoleService
    {
        private IRoleRepository _dal;

        public RoleService(IRoleRepository dal)
        {
            _dal = dal;
            base.BaseDal = dal;

        }
    }
}
