using MediatR;
using System;
using System.Threading.Tasks;

namespace Catalog.Aplication.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
