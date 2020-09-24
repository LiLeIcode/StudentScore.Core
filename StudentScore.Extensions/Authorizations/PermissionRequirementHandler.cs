//using System.Net;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using StudentScore.IService;
//using StudentScore.Models;

//namespace StudentScore.Extensions.Authorizations
//{
//    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
//    {
//        protected override  Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
//        {

//            Claim claim = context.User.FindFirst(x => x.Type == ClaimTypes.Role);
//            if (claim!=null)
//            {
//                string value = claim.Value;
//                if (value == requirement._role)
//                {
//                    context.Succeed(requirement);
//                }
//            }
//            return Task.CompletedTask;
//        }
//    }
//}