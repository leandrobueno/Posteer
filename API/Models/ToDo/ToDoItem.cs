namespace API.Models.ToDo
{
  public class ToDoItem
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime DuoDate { get; set; }
    public virtual IList<Label>? Labels { get; set; }
  }

}