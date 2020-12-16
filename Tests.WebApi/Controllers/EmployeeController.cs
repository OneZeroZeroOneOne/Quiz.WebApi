using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<List<OutEmployeeViewModel>> GetEmployees()
        {

            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            List<Employee> emps = await _employeeService.GetEmployees(authorizedUserModel.Id);
            if (emps != null)
            {
                return _mapperProfile.Map<List<OutEmployeeViewModel>>(emps);
            }
            return null;
            
        }

        [HttpPost]
        [Authorize(Policy = "ClientAdmin")]
        public async Task<OutEmployeeViewModel> AddEmployee(InEmployeeViewModel employeeInViewModel)
        {

            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            Employee newEmp = _mapperProfile.Map<Employee>(employeeInViewModel);
            await _employeeService.AddEmployee(newEmp, authorizedUserModel.Id);
            return _mapperProfile.Map<OutEmployeeViewModel>(newEmp);

        }


        /*[HttpDelete]
        [Authorize(Policy = "ClientAdmin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            await _employeeService.DeleteEmployee(id, authorizedUserModel.Id);
            return Ok();

        }
        */
    }
}