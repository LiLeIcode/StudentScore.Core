using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentScore.IRepository;
using StudentScore.IService;
using StudentScore.Models;

namespace StudentScore.Service
{
    public class UserService:BaseService<Users>,IUserService
    {
        private IUserRepository _dal;

        public UserService(IUserRepository dal)
        {
            _dal = dal;
            base.BaseDal = dal;
        }
        /// <summary>
        /// 添加用户时监测account是否存在
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task<long> AddUserInfo(Users users)
        {
            var queryExp = _dal.QueryExp(x => x.Account == users.Account)?.ToListAsync();
            if (queryExp.Result.Count<1)
            {
                return await _dal.Add(users);
            }
            else
            {
                return -1;
            }
        }
    }
}
