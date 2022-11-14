using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.ToDo;
using Microsoft.AspNetCore.Identity;

namespace API.Models.Account
{
  public class ApplicationUser : IdentityUser
  {
    public virtual ICollection<ToDoItem>? ToDoItems { get; set; }
  }
}