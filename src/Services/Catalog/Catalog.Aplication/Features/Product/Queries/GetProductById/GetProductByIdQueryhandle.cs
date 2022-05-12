using AutoMapper;
using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Aplication.Features.Product.Queries.GetProductById
{
    public class GetProductByIdQueryhandle : IRequestHandler<GetProductQuery, ProductsVm>
    {
        private readonly IProductRepository _iproductRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryhandle(IProductRepository iproductRepository, IMapper mapper)
        {
            _iproductRepository = iproductRepository;
            _mapper = mapper;
        }

        public async Task<ProductsVm> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var productList = await _iproductRepository.GetProduct(request.ProductId);
            return _mapper.Map<ProductsVm>(productList);
        }
    }
}
