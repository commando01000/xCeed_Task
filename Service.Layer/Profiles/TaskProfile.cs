using AutoMapper;
using Data.Layer.Entities;
using Service.Layer.ViewModels.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Layer.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            // From Entity to ViewModel
            CreateMap<TaskItem, TaskVM>()
                .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedUser.DisplayName));

            // From ViewModel to Entity, ignore nulls
            CreateMap<TaskVM, TaskItem>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
