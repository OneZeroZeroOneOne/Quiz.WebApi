﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Tests.WebApi.Contexts;
using Tests.WebApi.Dal.Models;
using Tests.WebApi.Utilities.Exceptions;

namespace Tests.WebApi.Bll.Services
{
    public class EmployeeService
    {
        public MainContext _context;
        public QuizService _quizService;

        public EmployeeService(MainContext context, IMapper mapperProfile)
        {
            _context = context;

        }


        public async Task<Employee> GetEmployee(int empId, int userId)
        {
            UserEmployee userEmployee = await _context.UserEmployee.FirstOrDefaultAsync(x => x.EmployeeId == empId);
            if (userEmployee != null && userEmployee.UserId == userId)
            {
                return await _context.Employee.Include(x => x.UserQuizzes).ThenInclude(x => x.Quiz).ThenInclude(x => x.Status).Include(x => x.UserQuizzes).ThenInclude(x => x.Quiz).ThenInclude(x => x.Questions).ThenInclude(x => x.UserAnswers).FirstOrDefaultAsync(x => x.Id == empId);
            }
            return null;
        }

        public async Task<List<Employee>> GetEmployees(int userId)
        {

            List<Employee> userEmployees = await _context.UserEmployee.Where(x => x.UserId == userId).Include(x => x.Employee).ThenInclude(x => x.UserQuizzes).ThenInclude(x => x.Quiz).ThenInclude(x => x.Status).Select(x => x.Employee).ToListAsync();
            return userEmployees;


        }

        public async Task<Employee> AddEmployee(Employee newEmp, int userId)
        {
            await _context.Employee.AddAsync(newEmp);
            await _context.SaveChangesAsync();
            UserEmployee ue = new UserEmployee()
            {
                UserId = userId,
                EmployeeId = newEmp.Id,
            };
            await _context.UserEmployee.AddAsync(ue);
            await _context.SaveChangesAsync();
            return newEmp;
        }

        public async Task<List<Employee>> AddEmployees(List<Employee> newEmployees)
        {
            await _context.Employee.AddRangeAsync(newEmployees);
            await _context.SaveChangesAsync();
            return newEmployees;
        }

        /*public async Task<Employee> EditEmployee(Employee emp)
        {
            _context.Update()
        }*/
    }
}