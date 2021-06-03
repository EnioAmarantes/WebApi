using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using webapi.Models;
using webapi.Data;

namespace controller.CategoryController
{
    [ApiController]
    [Route("v1/categories")]
    public class categoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return categories;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Get([FromServices] DataContext context, int id)
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(obj => obj.Id == id);
            return category;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody]Category model)
        {
            if(ModelState.IsValid)
            {
                context.Categories.Add(model);
                model.Id = await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Category>> Put([FromServices] DataContext context, [FromBody]Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return context.Categories.AsNoTracking().FirstOrDefault(obj => obj.Id == category.Id);
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<string>> Delete([FromServices] DataContext context, [FromBody] Category category)
        {
            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return $"Categoria: { category.Title } removida com sucesso.";
            }
            catch (DbUpdateException ex) 
            {
                return $"Não foi possível remover a categoria: { category.Title } verifique o erro abaixo:\n { ex.Message }";
            }
        }

    }
}