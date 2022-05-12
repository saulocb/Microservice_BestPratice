using Catalog.Aplication.Features.Product.Commands.CreateProduct;
using Catalog.Aplication.Features.Product.Commands.DeleteProduct;
using Catalog.Aplication.Features.Product.Commands.UpdateProduct;
using Catalog.Aplication.Features.Product.Queries.GetProduByCategoryName;
using Catalog.Aplication.Features.Product.Queries.GetProductById;
using Catalog.Aplication.Features.Product.Queries.GetProductByListId;
using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using Catalog.Aplication.Features.Product.Queries.GetProductsListAll;
using Catalog.Aplication.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Aplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;
        private readonly IMediator _mediator;

        public ProductService(IProductRepository repository, ILogger<ProductService> logger, IMediator mediator)
        {
            _repository = repository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task CreateProduct(CreateProductCommand product)
        {
            await _mediator.Send(product);
        }

        public async Task DeleteProduct(string id)
        {
           await _mediator.Send(new DeleteProductCommand() { Id = id});
        }

        public async Task<ProductsVm> GetProduct(string id)
        {
           return await _mediator.Send(new GetProductQuery(id));
        }

        public async Task<IEnumerable<ProductsVm>> GetProductByCategory(string categoryName)
        {
            return await _mediator.Send(new GetProduByCategoryNameQuery(categoryName));

        }

        public async Task<List<ProductsVm>> GetProductbyListIds(List<string> ids)
        {
            return await _mediator.Send(new GetProductByListIdsQuery(ids));
        }

        public async Task<IEnumerable<ProductsVm>> GetProductByName(string name)
        {
            return await _mediator.Send(new GetProductListQuery(name));
        }

        public async Task<IEnumerable<ProductsVm>> GetProducts()
        {
            return await _mediator.Send(new GetProductListAllQuery());
        }

        public async Task UpdateProduct(UpdateProductCommand product)
        {
              await _mediator.Send(product);
        }
    }
}
