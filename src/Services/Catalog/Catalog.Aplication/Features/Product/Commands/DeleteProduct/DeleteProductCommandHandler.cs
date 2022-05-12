using AutoMapper;
using Catalog.Aplication.Exceptions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Aplication.Features.Product.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _iproductRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IMapper mapper, ILogger<DeleteProductCommandHandler> logger, IProductRepository iproductRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _iproductRepository = iproductRepository ?? throw new ArgumentNullException(nameof(iproductRepository));
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _iproductRepository.GetProduct(request.Id.ToString());
            if (productToDelete == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }            

            await _iproductRepository.DeleteProduct(productToDelete.Id.ToString());

            _logger.LogInformation($"Product {productToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
