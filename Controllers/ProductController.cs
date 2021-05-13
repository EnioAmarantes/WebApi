using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

using webapi.Models;
using webapi.Data;


namespace webapi
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController: ControllerBase
    {

        //GETAll
        [HttpGet]
        [RequireHttps]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context
                .Products
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

        //GETBYID
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Get([FromServices] DataContext context, int id)
        {
            var product = await context
                .Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            
            return product;
        }

        //GETBYCATEGORY
        [HttpGet]
        [Route("category/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var products = await context
                .Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.Category.Id == id)
                .ToListAsync();

            return products;
        }


        //POST
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product model)
        {
            if(ModelState.IsValid)
            {
                context.Add(model);
                await context.SaveChangesAsync();
                return context
                    .Products
                    .Include(p => p.Category)
                    .AsNoTracking()
                    .Where(p => p.Id == model.Id)
                    .FirstOrDefault();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE
        //[HttpDelete]
        //[Route("{id:int}")]
        
    }
}