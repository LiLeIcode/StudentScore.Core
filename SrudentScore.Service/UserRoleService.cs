using System;
using System.Collections.Generic;
using System.Text;
using StudentScore.IRepository;
using StudentScore.IService;
using StudentScore.Models;

namespace StudentScore.Service
{
    public class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        private IUserRoleRepository _dal;

        public UserRoleService(IUserRoleRepository dal)
        {
            _dal = dal;
            base.BaseDal = dal;

        }
    }
}
