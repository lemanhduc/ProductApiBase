using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mississippi.product.Models;
using Microsoft.AspNetCore.Mvc;
using product;

namespace mississippi.product.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {

        IProductStore _productStore;
        public ProductController (IProductStore productStore) {
            _productStore = productStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get () {
            var products = _productStore.Find (p => { return true; });
            if (products.Any ()) {
                return Ok (products);
            }
            return NotFound (new List<Product>());
        }

        [HttpGet ("{id}")]
        public ActionResult<Product> Get (long id) {
            var product = _productStore.Find (p => p.Id == id).SingleOrDefault ();
            if (product != null) {
                return Ok (product);
            }
            return NotFound (new Product());
        }

        [HttpPost]
        public ActionResult Post ([FromBody] Product product) {
            var saved = _productStore.CreateProduct (product);
            if (saved > 0) {
                return CreatedAtAction (nameof (Get), new { id = product.Id }, product);
            }
            return Conflict ();
        }

        [HttpPut ("{id}")]
        public ActionResult Put (long id, [FromBody] Product product) {
            var updated = _productStore.UpdateProduct(product, id);
            if (updated > 0) {
                return CreatedAtAction (nameof (Get), new { id = product.Id }, product);
            }
            return Conflict ();
        }

        [HttpDelete ("{id}")]
        public void Delete (long id) {
            _productStore.Delete (id);
        }
    }
}