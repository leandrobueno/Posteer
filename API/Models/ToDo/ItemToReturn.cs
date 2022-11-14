using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ToDo
{
  public class ItemToReturn
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime DuoDate { get; set; }
    // public List<Label>? Labels { get; set; }
  }
}