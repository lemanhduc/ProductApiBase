using System;
using System.Collections.Generic;
using mississippi.product.Models;

namespace product {
    public interface IProductStore {
        List<Product> Find (Func<Product, bool> predicate);
        int CreateProduct (Product product);
        int UpdateProduct (Product product, long id);
        void Delete (long id);
    }
}