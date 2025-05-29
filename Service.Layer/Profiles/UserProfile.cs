using AutoMapper;
using Data.Layer.Entities.Identity;
using Service.Layer.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Layer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // AppUser → UserVM
            CreateMap<AppUser, UserVM>()
                .ForMember(dest => dest.Role, opt => opt.Ignore()); // handled manually

            // UserVM → AppUser
            CreateMap<UserVM, AppUser>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
