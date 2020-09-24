using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StudentScore.Models;

namespace StudentScore.IService
{
    public interface IUserService:IBaseService<Users>
    {
       Task<long> AddUserInfo(Users users);
    }
}
