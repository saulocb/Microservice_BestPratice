using Catalog.Aplication.Features.Product.Commands.CreateProduct;
using Catalog.Aplication.Features.Product.Commands.UpdateProduct;
using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Aplication.Interfaces
{
     public interface IProductService
    {
        public Task<IEnumerable<ProductsVm>> GetProducts();
        public Task<ProductsVm> GetProduct(string id);

        public Task<List<ProductsVm>> GetProductbyListIds(List<string> ids);
        public Task<IEnumerable<ProductsVm>> GetProductByName(string name);
        public Task<IEnumerable<ProductsVm>> GetProductByCategory(string categoryName);

        public Task CreateProduct(CreateProductCommand product);
        public Task UpdateProduct(UpdateProductCommand product);
        public Task DeleteProduct(string id);

    }
}
