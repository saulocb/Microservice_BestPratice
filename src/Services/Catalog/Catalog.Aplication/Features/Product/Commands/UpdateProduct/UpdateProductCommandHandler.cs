using AutoMapper;
using Catalog.Aplication.Exceptions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Aplication.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _iproductRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IMapper mapper, ILogger<UpdateProductCommandHandler> logger, IProductRepository iproductRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _iproductRepository = iproductRepository ?? throw new ArgumentNullException(nameof(iproductRepository));
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productToUpdate = await _iproductRepository.GetProduct(request.Id);
            if (productToUpdate == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            
            _mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Catalog.Domain.Entities.Product));

            await _iproductRepository.UpdateProduct(productToUpdate);

            _logger.LogInformation($"Product {productToUpdate.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}
