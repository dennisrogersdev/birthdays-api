using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teste.Data;
using teste.Models;
using teste.ViewModels;

namespace teste.Controllers
{
    [ApiController]
    [Route("people")]
    public class PersonController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> List(
            [FromServices] AppDbContext context)
        {
            var people = await context.People.ToListAsync();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(
            [FromRoute] int id, 
            [FromServices] AppDbContext context) 
        {
            var person = await context.People.FirstOrDefaultAsync(p => p.Id == id);
            return Ok(person);
        }
    

        [HttpPost]
        public async Task<IActionResult> Save(
            [FromServices] AppDbContext context,
            [FromBody] PersonViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var person = new Person {
                Name = model.Name,
                BirthDate = model.BirthDate,
                CreatedAt = DateTime.Now
            };

            await context.People.AddAsync(person);
            await context.SaveChangesAsync();

            return Created($"v1/people/{person.Id}", person);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromServices] AppDbContext context,
            [FromRoute] int id,
            [FromBody] PersonViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var person = await context.People.FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
                return NotFound();

            person.Name = model.Name;
            person.BirthDate = model.BirthDate;
            person.UpdatedAt = DateTime.Now;

            context.People.Update(person);
            await context.SaveChangesAsync();
            return Ok(person);
            
        }
    }
}