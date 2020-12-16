using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tests.WebApi.Bll.Services;

namespace Tests.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        public QuizService _quizService;
        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }
    }
}