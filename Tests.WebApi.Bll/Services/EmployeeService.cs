using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.WebApi.Contexts;
using Tests.WebApi.Dal.Models;
using Tests.WebApi.Utilities.Exceptions;

namespace Tests.WebApi.Bll.Services
{
    public class EmployeeService
    {
        private readonly MainContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(MainContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<List<Employee>> GetEmployees(int userId, int? quizStatusId)
        {
            var userEmployees = _context.UserEmployee.Where(x => x.UserId == userId).Include(x => x.Employee)
                .ThenInclude(x => x.UserQuizzes).ThenInclude(x => x.Quiz).ThenInclude(x => x.Status)
                .Select(x => x.Employee);

            if (quizStatusId != null)
                userEmployees = userEmployees.Where(x => x.UserQuizzes.Any() && x.UserQuizzes.OrderByDescending(xx => xx.Id).FirstOrDefault().Quiz.StatusId == quizStatusId);

            return await userEmployees.ToListAsync();
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

        public async Task<Employee> EditEmployee(Employee editEmp, int empId, int userId)
        {
            UserEmployee userEmployee = await _context.UserEmployee.FirstOrDefaultAsync(x => x.EmployeeId == empId);
            if (userEmployee == null || userEmployee.UserId != userId)
                throw ExceptionFactory.SoftException(ExceptionEnum.EditedUserIsNotYours, "Edited user is not yours");
            
            Employee emp = await _context.Employee.FirstOrDefaultAsync(x => x.Id == empId);

            _mapper.Map(editEmp, emp);

            await _context.SaveChangesAsync();
            return emp;
        }
    }
}
