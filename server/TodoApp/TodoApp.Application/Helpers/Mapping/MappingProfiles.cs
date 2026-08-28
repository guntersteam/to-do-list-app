using AutoMapper;
using TodoApp.Domain.Contracts.Category;
using TodoApp.Domain.Contracts.Task;
using TodoApp.Domain.Contracts.User;
using TodoApp.Domain.Models;
using Task = TodoApp.Domain.Models.Task;

namespace TodoApp.Application.Helpers.Mapping;

public class MappingProfiles : Profile
{
   public MappingProfiles()
   {
      CreateMap<User,UserDto>();
      CreateMap<Category,CategoryDto>();
      
      CreateMap<Task, TaskDto>()
         .ForMember(dest => dest.TaskCategories, opt => 
            opt.MapFrom(src => src.TaskCategories.Select(tc => tc.Category)));
   }
}