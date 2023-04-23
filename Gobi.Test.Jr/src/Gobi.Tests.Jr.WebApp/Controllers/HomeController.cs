using Gobi.Test.Jr.Application;
using Gobi.Test.Jr.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Gobi.Tests.Jr.WebApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoItemService _todoItemService;

        public TodoController(TodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public IActionResult Index()
        {
            var items = _todoItemService.GetAll();

            return View(items);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new TodoItem());
        }

        [HttpPost]
        public IActionResult Add(TodoItem todoItem)
        {
            _todoItemService.Add(todoItem);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetOne(int id)
        {
            var item = _todoItemService.GetOne(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _todoItemService.GetOne(id);
            if (item == null)
            {
                return NotFound();
            }
            _todoItemService.Delete(item);
            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(int id, TodoItem model)
        {
            var todoItem = _todoItemService.GetOne(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Description = model.Description;
            todoItem.Completed = model.Completed;

            _todoItemService.Put(todoItem);

            return RedirectToAction("Index");
        }

    }
}