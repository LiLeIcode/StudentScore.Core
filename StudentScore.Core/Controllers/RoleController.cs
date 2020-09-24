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
    [Authorize(Policy = "AdminRole")]
    [EnableCors("cors")]
    public class RoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        public RoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [Route("addRole")]
        [HttpGet]
        public async Task<MessageModel<string>> AddRole(long userId, long roleId)
        {
            var userRoles = await _userRoleService.QueryExp(x => x.UserId == userId).ToListAsync();
            if (userRoles.Count <= 0)
            {
                var userRoleAdd = await _userRoleService.Add(new UserRole()
                {
                    UserId = userId,
                    RoleId = roleId
                });
                return new MessageModel<string>()
                {
                    msg = "添加成功",
                    success = true,
                    response = userRoleAdd.ToString()
                };
            }
            else
            {
                return new MessageModel<string>()
                {
                    msg = "该用户已有角色",
                    success = true,
                };
            }
        }



        /// <summary>
        /// 修改用户的Role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [Route("updateRole")]
        [HttpGet]
        public async Task<MessageModel<string>> UpdateRole(long userId,long roleId)
        {
            var userRoles = await _userRoleService.QueryExp(x => x.UserId == userId).ToListAsync();
            if (userRoles.Count<=0)
            {
                return new MessageModel<string>()
                {
                    msg = "修改失败",
                    success = false,
                    response = "该用户没有权限可以修改"
                };
            }
            else
            {
                UserRole userRole = userRoles.FirstOrDefault(x => x.UserId == userId);
                if (userRole != null)
                {
                    userRole.RoleId = roleId;
                    var update = await _userRoleService.Update(userRole);
                    return new MessageModel<string>()
                    {
                        msg = "修改成功",
                        success = true,
                        response = update.ToString()
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "修改失败",
                        success = false,
                    };
                }
            }
        }
    }
}
