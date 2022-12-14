using API.Models.Account;

namespace API.Models.ToDo
{
  public class ToDoItem
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime DuoDate { get; set; }
    public Guid UserId { get; set; }
  }
}