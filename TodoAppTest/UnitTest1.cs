namespace TodoAppTest;

public class Tests
{
    private TodoList todoList;

    [SetUp]
    public void Setup()
    {
        todoList = new TodoList();
    }

    [Test]
    public void Show() {
        todoList.Add("Eat");
        todoList.Add("Sleep");
        todoList.Add("Dream");

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        todoList.Show();

        Assert.IsTrue(stringWriter.ToString().Contains("Eat"));
        Assert.IsTrue(stringWriter.ToString().Contains("Sleep"));
        Assert.IsTrue(stringWriter.ToString().Contains("Dream"));
    }

    [Test]
    public void Add()
    {
        string itemText = "Learn C#";
        todoList.Add(itemText);

        Assert.That(todoList.Items.Count, Is.EqualTo(1));
        Assert.That(todoList.Items[0].Text, Is.EqualTo(itemText));
    }

    [Test]
    public void Reset()
    {
        todoList.Add("Write C#");
        todoList.Add("Dream about C#");

        todoList.Reset();
        Assert.That(todoList.Items.Count, Is.EqualTo(0));
    }

    [Test]
    public void Complete()
    {
        todoList.Add("Write C#");
        todoList.Add("Dream about C#");
        todoList.Complete(0);

        Assert.That(todoList.Items[0].Text, Is.EqualTo("Dream about C#"));
    }
}