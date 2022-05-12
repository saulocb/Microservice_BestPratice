using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using MediatR;
using System;
using System.Collections.Generic;

namespace Catalog.Aplication.Features.Product.Queries.GetProduByCategoryName
{
    public class GetProduByCategoryNameQuery : IRequest<List<ProductsVm>>
    {
        public string CategoryName { get; set; }

        public GetProduByCategoryNameQuery(string categoryname)
        {
            CategoryName = categoryname ?? throw new ArgumentNullException(nameof(categoryname));
        }
    }
}
