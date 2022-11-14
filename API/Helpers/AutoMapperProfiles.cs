using API.Models.Account;
using API.Models.ToDo;
using AutoMapper;

namespace API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<UserToRegister, ApplicationUser>();
      CreateMap<ApplicationUser, UserToReturn>();
      CreateMap<ItemToCreate, ToDoItem>();
      CreateMap<ToDoItem, ItemToReturn>();
    }
  }
}