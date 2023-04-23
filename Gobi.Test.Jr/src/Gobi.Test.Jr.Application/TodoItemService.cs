using Gobi.Test.Jr.Domain;
using Gobi.Test.Jr.Domain.Interfaces;

namespace Gobi.Test.Jr.Application
{
    public class TodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todoItemRepository.GetAll();
        }

        public void Add(TodoItem todoItem)
        {
            _todoItemRepository.Add(todoItem);
        }

        public void Put(TodoItem todoItem)
        {
            _todoItemRepository.Put(todoItem);
        }

        public void Delete(TodoItem todoItem)
        {
            _todoItemRepository.Delete(todoItem);
        }

        public TodoItem GetOne(int id)
        {
            return _todoItemRepository.GetOne(id);
        }
    }
}