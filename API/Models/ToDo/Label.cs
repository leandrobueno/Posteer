using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ToDo
{
  public class Label
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
  }
}