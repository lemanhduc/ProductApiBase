using System;
using System.Collections.Generic;
using System.Linq;
using mississippi.product;
using mississippi.product.Models;

namespace product {
    public class ProductStore : IProductStore 
    {
        ProductDBContext _dbContext { get; set; }

        public ProductStore (ProductDBContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public List<Product> Find (Func<Product, bool> predicate) 
        {
            return _dbContext.Products.Where (predicate).ToList ();
        }

        public int CreateProduct (Product product) 
        {
            _dbContext.Add (product);
            return _dbContext.SaveChanges ();
        }

        public int UpdateProduct (Product product, long id) 
        {
            var update = _dbContext.Products.Where(c => c.Id == id).SingleOrDefault();
            update.Name = product.Name;
            update.Description = product.Description;
            update.Sku = product.Sku;
            update.Price = product.Price;
            return _dbContext.SaveChanges ();
        }
        
        public void Delete (long id) {
            var product = Find (p => p.Id == id).SingleOrDefault ();
            if (product != null) {
                _dbContext.Products.Remove (product);
                _dbContext.SaveChanges ();
            }
        }
    }
}