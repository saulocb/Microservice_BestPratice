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

namespace Catalog.Aplication.Features.Product.Queries.GetProductByListId
{
    public class GetProductByILisdsQueryhandle : IRequestHandler<GetProductByListIdsQuery, List<ProductsVm>>
    {
        private readonly IProductRepository _iproductRepository;
        private readonly IMapper _mapper;

        public GetProductByILisdsQueryhandle(IProductRepository iproductRepository, IMapper mapper)
        {
            _iproductRepository = iproductRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductsVm>> Handle(GetProductByListIdsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _iproductRepository.GetProductByListId(request.ItemsId);
            return _mapper.Map<List<ProductsVm>>(productList);
        }
    }
}
