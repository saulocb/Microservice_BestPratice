using MediatR;

namespace Catalog.Aplication.Features.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public string Id { get; set; }
    }
}
