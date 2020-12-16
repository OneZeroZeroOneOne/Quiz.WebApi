using Microsoft.AspNetCore.Authorization;

namespace Tests.WebApi.Bll.Authorization
{
    public class RoleEntryRequirement : IAuthorizationRequirement
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int RoleId { get; }

        public RoleEntryRequirement(int roleId)
        {
            RoleId = roleId;
        }
    }
}
