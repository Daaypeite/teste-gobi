namespace Gobi.Test.Jr.Domain.Interfaces
{
	public interface ITodoItemRepository
    {
        IEnumerable<TodoItem> GetAll();
        void Add(TodoItem todoItem);

        void Put(TodoItem todoItem);

        void Delete(TodoItem todoItem);

        TodoItem GetOne(int id);
    }
}
