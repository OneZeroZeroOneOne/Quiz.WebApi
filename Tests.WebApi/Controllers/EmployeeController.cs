using System;
using System.Buffers.Text;
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
using Tests.WebApi.Utilities.Exceptions;
using Tests.WebApi.Utilities.Validation;

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
            return new List<OutEmployeeViewModel>();
            
        }

        [HttpPost]
        [Authorize(Policy = "ClientAdmin")]
        public async Task<OutEmployeeViewModel> AddEmployee(InEmployeeViewModel employeeInViewModel)
        {
            if (employeeInViewModel.Avatar != null && Base64Validator.IsBase64String(employeeInViewModel.Avatar) == true)
            {
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.AvatarIsNotBase64, "Avatart is not Base64 string");
            }
            if (employeeInViewModel.Resume != null && Base64Validator.IsBase64String(employeeInViewModel.Resume) == true)
            {
            }
            else
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.ResumeIsNotBase64, "Resume is not Base64 string");
            }
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            Employee newEmp = _mapperProfile.Map<Employee>(employeeInViewModel);
            await _employeeService.AddEmployee(newEmp, authorizedUserModel.Id);
            return _mapperProfile.Map<OutEmployeeViewModel>(newEmp);

        }


        [HttpPost]
        [Authorize(Policy = "ClientAdmin")]
        [Route("{id}")]
        public async Task<OutEmployeeViewModel> EditEmployee(InEmployeeViewModel inEmployeeViewModel, int id)
        {

            if (inEmployeeViewModel.Avatar != null)
            {
                if (Base64Validator.IsBase64String(inEmployeeViewModel.Avatar) == false)
                {
                    throw ExceptionFactory.SoftException(ExceptionEnum.AvatarIsNotBase64, "Avatart is not Base64 string");
                }
            }
            if (inEmployeeViewModel.Resume != null)
            {
                if (Base64Validator.IsBase64String(inEmployeeViewModel.Resume) == false)
                {
                    throw ExceptionFactory.SoftException(ExceptionEnum.ResumeIsNotBase64, "Resume is not Base64 string");
                }
            }
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            Employee emp = _mapperProfile.Map<Employee>(inEmployeeViewModel);
            Employee editedEmp = await _employeeService.EditEmployee(emp, id, authorizedUserModel.Id);
            return _mapperProfile.Map<OutEmployeeViewModel>(editedEmp);
        }
        /*[HttpDelete]
        [Authorize(Policy = "ClientAdmin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            AuthorizedUserModel authorizedUserModel = (AuthorizedUserModel)HttpContext.User.Identity;
            await _employeeService.DeleteEmployee(id, authorizedUserModel.Id);
            return Ok();

        }*/
    }
}
