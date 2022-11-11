using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Account
{
  public class User
  {
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
    public Gender Gender { get; set; }
    public virtual ApplicationUser? ApplicationUser { get; set; }
    public string? ApplicationUserId { get; set; }
  }
}