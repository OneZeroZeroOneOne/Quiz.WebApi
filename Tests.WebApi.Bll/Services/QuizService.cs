using System;
using System.Collections.Generic;
using System.Text;
using Tests.WebApi.Contexts;
using Tests.WebApi.Dal.Models;

namespace Tests.WebApi.Bll.Services
{
    public class QuizService
    {
        public MainContext _context;
        public QuizService(MainContext context)
        {
            _context = context;
        }


        public void CreateNewQuiz(Employee emp)
        {

        }
    }
}
