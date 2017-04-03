using System.Collections.Generic;
using ToDoApi.Models;

namespace ToDoApi.Interfaces
{
    public interface ITodoRepository
    {
        void Add(TodoItem item);

        IEnumerable<TodoItem> GetAll();

        TodoItem Find(long key);

        void Update(TodoItem item);

        void Remove(long key);

    }
}
