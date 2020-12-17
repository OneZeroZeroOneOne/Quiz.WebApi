using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.WebApi.Dal.Out
{
    public class OutQuizViewModel
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string AddressKey { get; set; }
        public OutStatusViewModel Status { get; set; }
    }
}
