using TodoApp.Classes.Interfaces;

namespace TodoApp.Classes.Models
{
	public class TodoItem : ITodoItem
	{
			public int Id { get; set; }
			public string Text { get; set; }
			public bool IsComplete { get; set; }
			public TodoItem(string text = "", bool isComplete = false)
			{
					Text = text;
					IsComplete = isComplete;
			}
	}
}
