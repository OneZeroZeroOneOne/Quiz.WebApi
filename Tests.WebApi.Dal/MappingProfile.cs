
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using AutoMapper;
using Tests.WebApi.Dal.In;
using Tests.WebApi.Dal.Models;
using Tests.WebApi.Dal.Out;

namespace Tests.WebApi.Dal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Status, OutStatusViewModel>();
            CreateMap<Quiz, OutQuizViewModel>().ForMember(x => x.Status, x => x.MapFrom(y => y.Status));
            CreateMap<InEmployeeViewModel, Employee>();
            CreateMap<Employee, OutEmployeeViewModel>().ForMember(x => x.Quizzes, x => x.MapFrom(y => y.UserQuizzes.ToList().Select(t => t.Quiz)));
            CreateMap<Employee, Employee>().ForMember(x => x.Id, x => x.MapFrom((y, yy) => yy.Id));
        }
    }
}