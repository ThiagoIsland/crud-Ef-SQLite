using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{

    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet("/Pega")]
        public IActionResult Get(
            [FromServices] AppDbContext context)
        
           => Ok(context.Todos.ToList()); 
        
        
        [HttpGet("/Pega/{id:int}")]
        public IActionResult GetById(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var todos = context.Todos.FirstOrDefault(x => x.Id == id);
            
            if (todos == null)
                return NotFound();

            return Ok(todos);
        }

        
        [HttpPost("/Post")]
        public IActionResult Post(
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();

            return Created($"/Post/{todo.Id}",todo);
        }


        [HttpPut("/Put/{id:int}")]
        public IActionResult Put(
            [FromRoute] int id,
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            
            if (model == null)
                return NotFound();


            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();

            return Ok(model);

        }
        [HttpDelete("/Delete/{id:int}")]
        public IActionResult Deçete(
            [FromRoute] int id,
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);

            if (model == null)
                return NotFound();

            context.Todos.Remove(model);
            context.SaveChanges();

            return Ok(model);

        }
    }
}
