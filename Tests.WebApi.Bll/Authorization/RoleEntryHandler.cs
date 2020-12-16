using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Tests.WebApi.Bll.Authorization;
using Tests.WebApi.Dal.Models;

namespace Tests.WebApi.Bll.Authorization
{
    public class RoleEntryHandler : AuthorizationHandler<RoleEntryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            RoleEntryRequirement requirement)
        {
            var myUser = (AuthorizedUserModel)context.User.Identity;
            if (myUser != null)
            {
                if (myUser.RoleId == requirement.RoleId)
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                context.Fail();
            }
            
            return Task.CompletedTask;
        }
    }
  
}
