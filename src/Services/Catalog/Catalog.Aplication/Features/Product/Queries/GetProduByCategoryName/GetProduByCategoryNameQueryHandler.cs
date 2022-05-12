using AutoMapper;
using Catalog.Aplication.Features.Product.Queries.GetProductsList;
using Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Catalog.Aplication.Features.Product.Queries.GetProduByCategoryName
{
    public class GetProduByCategoryNameQueryHandler : IRequestHandler<GetProduByCategoryNameQuery, List<ProductsVm>>
    {
        private readonly IProductRepository _iproductRepository;
        private readonly IMapper _mapper;

        public GetProduByCategoryNameQueryHandler(IMapper mapper, IProductRepository iproductRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _iproductRepository = iproductRepository;
        }

        public async Task<List<ProductsVm>> Handle(GetProduByCategoryNameQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _iproductRepository.GetProductByCategory(request.CategoryName);
            return _mapper.Map<List<ProductsVm>>(orderList);
        }
    }
}
