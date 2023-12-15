using Microsoft.EntityFrameworkCore;

public class Program {
    public static void Main(string[] args)
    {
        var context = new TodoContext();
        var todoList = new TodoList(context);
        string action = (args.Length == 0) ? "" : args[0];

        if (new[] {"complete", "add"}.Contains(action) && args.Length == 1)
        {
            Console.WriteLine($"Missing id for '{action}' action");
            todoList.Show();
            return;
        }

        switch(action)
        {
            case "add":
                string text = String.Join(" ", args[1..]);
                todoList.Add(text);
                break;
            case "complete":
                var completeId = int.Parse(args[1]);
                todoList.Complete(completeId);
                break;
            case "reset":
                todoList.Reset();
                break;
        }
        todoList.Show();
    }
}

public interface ITodoItem
{
    string Text { get; set; }
    bool IsComplete { get; set; }
}

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
            Console.WriteLine($"Item with id {id} not found");
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

public class TodoContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=todo;Username=postgres;Password=");
    }
}
