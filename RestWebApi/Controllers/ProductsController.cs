using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWebApi.Data;
using RestWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductDbContext productDbContext;
        public ProductsController(ProductDbContext _productDbContext)
        {
            productDbContext = _productDbContext;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> Get(string sortPrice)
        {
            IQueryable<Product> products;
            switch (sortPrice)
            {
                case "desc":
                    products = productDbContext.Products.OrderByDescending(p => p.Price);
                    break;
                case "asc":
                    products = productDbContext.Products.OrderBy(p => p.Price);
                    break;
                default:
                    products = productDbContext.Products;
                    break;
            }
            return products;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var product = productDbContext.Products.SingleOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound("No Records Found...");
            }

            return Ok(product);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            productDbContext.Products.Add(product);
            productDbContext.SaveChanges(true);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            try
            {
                productDbContext.Products.Update(product);
                productDbContext.SaveChanges(true);
                return Ok("Product Updated...");
            }
            catch (Exception)
            {
                return NotFound("No record found against this id");
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = productDbContext.Products.SingleOrDefault(s => s.ProductId == id);
            if (product == null)
            {
                return NotFound("No record found...");
            }

            try
            {
                productDbContext.Products.Remove(product);
                productDbContext.SaveChanges(true);
                return Ok("Product Deleted...");
            }
            catch (Exception)
            {
                return NotFound("No record found against this id");
            }
        }
    }
}
