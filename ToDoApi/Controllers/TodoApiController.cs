using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoApi.Interfaces;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/v1/todos")]
    public class TodoApiController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public TodoApiController(ITodoRepository todoRepository)
        {
            this._todoRepository = todoRepository;
        }

        /// <summary>
        /// GetAll Todos
        /// </summary>
        /// <returns></returns>
        #region GetAll
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_todoRepository.GetAll());
        }
        #endregion

        /// <summary>
        /// Get Todo by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region GetById
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {          
            var item = _todoRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        #endregion

        /// <summary>
        /// Created a new Todo
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        #region Create
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
            }

            _todoRepository.Add(item);

            return CreatedAtRoute("GetTodo", new { id = item.Key }, item);
        }
        #endregion

        /// <summary>
        /// Update a specific Todo by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        #region Update
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = _todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _todoRepository.Update(todo);
            return new NoContentResult();
        }
        #endregion

        /// <summary>
        /// Delete a specific Todo by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _todoRepository.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _todoRepository.Remove(id);
            return new NoContentResult();
        }
        #endregion

        /// <summary>
        /// Error Code
        /// </summary>
        #region ErrorCode
        public enum ErrorCode
        {
            TodoItemNameAndNotesRequired,
            TodoItemIDInUse,
            RecordNotFound,
            CouldNotCreateItem,
            CouldNotUpdateItem,
            CouldNotDeleteItem
        }
        #endregion

    }
}