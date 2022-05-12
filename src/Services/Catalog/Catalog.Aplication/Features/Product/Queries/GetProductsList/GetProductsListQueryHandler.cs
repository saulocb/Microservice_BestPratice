using AutoMapper;
using Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Catalog.Aplication.Features.Product.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductListQuery, List<ProductsVm>>
    {
        private readonly IProductRepository _iproductRepository;
        private readonly IMapper _mapper;

        public GetProductsListQueryHandler(IMapper mapper, IProductRepository iproductRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _iproductRepository = iproductRepository;
        }

        public async Task<List<ProductsVm>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _iproductRepository.GetProductByName(request.Name);
            return _mapper.Map<List<ProductsVm>>(orderList);
        }
    }
}
