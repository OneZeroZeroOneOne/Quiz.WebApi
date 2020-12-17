using System;
using System.Collections.Generic;
using System.Text;
using Tests.WebApi.Dal.Models;

namespace Tests.WebApi.Dal.Out
{
    public class OutEmployeeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public int Salary { get; set; }
        public string SotialNetworks { get; set; }
        public string Avatar { get; set; }
        public string Adress { get; set; }
        public string Resume { get; set; }
        public List<OutQuizViewModel> Quizzes {get; set;}
    }
}
