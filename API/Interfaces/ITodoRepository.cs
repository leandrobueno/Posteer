using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.ToDo;
using SendGrid;

namespace API.Interfaces
{
  public interface ITodoRepository
  {
    Task<Response<ItemToReturn>> Create(ItemToCreate item);
    Task<Response<ItemToReturn>> Get(Guid id);
    Task<Response<List<ItemToReturn>>> GetAll(Guid id);
    Task<Response<string>> Delete(Guid id);
  }
}