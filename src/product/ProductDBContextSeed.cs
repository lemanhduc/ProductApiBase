using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mississippi.product;
using mississippi.product.Models;

namespace product {
    public class ProductDBContextSeed {
        public async Task SeedAsync (ProductDBContext context) {
            if (context.Products.Any ()) {
                return;
            }
            List<Product> products = new List<Product> {
                new Product {
                Name = "T-shirt Black",
                Description = "T-shirt Black",
                Price = 10,
                Sku = "MSP1"
                },
                new Product {
                Name = "T-shirt Blue",
                Description = "T-shirt Blue",
                Price = 11,
                Sku = "MSP2"
                },
                new Product {
                Name = "Shirt Black",
                Description = "Shirt Black",
                Price = 20,
                Sku = "MSP3"
                },
                new Product {
                Name = "Shirt Blue",
                Description = "Shirt Blue",
                Price = 20,
                Sku = "MSP4"
                },
                new Product {
                Name = "Pant Black",
                Description = "Pant Black",
                Price = 30,
                Sku = "MSP5"
                },
                new Product {
                Name = "Pant Blue",
                Description = "Pant Blue",
                Price = 30,
                Sku = "MSP6"
                }
            };
            context.Products.AddRange (products);
            await context.SaveChangesAsync ();
        }
    }
}