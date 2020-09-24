using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentScore.Extensions.Authorizations;
using StudentScore.IService;
using StudentScore.Models;
using StudentScore.Models.ResponseModels;

namespace StudentScore.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;

        public LoginController(IUserService userService, IUserRoleService userRoleService, IRoleService roleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        /// <summary>
        /// 登录拿token 例：account:123456   password:123456
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("token")]
        [HttpPost]
        public async Task<MessageModel<string>> PostToken([FromBody]LoginUser user)
        {
            if (!ModelState.IsValid)
            {
                return new MessageModel<string>()
                {
                    success = false,
                    msg = "账号或密码不能为空"
                };
            }
            try
            {
                Users users = await _userService.QueryExp(x => x.Account == user.Account && x.Password == user.Password && x.IsRemove == false).FirstOrDefaultAsync();
                UserRole userRoles = await _userRoleService.QueryExp(x => x.UserId == users.ID).FirstOrDefaultAsync();
                //改角色的权限
                Roles roleName = await _roleService.QueryById(userRoles.RoleId);

                Claim[] claims = new Claim[]
                {
                    new Claim(JwtClaimTypes.Id, users.ID.ToString()),
                    new Claim(JwtClaimTypes.Name, user.Account),
                    new Claim(JwtClaimTypes.Role, roleName.RoleName),
                };
                var nbf = DateTime.UtcNow;//生效时间
                var exp = DateTime.UtcNow.AddSeconds(1000);//过期时间
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigField.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken jwt = new JwtSecurityToken(issuer: ConfigField.Iss, audience: ConfigField.Aud, claims: claims, notBefore: nbf, expires: exp,
                    signingCredentials: credentials);
                var tokenHandler = new JwtSecurityTokenHandler();
                string token = tokenHandler.WriteToken(jwt);
                return new MessageModel<string>()
                {
                    msg = "请求成功",
                    success = true,
                    response = token
                };
            }
            catch (Exception e)
            {
                return new MessageModel<string>()
                {
                    msg = "请求失败",
                    success = false
                };
            }
        }

    }

}
