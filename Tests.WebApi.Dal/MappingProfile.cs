
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
            // Add as many of these lines as you need to map your objects
            /* CreateMap<Category, CategoryResponseViewModel>().ForMember(x => x.Groups, x => x.MapFrom(y => y.Group.ToList()));
             CreateMap<Source, SourceResponseViewModel>();
             CreateMap<Content, ContentResponseViewModel>();
             CreateMap<UserCredential, UserCredentialResponseViewModel>();
             CreateMap<Group, GroupResponseViewModel>().ForMember(x => x.Sources, x => x.MapFrom(y => y.GroupSource.Select(t => t.Source)));
             */
        }
    }
}