using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProductBackend.EF;
using ProductBackend.Models;

namespace ProductBackend.Controllers.API
{
    public class ProductController : ApiController
    {
        private DBContext db = new DBContext();

        // GET: api/Product
        [HttpGet]
        public IQueryable<ProductModel> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Product/5
        [HttpGet]
        [ResponseType(typeof(ProductModel))]
        public async Task<IHttpActionResult> GetProductModel(int id)
        {
            ProductModel productModel = await db.Products.FindAsync(id);
            if (productModel == null)
            {
                return Content(HttpStatusCode.NotFound, $"沒有找到水果({id})");
                //return NotFound();
            }

            return Ok(productModel);
        }

        // PUT: api/Product/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductModel(int id, ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productModel.ID)
            {
                return BadRequest("id不符合。");
            }

            db.Entry(productModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Product
        [HttpPost]
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult PostProductModel(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(productModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productModel.ID }, productModel);
        }

        // DELETE: api/Product/5
        [HttpDelete]
        [ResponseType(typeof(ProductModel))]
        public async Task<IHttpActionResult> DeleteProductModel(int id)
        {
            ProductModel productModel = await db.Products.FindAsync(id);
            if (productModel == null)
            {
                return Content(HttpStatusCode.NotFound, $"沒有找到水果({id})");
                //return NotFound();
            }

            db.Products.Remove(productModel);
            await db.SaveChangesAsync();

            return Ok(productModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductModelExists(int id)
        {
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}