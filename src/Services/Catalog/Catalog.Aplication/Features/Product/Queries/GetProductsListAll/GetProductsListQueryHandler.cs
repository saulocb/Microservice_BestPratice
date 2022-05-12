using AutoMapper;
using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Catalog.Aplication.Features.Product.Queries.GetProductsListAll
{
    public class GetProductsListAllQueryHandler : IRequestHandler<GetProductListAllQuery, List<ProductsVm>>
    {
        private readonly IProductRepository _iproductRepository;
        private readonly IMapper _mapper;

        public GetProductsListAllQueryHandler(IMapper mapper, IProductRepository iproductRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _iproductRepository = iproductRepository;
        }

        public async Task<List<ProductsVm>> Handle(GetProductListAllQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _iproductRepository.GetProducts();
            var test = _mapper.Map<List<ProductsVm>>(orderList);
            return _mapper.Map<List<ProductsVm>>(orderList);
        }
    }
}
