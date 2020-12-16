using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Tests.WebApi.Bll.Authorization
{
    public class AuthorizedUserModel : IIdentity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string? AuthenticationType { get; }
        public bool IsAuthenticated { get; }
        public string? Name { get; }
    }
}
