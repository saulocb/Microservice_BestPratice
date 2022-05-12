using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Aplication.Features.Product.Queries.GetProductById
{
    public  class GetProductQuery:  IRequest<ProductsVm>
    {
        public  string ProductId { get; set; }
        public GetProductQuery(string productId)
        {
            ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
        }
    }
}
