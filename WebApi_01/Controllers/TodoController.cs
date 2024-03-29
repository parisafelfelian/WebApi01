﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_01.Models;

namespace WebApi_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public ITodoRepository TodoItems { get; set; }

        public TodoController(ITodoRepository todoItem)
        {
            TodoItems = todoItem;
        }


        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            var item = TodoItems.GetAll();
            return item;
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult GetById(string id)
        {
            var item = TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
         public IActionResult Create([FromBody] TodoItem item, string id)
        {
            if (item.Key != id)
            {
                return BadRequest();
            }

            TodoItems.Add(item);
            return CreatedAtRoute("GetTodo", new { id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }
            TodoItems.Update(item);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] TodoItem item,string id)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var todo = TodoItems.Find(id);
            if (todo== null)
            {
                return NotFound();
            }
            item.Key = todo.Key;
            TodoItems.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var item = TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            TodoItems.Remove(id);
            return new NoContentResult();
        }
    }
}