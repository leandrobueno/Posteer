using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Models;
using API.Models.ToDo;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
  public class TodoRepository : ITodoRepository
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public TodoRepository(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Response<ItemToReturn>> Create(ItemToCreate itemToCreate)
    {
      var item = _mapper.Map<ToDoItem>(itemToCreate);
      _context.ToDoItems.Add(item);

      if (await _context.SaveChangesAsync() == 0)
      {
        return new Response<ItemToReturn>
        {
          Code = 501,
          Success = false,
          Message = "Error saving item. Please try again",
          Data = null
        };
      }

      return new Response<ItemToReturn>
      {
        Code = 201,
        Success = true,
        Message = null,
        Data = _mapper.Map<ItemToReturn>(item)
      };
    }

    public async Task<Response<ItemToReturn>> Get(Guid id)
    {
      var item = await _context.ToDoItems.FindAsync(id);

      if (item == null)
      {
        return new Response<ItemToReturn>
        {
          Code = 404,
          Success = false,
          Message = "Item not found with provided Id",
          Data = null
        };
      }

      return new Response<ItemToReturn>
      {
        Code = 200,
        Success = true,
        Message = null,
        Data = _mapper.Map<ItemToReturn>(item)
      };
    }

    public async Task<Response<List<ItemToReturn>>> GetAll(Guid id)
    {
      var item = await _context.ToDoItems.Where(x => x.UserId == id).ToListAsync();

      if (item == null)
      {
        return new Response<List<ItemToReturn>>
        {
          Code = 404,
          Success = false,
          Message = "Found no items for user.",
          Data = null
        };
      }

      item = item.OrderByDescending(x => x.CreatedAt).ToList();

      return new Response<List<ItemToReturn>>
      {
        Code = 200,
        Success = true,
        Message = null,
        Data = _mapper.Map<List<ItemToReturn>>(item)
      };
    }
  }
}