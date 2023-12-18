
using TodoApp.Classes.Models;
using TodoApp.Classes.Services;

public class Program {
    public static void Main(string[] args)
    {
        var context = new TodoContext();
        var todoList = new TodoList(context);
        string action = (args.Length == 0) ? "" : args[0];

        if (new[] {"complete", "add"}.Contains(action) && args.Length == 1)
        {
            Console.WriteLine($"Missing second argument for '{action}' action\n");
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
                if (int.TryParse(args[1], out var completeId))
                {
                    todoList.Complete(completeId);
                }
                else
                {
                    Console.WriteLine("Invalid id for 'complete' action");
                    return;
                }
                break;
            case "reset":
                todoList.Reset();
                break;
        }
        todoList.Show();
    }
}
