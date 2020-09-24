using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentScore.IService;
using StudentScore.Models;

namespace StudentScore.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    [Authorize(Policy = "UserRole")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<MessageModel<string>> RegisterUser(string account,string password)
        {
            if (string.IsNullOrEmpty(account)&&string.IsNullOrEmpty(password))
            {
                return new MessageModel<string>()
                {
                    msg = "账号或者密码不能为空",
                    success = false,
                };
            }
            else
            {
                var addUserInfo = await _userService.AddUserInfo(new Users()
                {
                    Account = account,
                    Password = password
                });
                if (addUserInfo == -1)
                {
                    return new MessageModel<string>()
                    {
                        msg = "该账号已存在",
                        success = false,
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "注册成功",
                        success = true,
                        status = 200,
                        response = "账号:" + account + " 注册成功!"
                    };
                }
            }
            
        }

        /// <summary>
        /// 修改user的密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("updateUser")]
        public async Task<MessageModel<string>> UpdateUserInfo(string account,string oldPassword,string newPassword)
        {
            if (string.IsNullOrEmpty(account) &&string.IsNullOrEmpty(oldPassword) && string.IsNullOrEmpty(newPassword))
            {
                return new MessageModel<string>()
                {
                    msg = "账号或者密码不能为空",
                    success = false,
                };
            }
            else
            {
                var user = await _userService.QueryAll().FirstOrDefaultAsync(x => x.Account == account && x.Password == oldPassword);
                if (user!=null)
                {
                    user.Password = newPassword;
                    var update = await _userService.Update(user);
                    return new MessageModel<string>()
                    {
                        msg = "修改成功",
                        success = true,
                        status = 200,
                        response = update.ToString()
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "找不到此用户",
                        success = false
                    };
                }
            }
        }
    }
}
