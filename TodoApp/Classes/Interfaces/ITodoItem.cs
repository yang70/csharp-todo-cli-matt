namespace TodoApp.Classes.Interfaces
{
	public interface ITodoItem
	{
    string Text { get; set; }
    bool IsComplete { get; set; }
	}
}
