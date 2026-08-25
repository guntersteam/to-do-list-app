using AutoMapper;
using TodoApp.Domain.Contracts.User;
using TodoApp.Domain.Models;

namespace TodoApp.Application.Helpers.Mapping;

public class MappingProfiles : Profile
{
   public MappingProfiles()
   {
      CreateMap<User,UserDto>();
   }
}