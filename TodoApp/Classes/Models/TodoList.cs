using Microsoft.EntityFrameworkCore;

using TodoApp.Classes.Models;
using TodoApp.Classes.Services;

namespace TodoApp.Classes.Models
{
	public class TodoList
	{
			private readonly TodoContext _context;

			public List<TodoItem> Items = new List<TodoItem>();

			public TodoList(TodoContext context)
			{
					_context = context;
			}

			public void Show()
			{
					var items = _context.TodoItems.OrderBy(item => item.Id).ToList();

					if (!items.Any())
					{
							Console.WriteLine("Hooray! No Items");
							return;
					}
			Console.WriteLine("Todo List");
			Console.WriteLine("---------");
			for (int i=0; i < items.Count; i++) {
							var item = items[i];
				Console.WriteLine($"{item.Id}. {item.Text} - {(item.IsComplete ? "✅" : "🔲")}");
			}
			}
			public void Add(string text)
			{
					_context.TodoItems.Add(new TodoItem(text));
					_context.SaveChanges();
			}
			public void Complete(int id)
			{
					var item = _context.TodoItems.Find(id);
					if (item == null)
					{
							Console.WriteLine($"Item with id {id} not found\n");
							return;
					}
					item.IsComplete = true;
					_context.SaveChanges();
			}
			public void Reset()
			{
					_context.TodoItems.ExecuteDelete();
					_context.Database.ExecuteSqlRaw("ALTER SEQUENCE \"TodoItems_Id_seq\" RESTART WITH 1;");
					Console.WriteLine($"Deleted all items");
			}
	}
}