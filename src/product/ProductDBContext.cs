using Microsoft.EntityFrameworkCore;
using mississippi.product.Models;

namespace mississippi.product {
    public class ProductDBContext : DbContext {
        public ProductDBContext (DbContextOptions<ProductDBContext> options) : base (options) {

        }

        public virtual DbSet<Product> Products { get; set; }
    }
}