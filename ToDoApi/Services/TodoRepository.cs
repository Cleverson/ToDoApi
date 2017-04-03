using System.Collections.Generic;
using System.Linq;
using ToDoApi.Data;
using ToDoApi.Interfaces;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            this._context = context;

            Add(new TodoItem
            {
                Name = "item1",
                IsComplete = false
            });
            Add(new TodoItem
            {
                Name = "item2",
                IsComplete = true
            });
            Add(new TodoItem
            {
                Name = "item3",
                IsComplete = true
            });
            Add(new TodoItem
            {
                Name = "item4",
                IsComplete = true
            });
            Add(new TodoItem
            {
                Name = "item5",
                IsComplete = true
            });
            Add(new TodoItem
            {
                Name = "item6",
                IsComplete = false
            });
            Add(new TodoItem
            {
                Name = "item7",
                IsComplete = true
            });
            Add(new TodoItem
            {
                Name = "item8",
                IsComplete = true
            });
            Add(new TodoItem
            {
                Name = "item9",
                IsComplete = true
            });
            Add(new TodoItem
            {
                Name = "item10",
                IsComplete = true
            });
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItens.ToList();
        }

        public void Add(TodoItem item)
        {
            _context.TodoItens.Add(item);
            _context.SaveChanges();
        }

        public TodoItem Find(long key)
        {
            return _context.TodoItens.FirstOrDefault( t => t.Key == key);
        }        

        public void Remove(long key)
        {
            var entity = _context.TodoItens.First( t => t.Key == key);
            _context.TodoItens.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(TodoItem item)
        {
            _context.TodoItens.Update(item);
            _context.SaveChanges();
        }
    }
}