using Microsoft.EntityFrameworkCore;

using TodoApp.Classes.Models;

namespace TodoApp.Classes.Services
{
  public class TodoContext : DbContext
  {
    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost:5432;Database=todo;Username=postgres;Password=");
    }
  }
}
