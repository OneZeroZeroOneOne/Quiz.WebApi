using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.WebApi.Dal.Models
{
    public class AvatarEmployee
    {
        public int AvatarId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Avatar Avatar { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
