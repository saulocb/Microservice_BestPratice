using MediatR;
using System;
using System.Collections.Generic;

namespace Catalog.Aplication.Features.Product.Queries.GetProductsList
{
    public class GetProductListQuery : IRequest<List<ProductsVm>>
    {
        public string Name { get; set; }

        public GetProductListQuery(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
