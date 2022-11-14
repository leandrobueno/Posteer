using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ToDo
{
  public class ItemToCreate
  {
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public DateTime DuoDate { get; set; }
    public Guid UserId { get; set; }
  }
}