using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Payment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {

        public SubscriptionController()
        {
        }

        [HttpGet]
        public async Task Get()
        {

        }
    }
}
