using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace WebApi_01.Models
{
    public class TodoRepository : ITodoRepository
    {
        private static ConcurrentDictionary<string, TodoItem> _Todos = new ConcurrentDictionary<string, TodoItem>();

        public TodoRepository()
        {
            Add(new TodoItem { Name = "Item01" });
        }
        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            _Todos[item.Key] = item;
        }

        public TodoItem Find(string key)
        {
            TodoItem item;
            _Todos.TryGetValue(key, out item);
            return item;
        }
        public void Update(TodoItem item)
        {
            _Todos[item.Key] = item;
        }

        public TodoItem Remove(string key)
        {
            TodoItem item;
            _Todos.TryRemove(key, out item);
            return item;
        }


        public IEnumerable<TodoItem> GetAll()
        {
            return _Todos.Values;
        }

    }
}
