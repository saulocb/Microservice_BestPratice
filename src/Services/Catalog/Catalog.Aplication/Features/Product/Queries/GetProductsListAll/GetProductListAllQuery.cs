using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using MediatR;
using System;
using System.Collections.Generic;

namespace Catalog.Aplication.Features.Product.Queries.GetProductsListAll
{
    public class GetProductListAllQuery : IRequest<List<ProductsVm>>
    {

    }
}
