using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.WebApi.Bll.Authorization;
using Tests.WebApi.Bll.Services;
using Tests.WebApi.Dal.In;
using Tests.WebApi.Dal.Models;
using Tests.WebApi.Dal.Out;

namespace Tests.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        EmployeeService _employeeService;
        private readonly IMapper _mapperProfile;
        public EmployeeController(EmployeeService employeeService, IMapper mapperProfile)
        {
            _employeeService = employeeService;
            _mapperProfile = mapperProfile;
        }

        [HttpGet]
        [Authorize(Policy = "ClientAdmin")]
        [Route("{id}")]
        public async Task<OutEmployeeViewModel> GetEmployee(int id)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            Employee emp = await _employeeService.GetEmployee(id, authorizedUserModel.Id);
            if (emp != null)
            {
                return _mapperProfile.Map<OutEmployeeViewModel>(emp);
            }
            return null;
        }

        [HttpGet]
        [Authorize(Policy = "ClientAdmin")]
        public async Task<List<OutEmployeeViewModel>> GetEmployees([FromQuery]int? quizStatusId = null)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            List<Employee> emps = await _employeeService.GetEmployees(authorizedUserModel.Id, quizStatusId);
            if (emps != null)
            {
                return _mapperProfile.Map<List<OutEmployeeViewModel>>(emps);
            }
            return new List<OutEmployeeViewModel>();
            
        }

        [HttpPost]
        [Authorize(Policy = "ClientAdmin")]
        public async Task<OutEmployeeViewModel> AddEmployee(InEmployeeViewModel inEmployeeViewModel)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;

            Employee newEmp = _mapperProfile.Map<Employee>(inEmployeeViewModel);
            await _employeeService.AddEmployee(newEmp, authorizedUserModel.Id);

            return _mapperProfile.Map<OutEmployeeViewModel>(newEmp);
        }


        [HttpPatch]
        [Authorize(Policy = "ClientAdmin")]
        [Route("{id}")]
        public async Task<OutEmployeeViewModel> EditEmployee(InEmployeeViewModel inEmployeeViewModel, int id)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;

            Employee emp = _mapperProfile.Map<Employee>(inEmployeeViewModel);
            Employee editedEmp = await _employeeService.EditEmployee(emp, id, authorizedUserModel.Id);

            return _mapperProfile.Map<OutEmployeeViewModel>(editedEmp);
        }
    }
}
